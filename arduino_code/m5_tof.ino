// Le code de base pour le M5Stack Atom

// Inclure la librairie M5 (version pour M5Atom) :
// https://github.com/m5stack/M5Atom
#include <M5Atom.h>

// Inclure la librairie FastLED qui va gérer le pixel :
// https://github.com/FastLED/FastLED
#include <FastLED.h>

// Un tableau qui contient une variable de type CRGB.
// Il y a un seul pixel, mais il doit être dans un tableau.
// CRGB est un type de couleur défini par la lirairie FastLed :
// https://github.com/FastLED/FastLED/wiki/Pixel-reference#crgb-reference
CRGB mesPixels[1];


#include <VL53L0X.h>
VL53L0X myTOF;

unsigned long myChronoStart;

void setup() {
  // Démarrer la libraire M5 avec toutes les options de pré-configuration désactivées :
  M5.begin(false, false, false);

  // Démarrer la connexion sérielle :
  Serial.begin(115200);

  // Ajouter le pixel (il y en a un seul) du M5Atom à la librairie FastLED :
  FastLED.addLeds<WS2812, DATA_PIN, GRB>(mesPixels, 1);


  Wire.begin();

  myTOF.init();
  myTOF.setTimeout(500);
  myTOF.startContinuous();

  // Animation de démarrage
  while (millis() < 5000) {
    mesPixels[0] = CHSV((millis() / 5) % 255, 255, 255 - (millis() * 255 / 5000));
    FastLED.show();
    delay(50);
  }
  mesPixels[0] = CRGB(0, 0, 0);
  FastLED.show();
}

void loop() {
  // Toujours inclure M5.update() au début de loop() :
  M5.update();

  if (millis() - myChronoStart >= 50) {
    myChronoStart = millis();

    uint16_t value = myTOF.readRangeContinuousMillimeters();
    if (myTOF.timeoutOccurred()) {
      Serial.println("TOF TIMEOUT");
    } else {
      Serial.println(value);
    }
  }
}
