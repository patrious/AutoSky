int rightEnablePin = 7;
int leftEnablePin = 4;

void setup() {
  Serial.begin(9600);
  pinMode(rightEnablePin, OUTPUT);
  pinMode(leftEnablePin, OUTPUT);
}

void loop() {
  digitalWrite(leftEnablePin, LOW);
  digitalWrite(rightEnablePin, HIGH);
  delay(500);
  digitalWrite(rightEnablePin, LOW);
  digitalWrite(leftEnablePin, HIGH);
  delay(500);
}
