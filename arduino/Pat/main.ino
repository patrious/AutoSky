#include <MotorControl.h>
#include <GpsControl.h>
#include <SoftwareSerial.h>
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_GPS.h>
#include <Math.h>

enum STATE
{
    ready,
    information,
    error,
    moving,
    moving_ready,
    start
} currentState;

void setup()
{
  currentState = start;
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
}

String input = "";
void WaitAndReadCommand()
{
  if (Serial.available()) {
    char inByte = Serial.read();
    if (inByte != ';') {
      input = input + inByte;
    }
    else
    {
      if (input.find("_GET") != -1) {
        currentState = information;        
      } else if (input.equals("AAPR")) {
        Serial.println("RPAA");
      } else if (input.startsWith("ORN_SET")) {
        currentState = moving;
      } else if (input.startsWith("DIR_SET")) {
        currentState = moving_ready;
      } else if (input.equals("restart")){
        currentState = start;
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
  if (count < 500 || checkConnection == 0)  {return 0; }  
  currentState = error;
  return -1;}

int checkConnection()
{
  return -1;
}

float ParseAlt(string data)
{
  int idx = data.indexOf(",");        
  //Get the altitude
  String altS = data.substring(8, idx-1);
  char buf[altS.length()];
  altS.toCharArray(buf, altS.length());
  float alt = atof(buf);
  return alt;}

float ParseAzi(string data)
{
  int idx = input.indexOf(",");
  //Get the azimuth
  String aziS = input.substring(idx+1, input.length()-1);
  buf[aziS.length()];
  aziS.toCharArray(buf, aziS.length());
  float azi = atof(buf);
  return azi;}


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
      switch(input)
      {
        case input.equals("GPS_GET"):
        returnGPS();
        break;
        case input.equals("ORN_GET"):
        returnORN();
        case input.equals("SAT_GET")
        Serial.println(currentState);
        break;
      }
      SetReady();
      break;
    case error:
      StopMotors();
      //halt all motors, send error to AS
      break;
    case moving:
      //Input will be ORN_SET:altitude,azimuth;
      //e.g. ORN_SET:47.1454,195.6346;
      //     0123456789012345678901       
      float alt = ParseAlt(input);
      float azi = ParseAzi(input);
      int ret = pointTelescope(alt, azi); //move to designated location
      Serial.print("MOV,"); Serial.println(ret); //"MOV,0" for fail, "MOV,1" for success
      SetReady();
      break;
    case moving_ready:
      //set pins to input, wait for next command.
      //Check if we have been running too long, ask system if problem exists.
      //Continue if okay, Error otherwise.

      if (checkTimeout() == -1) {break; }
      input = "";
      WaitAndReadCommand();      
      break;    
    case start:
      //set sensors up
      setup();
      SetReady();
      break;
  }
}



}
