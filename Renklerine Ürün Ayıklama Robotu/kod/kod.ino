#include <Wire.h>
#include <Servo.h>             //Gerekli kütüphane tanımları
#include "Adafruit_TCS34725.h"
 
Servo ustServo;  
Servo altServo;
int renk = 0;                  //değişkenler tanımlandı
int veri, durum = false;
 
Adafruit_TCS34725 sensor = Adafruit_TCS34725(TCS34725_INTEGRATIONTIME_50MS, TCS34725_GAIN_1X); //renk sensörü tanımlandı
void setup() {
Serial.begin(9600);
 
if (sensor.begin()) {  //sensör kontrolü
} else {
 
while (1); // Dur!
}
 
ustServo.attach(9);   //üst serbo 9.pin ayarlandı
altServo.attach(8);   //alt sevo 8.pin ayarlandı
 
ustServo.write(125);  //üst servo 125 derece ayarlandı
delay(10);
}
void loop() {
 
veri = Serial.read();   //ardunio dan veri okundu
delay(10);
if ( veri == '1')     //arayüzden calıstır butonu ile 8 değeri atanıyor
{
durum = true;
 
}
 
if ( veri =='0')    
{
durum = false;        //arayüzden durdur butonu ile 9 değeri atanıyor
}
 
if ( durum == true)
{
makine_baslat();   //durum doğru olursa makine başlat fonksiyonu çağrılır.
}
if ( durum == false)
{
makine_durdur();    //durum yanlıs olursa makine durdur fonksiyonu cağrılır.
}
}
 
void makine_baslat()
{
delay(500);
for(int i = 125; i > 68; i--) {      
ustServo.write(i);                 //üst servo sensöre göre ayarlandı
delay(5);
}
delay(500);
 
renk = renk_oku();               //renk fonksiyonu çağrıldı
delay(50);
switch (renk) {
case 1:
altServo.write(10);
 
Serial.println(1);
break;
case 2:
altServo.write(25);
Serial.println(2);
break;
case 3:
altServo.write(50);
Serial.println(3);
break;
case 4:                      // RENK BELİRLENDİ ALT SERVO DERECESİ AYARLANDI
altServo.write(70);
Serial.println(4);
break;
case 5:
altServo.write(93);
Serial.println(5);
break;
case 6:
altServo.write(120);
Serial.println(6);
break;
default:
Serial.println(0);
break;
}
delay(300);
 
 
for(int i = 68; i > 25; i--) {
ustServo.write(i);                   //  ürün alt servoya aktarıldı.
delay(5);
}
delay(200);
 
for(int i = 25; i < 125; i++) {      // üst başlangıç derecesine döndü.
ustServo.write(i);
delay(5);
}
renk=0;
}
 
void makine_durdur()
{
ustServo.write(ustServo.read());    // arayüzden durdur butonuna basıldı 
altServo.write(altServo.read());    // son dönen açıda bekle
}
 
// renk okuma fonksiyonu
int renk_oku() {                         // renk okuma fonksiyonu
uint16_t clearcol, red, green, blue;      // Renk değerleri için tanımlamalar
float average, r, g, b;                    // renk hesaplamaları için tanımlamalar
sensor.getRawData(&red, &green, &blue, &clearcol);
 
average = (red+green+blue)/3;         //Renk degerlerini topla ve ortalamasını al.
r = red/average;
g = green/average;                    //renkleri ortalamaya böl  
b = blue/average;
 
delay(50);

if((r < 1.5)&& (r > 1.35)) /*&& ((g > 0.74) && (g < 0.85))&& ((b < 0.9)&& (b > 0.7)))*/{
renk = 1;
 
}
if((r < 1.34)&& (r > 1.15))/* && ((g > 1.24) && (g < 1.34))&&((b < 0.52&& (b > 0.42))))*/{
renk = 2;
 
}
if(((r < 1)&& (r > 0.70)) && ((g > 1.40) && (g < 2))/*&& ((b < 1)&& (b > 0.54))*/){
renk = 3;
 
}
if(((r < 1.05)&& (r > 0.40)) &&/*((g > 1.24) && (g < 1.39))&& */((b < 2.24)&& (b > 1.14))){        //değerlere göre renk döndürüyor
renk = 4;
 
}
if(((r < 2.2)&& (r > 1.8))/*&& ((g > 0.5) && (g < 0.65))&& ((b < 0.55)&& (b > 0.4))*/){
renk = 5;
 
}
if(((r < 1.79)&& (r > 1.51))&& ((g > 0.76) && (g < 1.2))/*&&((b < 0.56&& (b > 0.45)))*/){
renk = 6;
 
}
return renk;
}
