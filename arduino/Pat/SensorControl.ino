#include <SesonControl.h>
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_LSM303_U.h>

Adafruit_LSM303_Mag_Unified magnet = Adafruit_LSM303_Mag_Unified(11111);
Adafruit_LSM303_Accel_Unified accel = Adafruit_LSM303_Accel_Unified(11112);

void displaySensorDetails(void)
{
  sensor_t sensor;
  accel.getSensor(&sensor);
  Serial.println("------------------------------------");
  Serial.print  ("Sensor:       "); Serial.println(sensor.name);
  Serial.print  ("Driver Ver:   "); Serial.println(sensor.version);
  Serial.print  ("Unique ID:    "); Serial.println(sensor.sensor_id);
  Serial.print  ("Max Value:    "); Serial.print(sensor.max_value); Serial.println(" m/s^2");
  Serial.print  ("Min Value:    "); Serial.print(sensor.min_value); Serial.println(" m/s^2");
  Serial.print  ("Resolution:   "); Serial.print(sensor.resolution); Serial.println(" m/s^2");  
  Serial.println("------------------------------------");
  Serial.println("");
}

int SetupSensors()
{
  Serial.println("Magnemeter Test"); Serial.println("");  
  if (!magnet.begin()) {
    Serial.println("LSM303 not detected...");
    return -1;
  }

  Serial.println("Accelerometer Test"); Serial.println("");  
  if (!accel.begin()) {
    Serial.println("LSM303 not detected...");
    return -1;
  }
   displaySensorDetails(); 
   return 0;
  
}

void GetSensorData() 
{
  /* Get a new sensor event */ 
  sensors_event_t event; 
  Serial.println("Getting Sensor Event");
  accel.getEvent(&event);
 
  /* Display the results (acceleration is measured in m/s^2) */
  Serial.print("X: "); Serial.print(event.acceleration.x); Serial.print("  ");
  Serial.print("Y: "); Serial.print(event.acceleration.y); Serial.print("  ");
  Serial.print("Z: "); Serial.print(event.acceleration.z); Serial.print("  ");Serial.println("m/s^2 ");
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
