int aziPin1 = 10;
int aziPin2 = 11;
int altPin1 = 5;
int altPin2 = 6;

void setup()
{
  Serial.begin(9600);
  pinMode(aziPin1, OUTPUT);
  pinMode(aziPin2, OUTPUT);
  pinMode(altPin1, OUTPUT);
  pinMode(altPin2, OUTPUT);
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, LOW);
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, LOW);
}

void loop()
{
  Serial.write("Alt down\n");
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, HIGH);
  delay(5000);
  Serial.write("Stop\n");
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, LOW);
  delay(2000);
  Serial.write("Alt up\n");
  digitalWrite(altPin1, HIGH);
  digitalWrite(altPin2, LOW);
  delay(5000);
  Serial.write("Stop\n");
  digitalWrite(altPin1, LOW);
  digitalWrite(altPin2, LOW);
  delay(2000);
  Serial.write("Azi CCW\n");
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, HIGH);
  delay(5000);
  Serial.write("Stop\n");
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, LOW);
  delay(2000);
  Serial.write("Azi CW\n");
  digitalWrite(aziPin1, HIGH);
  digitalWrite(aziPin2, LOW);
  delay(5000);
  Serial.write("Stop\n");
  digitalWrite(aziPin1, LOW);
  digitalWrite(aziPin2, LOW);
  delay(1000);
}
