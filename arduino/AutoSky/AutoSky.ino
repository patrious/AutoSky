/*Arduino code for AutoSky.

Reads commands sent via the Serial (USB) connection and acts upon them.

Currently, no motor control.

*/
#include <SoftwareSerial.h>
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_GPS.h>
#include <Math.h>

int altPin1 = 10;
int altPin2 = 11;
int aziPin1 = 5;
int aziPin2 = 6;

//MAX_ITERATIONS = ItersPerSec (5) * MaxSeconds
const int MAX_ITERATIONS = 100;
const int FAST = 255;
const int SLOW = 200;

SoftwareSerial gpsSerial(4, 3); //Digits are (TX, RX)

Adafruit_GPS gps(&gpsSerial);
Adafruit_LSM303_Mag_Unified magnet = Adafruit_LSM303_Mag_Unified(11111);
Adafruit_LSM303_Accel_Unified accel = Adafruit_LSM303_Accel_Unified(11112);

boolean usingInterrupt = false;
void useInterrupt(boolean);

//Points the telescope to the given altitude (using 3axis) and azimuth (using magnetometer)
int pointTelescope(float altitude, float azimuth) {
  //First, set the altitude using the 3axis data
  //Y arrow points towards front of telescope. Z arrow points straight up. (X axis not used.)
  sensors_event_t accEvent;
  for (int i = 0; i < MAX_ITERATIONS; i++) {
    accel.getEvent(&accEvent);
    double heading = (atan2(accEvent.acceleration.y, accEvent.acceleration.z) * 180.0) / 3.1415;
    //Too close to adjust
    if (abs(altitude - heading) < 1) {
      break;
    }
    //Fine control
    else if (abs(altitude - heading) < 5) {
      if (altitude > heading) {
        telescopeUp(SLOW);
      } else {
        telescopeDown(SLOW);
      }
    }
    //Coarse control
    else {
      if (altitude > heading) {
        telescopeUp(FAST);
      } else {
        telescopeDown(FAST);
      }
    }
    if (i >= MAX_ITERATIONS - 1) {
      return 1;
    }
  }
  //Now set the azimuth using the magnetometer
  //X arrow points to side of telescope. Y arrow points towards front of telescope. (Z axis not used.)
  sensors_event_t magEvent;
  for (int i = 0; i < MAX_ITERATIONS; i++) {
    magnet.getEvent(&magEvent);
    double heading = (atan2(magEvent.magnetic.y, magEvent.magnetic.x) * 180.0) / 3.1415;
    if (heading < 0) heading += 360;
    //Too close to adjust
    if (abs(azimuth - heading) < 1) {
      break;
    }
    //Fine control
    else if (abs(azimuth - heading) < 5) {
      if (azimuth > heading) {
        telescopeRight(SLOW);
      } else {
        telescopeLeft(SLOW);
      }
    }
    //Coarse control
    else {
      if ((azimuth - 180.0) > (heading + 180.0)) {
        telescopeRight(FAST);
      } else {
        telescopeLeft(FAST);
      }
    }
    if (i >= MAX_ITERATIONS - 1) {
      return 1;
    }
  }
  return 0;
}

//The HIGH/LOW in these may be backwards... figure that out once the mount is built.
//The only parameter passed to each of these is speed; all run for 200ms
void telescopeDown(int spd) {
  digitalWrite(altPin2, LOW);
  analogWrite(altPin1, spd);
  delay(50);
  analogWrite(altPin1, 0);
}
void telescopeUp(int spd) {
  digitalWrite(altPin1, LOW);
  analogWrite(altPin2, spd);
  delay(50);
  analogWrite(altPin2, 0);
}
void telescopeLeft(int spd) {
  digitalWrite(aziPin1, LOW);
  analogWrite(aziPin2, spd);
  delay(50);
  analogWrite(aziPin2, 0);
}
void telescopeRight(int spd) {
  digitalWrite(aziPin2, LOW);
  analogWrite(aziPin1, spd);
  delay(50);
  analogWrite(aziPin1, 0);
}

//Measures orientation and prints to serial; used to communicate with AutoSky app
void returnORN() {
  sensors_event_t accEvent;
  accel.getEvent(&accEvent);
  //Message reply: "ORN:0.000,0.000,0.000;0.000,0.000,0.000"
  Serial.print("ORN:");
  Serial.print(accEvent.acceleration.x, 8); Serial.print(',');
  Serial.print(accEvent.acceleration.y, 8); Serial.print(',');
  Serial.print(accEvent.acceleration.z, 8); Serial.print(';');
  sensors_event_t magEvent;
  magnet.getEvent(&magEvent);
  Serial.print(magEvent.magnetic.x, 8); Serial.print(',');
  Serial.print(magEvent.magnetic.y, 8); Serial.print(',');
  Serial.println(magEvent.magnetic.z, 8);
}

//Interrupt function stuff
SIGNAL(TIMER0_COMPA_vect) {
  char c = gps.read();
}

//Interrupt function stuff
void useInterrupt(boolean v) {
  if (v) {
    OCR0A = 0xAF;
    TIMSK0 |= _BV(OCIE0A);
    usingInterrupt = true;
  } else {
    TIMSK0 &= ~_BV(OCIE0A);
    usingInterrupt = false;
  }
}

//Measures GPS and prints to serial; used to communicate with AutoSky app
void returnGPS() {
  if (gps.newNMEAreceived()) {
    if (!gps.parse(gps.lastNMEA())) {
      return;
    }
  }
  if (gps.fix) {
    Serial.print("GPS:");
    Serial.print(gps.latitude, 8); Serial.print(',');
    Serial.println(gps.longitude, 8);
  } else {
    Serial.println("GPS:-1,-1");
  }
}

void setup(void) {
  Serial.begin(9600);
  pinMode(altPin1, OUTPUT);
  pinMode(altPin2, OUTPUT);
  pinMode(aziPin1, OUTPUT);
  pinMode(aziPin2, OUTPUT);
  
  if (!magnet.begin()) {
    Serial.println("LSM303 not detected...");
    while(1);
  }
  
  if (!accel.begin()) {
    Serial.println("LSM303 not detected...");
    while(1);
  }
  
  gps.begin(9600);
  gps.sendCommand(PMTK_SET_NMEA_OUTPUT_RMCGGA);
  gps.sendCommand(PMTK_SET_NMEA_UPDATE_1HZ);
  useInterrupt(true);
  
  delay(1000);
}

String input = "";
void loop(void) {
  if (Serial.available()) {
    char inByte = Serial.read();
    if (inByte == ';') {
      if (input.equals("GPS_GET")) {
        returnGPS();
      } else if (input.equals("ORN_GET")) {
        returnORN();
      } else if (input.equals("AAPR")) {
        Serial.println("RPAA");
      } else if (input.startsWith("ORN_SET")) {
        //Input will be ORN_SET:altitude,azimuth;
        //e.g. ORN_SET:47.1454,195.6346;
        //     0123456789012345678901
        int idx = input.indexOf(",");
        
        //Get the altitude
        String altS = input.substring(8, idx-1);
        char buf[altS.length()];
        altS.toCharArray(buf, altS.length());
        float alt = atof(buf);
        
        //Get the azimuth
        String aziS = input.substring(idx+1, input.length()-1);
        buf[aziS.length()];
        aziS.toCharArray(buf, aziS.length());
        float azi = atof(buf);
        
        int ret = pointTelescope(alt, azi);
        Serial.print("MOV,"); Serial.println(ret); //"MOV,0" for fail, "MOV,1" for success
      }
      input = "";
    } else {
      input = input + inByte;
    }
  }
}
