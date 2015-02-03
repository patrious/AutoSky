#include <SoftwareSerial.h>
#include <Wire.h>
#include <String.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_GPS.h>
#include <Math.h>


String input = "";
enum STATE {
  ready,
  information,
  error,
  moving,
  moving_ready,
  start
};

STATE currentState = ready;

const int aziPin1 = 10;
const int aziPin2 = 11;
const int altPin1 = 5;
const int altPin2 = 6;

const int MAX_ITERATIONS = 500;

SoftwareSerial gpsSerial(4, 3); //Digits are (TX, RX)
Adafruit_GPS gps(&gpsSerial);
Adafruit_LSM303_Mag_Unified magnet = Adafruit_LSM303_Mag_Unified(11111);
Adafruit_LSM303_Accel_Unified accel = Adafruit_LSM303_Accel_Unified(11112);

void setup()
{
  currentState = ready;
  Serial.begin(9600);
  SetupMotorPins();
  StopMotors();  
  if (SetupSensors() == -1) 
  {
    //error, send to auto sky?
    currentState = error;
  }    
  if (SetupGps() == -1)
  {
    //error, send to autosky    
    currentState = error;
  }
  Serial.println("Done Setup");
}

void loop()
{
  switch(currentState)
  {
  case ready:
    //waiting for a command.
    WaitAndReadCommand();      
    break;
  case information:
    //Read info from sensors. Send back to App    
    if(input.equals("GPS_GET")){
      returnGPS();
    } 
    else if (input.equals("ORN_GET")){
      returnORN();
    } 
    else if (input.equals("SAT_GET"))
    {
      Serial.println(currentState);
      sensors_event_t magEvent;
      magnet.getEvent(&magEvent);
      // Calculate the angle of the vector y,x
      float currentHeading = (atan2(magEvent.magnetic.y, magEvent.magnetic.x) * 180.0) / 3.1415;
      // Normalize to 0-360
      if(currentHeading < 0)
      {      
        currentHeading += 360;
      }
      Serial.print("CurrentHeading: ");
      Serial.println(currentHeading); 
    }    
    SetReady();
    break;
  case error:
    StopMotors();
    Serial.println("Error State");
    //halt all motors, send error to AS
    break;
  case moving:
    //Input will be ORN_SET:altitude,azimuth;
    //e.g. ORN_SET:47.1454,195.6346;
    //     0123456789012345678901       
    float alt;
    float azi;
    int ret;
    alt = ParseAlt(input);
    azi = ParseAzi(input);
    ret = pointTelescope(alt, azi); //move to designated location
    Serial.print("MOV,"); 
    Serial.println(ret); //"MOV,0" for fail, "MOV,1" for success
    SetReady();
    break;
  case moving_ready:  
    int direc;
    direc = ParseDirection(input);
    MoveDirection(direc);
    SetReady();    
    break;    
  case start:
    //set sensors up
    setup();
    SetReady();
    break;
  }
}

int ParseDirection(String data)
{
  int idx = data.indexOf(":");        
  //Get the altitude
  String altS = data.substring(idx+1,idx+2);
  return altS.toInt();
}

void MoveDirection(int direc)
{

  if(direc == 1)    {
    LeftRightStop();      
  }
  else if(direc == 2)    {
    UpDownStop();
  }
  else if (direc == 3)    {
    telescopeUp_C();
  }
  else if (direc == 4)    {
    telescopeDown_C();
  }
  else if (direc == 5)    {
    telescopeLeft_C();
  }
  else if (direc == 6)    {
    telescopeLeft_C_S();
  }
  else if (direc == 7)    {
    telescopeRight_C();
  }
  else if (direc == 8)    {
    telescopeRight_C_S();
  }
  else if (direc == 0){
    StopMotors();
  }
}

void displaySensorDetails()
{
  sensor_t sensor;
  accel.getSensor(&sensor);
  Serial.println("------------------------------------");
  Serial.print  ("Sensor:       "); 
  Serial.println(sensor.name);
  Serial.print  ("Driver Ver:   "); 
  Serial.println(sensor.version);
  Serial.print  ("Unique ID:    "); 
  Serial.println(sensor.sensor_id);
  Serial.print  ("Max Value:    "); 
  Serial.print(sensor.max_value); 
  Serial.println(" m/s^2");
  Serial.print  ("Min Value:    "); 
  Serial.print(sensor.min_value); 
  Serial.println(" m/s^2");
  Serial.print  ("Resolution:   "); 
  Serial.print(sensor.resolution); 
  Serial.println(" m/s^2");  
  Serial.println("------------------------------------");
  Serial.println("");
}

int SetupSensors()
{
  Serial.println("Magnemeter Test"); 
  Serial.println("");  
  if (!magnet.begin()) {
    Serial.println("LSM303 not detected...");
    return -1;
  }

  Serial.println("Accelerometer Test"); 
  Serial.println("");  
  if (!accel.begin()) {
    Serial.println("LSM303 not detected...");
    return -1;
  }
  //displaySensorDetails(); 
  return 0;

}

//Measures orientation and prints to serial; used to communicate with AutoSky app
void returnORN() {
  sensors_event_t accEvent;
  accel.getEvent(&accEvent);
  //Message reply: "ORN:0.000,0.000,0.000;0.000,0.000,0.000"
  Serial.print("ORN:");
  Serial.print(accEvent.acceleration.x, 8); 
  Serial.print(',');
  Serial.print(accEvent.acceleration.y, 8); 
  Serial.print(',');
  Serial.print(accEvent.acceleration.z, 8); 
  Serial.print(';');
  sensors_event_t magEvent;
  magnet.getEvent(&magEvent);
  Serial.print(magEvent.magnetic.x, 8); 
  Serial.print(',');
  Serial.print(magEvent.magnetic.y, 8); 
  Serial.print(',');
  Serial.println(magEvent.magnetic.z, 8);
}

void SetupMotorPins()
{
  pinMode(aziPin1, OUTPUT);
  pinMode(aziPin2, OUTPUT);
  pinMode(altPin1, OUTPUT);
  pinMode(altPin2, OUTPUT);
}

void StopMotors()
{
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, LOW);
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, LOW);
}

void telescopeUp() {
  digitalWrite(altPin2, LOW);
  digitalWrite(altPin1, HIGH);
  delay(150);
  digitalWrite(altPin1, LOW);
}
void telescopeDown() {
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, HIGH);
  delay(150);
  digitalWrite(altPin2, LOW);
}
void telescopeLeft() {
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, HIGH);
  delay(150);
  digitalWrite(aziPin2, LOW);
}
void telescopeRight() {
  digitalWrite(aziPin2, LOW);
  digitalWrite(aziPin1, HIGH);
  delay(150);
  digitalWrite(aziPin1, LOW);
}

void UpDownStop()
{
  digitalWrite(altPin2, LOW);
  digitalWrite(altPin1, LOW);
}
void LeftRightStop()
{
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, LOW);
}
void telescopeUp_C() {
  digitalWrite(altPin2, LOW);
  digitalWrite(altPin1, HIGH);
}
void telescopeDown_C() {
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, HIGH);
}
void telescopeLeft_C() {
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, HIGH);
}
void telescopeRight_C() {
  digitalWrite(aziPin2, LOW);
  digitalWrite(aziPin1, HIGH);
}
void telescopeUp_C_S() {
  digitalWrite(altPin2, LOW);
  analogWrite(altPin1, 200);
}
void telescopeDown_C_S() {
  digitalWrite(altPin1, LOW);
  analogWrite(altPin2, 200);
}
void telescopeLeft_C_S() {
  digitalWrite(aziPin1, LOW);
  analogWrite(aziPin2, 200);
}
void telescopeRight_C_S() {
  digitalWrite(aziPin2, LOW);
  analogWrite(aziPin1, 200);
}

//Points the telescope to the given altitude (using 3axis) and azimuth (using magnetometer)
int pointTelescope(float altitude, float azimuth) {
  //First, set the altitude using the 3axis data
  //Y arrow points towards front of telescope. Z arrow points straight up. (X axis not used.)
  sensors_event_t accEvent;
  sensors_event_t magEvent;
  int i;
  boolean aziComplete = false;
  boolean altComplete = false;
  for (i = 0; i < MAX_ITERATIONS; i++) {
    accel.getEvent(&accEvent);
    double currentAlt = -1*(atan2(accEvent.acceleration.y, accEvent.acceleration.z) * 180.0) / 3.1415;

    magnet.getEvent(&magEvent);
    // Calculate the angle of the vector y,x
    float currentHeading = (atan2(magEvent.magnetic.y, magEvent.magnetic.x) * 180.0) / 3.1415;
    // Normalize to 0-360
    if(currentHeading < 0)
    {      
      currentHeading += 360;
    }
    Serial.print("Azimuth: ");
    Serial.println(azimuth);  
    Serial.print("CurrentHeading: ");
    Serial.println(currentHeading); 
    //Too close to adjust    
    if (abs(altitude - currentAlt) < 5){
      if (abs(altitude - currentAlt) < 0.5){
        UpDownStop();
        altComplete = true;
      }
      else
      {
        altComplete = false;
        if(altitude > currentAlt) {
          telescopeUp_C();
        } 
        else {
          telescopeDown_C();
        }
      }
    } 
    else{
      altComplete = false;
      if(altitude > currentAlt) {
        telescopeUp_C();
      } 
      else {
        telescopeDown_C();
      }
    }
    //Too close to adjust
    if (abs(azimuth - currentHeading) < 40) { 
      if (abs(azimuth - currentHeading) < 10) { 
        LeftRightStop();
        aziComplete = true;
      }   
      else
      {
        aziComplete = false;
        float a = azimuth - currentHeading;
        float b = currentHeading - azimuth;
        if (a < 0) {
          a = a + 360;
        }
        if (b < 0) {
          b = b + 360;
        }
        if(a < b){
          telescopeRight_C_S();
        }
        else
        {
          telescopeLeft_C_S();
        }
      }

    }
    else
    {   
      aziComplete = false;
      float a = azimuth - currentHeading;
      float b = currentHeading - azimuth;

      if(a < b){
        telescopeRight_C();
      }
      else
      {
        telescopeLeft_C();
      }
    }
    if(aziComplete && altComplete)
    {
      StopMotors();
      break;
    }
  }      
  StopMotors();
  StopMotors();
  if (i >= MAX_ITERATIONS - 1) { 
    return 1; 
  }  
  return 0;
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
  } 
  else {
    TIMSK0 &= ~_BV(OCIE0A);
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
    Serial.print(gps.latitude, 8); 
    Serial.print(',');
    Serial.println(gps.longitude, 8);
  } 
  else {
    Serial.println("GPS:-1,-1");
  }
}

int SetupGps()
{
  gps.begin(9600);
  gps.sendCommand(PMTK_SET_NMEA_OUTPUT_RMCGGA);
  gps.sendCommand(PMTK_SET_NMEA_UPDATE_1HZ);
  useInterrupt(true);
  return 0;
}

void WaitAndReadCommand()
{
  if (Serial.available()) {
    char inByte = Serial.read();
    if (inByte != ';') {
      input = input + inByte;
    }
    else
    {
      if (input.endsWith("_GET")) {
        currentState = information;        
      } 
      else if (input.equals("AAPR")) {
        Serial.println("RPAA");
        SetReady();
      } 
      else if (input.startsWith("ORN_SET")) {
        currentState = moving;
      } 
      else if (input.startsWith("DIR_SET")) {
        currentState = moving_ready;
      }
      else if (input.startsWith("DIR_STOP")){
        StopMotors();
        SetReady();
      }
      else if (input.equals("restart")){
        currentState = start;
      }
      else
      {
        input = "";
      }
    }
  }
}

void SetReady()
{
  input = "";
  currentState = ready;
}

int count;
int checkTimeout()
{
  if (count < 500 || checkConnection == 0)  {
    return 0; 
  }  
  currentState = error;
  return -1;
}

int checkConnection()
{
  return -1;
}

float ParseAlt(String data)
{
  int idx = data.indexOf(",");        
  //Get the altitude
  String altS = data.substring(8, idx-1);
  char buf[altS.length()];
  altS.toCharArray(buf, altS.length());
  float alt = atof(buf);
  return alt;
}

float ParseAzi(String data)
{
  int idx = data.indexOf(",");
  //Get the azimuth
  String aziS = data.substring(idx+1, data.length()-1);
  char buf[aziS.length()];
  aziS.toCharArray(buf, aziS.length());
  float azi = atof(buf);
  return azi;
}


























