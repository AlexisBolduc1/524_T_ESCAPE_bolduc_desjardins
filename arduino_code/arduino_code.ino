#include <M5Atom.h>
#include <FastLED.h>

CRGB mesPixels[1];

#include <M5_PbHub.h>
M5_PbHub myPbHub;

#include <MicroOscSlip.h>
MicroOscSlip<64> myMicroOsc(&Serial);

#define KEY_UNIT_CHANNEL 4
#define ANGLE_UNIT_CHANNEL 3
#define PIR_UNIT_CHANNEL 2
#define LIGHT_UNIT_CHANEL 1

#include "Unit_Encoder.h"
Unit_Encoder myEncoder;
int myEncoderPreviousRotation;

unsigned long monChronoDepart = 0;

void setup() {
  M5.begin(false, false, false);
  Serial.begin(115200);
  FastLED.addLeds<WS2812, DATA_PIN, GRB>(mesPixels, 1);
  myPbHub.begin();
  myPbHub.setPixelCount(KEY_UNIT_CHANNEL, 1);

  while ( millis() < 5000) {
    mesPixels[0] = CHSV( (millis()/5) % 255,255,255-(millis()*255/5000));
    FastLED.show();
    delay(50);
  } 
  
  Wire.begin();
  myEncoder.begin();
  
  mesPixels[0] = CRGB(0,0,0);
  FastLED.show();
}

void myOscMessageParser( MicroOscMessage& receivedOscMessage) {
  if ( receivedOscMessage.checkOscAddress("/pixel") ) {
    int r = receivedOscMessage.nextAsInt();
    int g = receivedOscMessage.nextAsInt();
    int b = receivedOscMessage.nextAsInt();

    mesPixels[0].red = r;
    mesPixels[0].green = g;
    mesPixels[0].blue = b;
    FastLED.show();
  } else if ( receivedOscMessage.checkOscAddress("/red") ) {
    int r = receivedOscMessage.nextAsInt();

    mesPixels[0].red = r;
    FastLED.show();
  } else if ( receivedOscMessage.checkOscAddress("/green") ) {
    int g = receivedOscMessage.nextAsInt();

    mesPixels[0].green = g;
    FastLED.show();
  } else if ( receivedOscMessage.checkOscAddress("/blue") ) {
    int b = receivedOscMessage.nextAsInt();

    mesPixels[0].blue = b;
    FastLED.show();
  }
}

void loop() {
  M5.update();
  myMicroOsc.onOscMessageReceived( myOscMessageParser );

  if (millis() - monChronoDepart >= 50) {  // SI LE TEMPS ÉCOULÉ DÉPASSE 50 MS...
    monChronoDepart = millis();            // ...REDÉMARRER LE CHRONOMÈTRE...

  //bout de code pour le key unit
    //Serial.print("/button ");
    int maValeurKey = myPbHub.digitalRead(KEY_UNIT_CHANNEL);
    //Serial.println(maValeurKey);
    myMicroOsc.sendInt("/button", maValeurKey);

  //light unit
    int maValeurLight = myPbHub.analogRead(LIGHT_UNIT_CHANEL);
    myMicroOsc.sendInt("/photo", maValeurLight);

  //bout de code pour l'encodeur
    int encoderRotation = myEncoder.getEncoderValue();
    int encoderRotationChange = encoderRotation - myEncoderPreviousRotation;
    myEncoderPreviousRotation = encoderRotation;

    int encoderButton = myEncoder.getButtonStatus();
    myMicroOsc.sendInt("/encBut", encoderButton);
    Serial.println(encoderButton);

    uint32_t myColorOn = 0xFFFFFF;
    uint32_t myColorOff = 0x000000;

    if (encoderButton == 0) {
      myEncoder.setLEDColor(2, myColorOn);
      myEncoder.setLEDColor(1, myColorOn);
    } else {
      if ( encoderRotationChange > 0) {
        myEncoder.setLEDColor(1, myColorOn);
        myEncoder.setLEDColor(2, myColorOff);
      } else if (encoderRotationChange < 0) {
        myEncoder.setLEDColor(1, myColorOff);
        myEncoder.setLEDColor(2, myColorOn);
      } else {
        myEncoder.setLEDColor(1, myColorOff);
        myEncoder.setLEDColor(2, myColorOff);
      }
    myMicroOsc.sendInt("/enc", encoderRotationChange);
    }
  }
}
