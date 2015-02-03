
int altSpeedPin = 10;
int alt1Pin = 8;
int alt2Pin = 9;
int aziSpeedPin = 11;
int azi1Pin = 12;
int azi2Pin = 13;

void telescopeDown(int spd) {
  digitalWrite(alt1Pin, HIGH);
  digitalWrite(alt2Pin, LOW);
  analogWrite(altSpeedPin, spd);
  delay(50);
  analogWrite(altSpeedPin, 0);
}
void telescopeUp(int spd) {
  digitalWrite(alt1Pin, LOW);
  digitalWrite(alt2Pin, HIGH);
  analogWrite(altSpeedPin, spd);
  delay(50);
  analogWrite(altSpeedPin, 0);
}
void telescopeLeft(int spd) {
  digitalWrite(azi1Pin, LOW);
  digitalWrite(azi2Pin, HIGH);
  analogWrite(aziSpeedPin, spd);
  delay(500);
  analogWrite(aziSpeedPin, 0);
}
void telescopeRight(int spd) {
  digitalWrite(azi1Pin, HIGH);
  digitalWrite(azi2Pin, LOW);
  analogWrite(aziSpeedPin, spd);
  delay(500);
  analogWrite(aziSpeedPin, 0);
}

void setup() {
  Serial.begin(9600);
  
  pinMode(altSpeedPin, OUTPUT);
  pinMode(alt1Pin, OUTPUT);
  pinMode(alt2Pin, OUTPUT);
  pinMode(aziSpeedPin, OUTPUT);
  pinMode(azi1Pin, OUTPUT);
  pinMode(azi2Pin, OUTPUT);
}

void loop() {
  for (int i = 0; i < 20; i++) {
    Serial.println("Moving...");
  telescopeUp(255);
  }
  delay(10000000);
}

