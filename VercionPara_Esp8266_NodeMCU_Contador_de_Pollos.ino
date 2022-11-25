#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
//#include <SoftwareSerial.h>

//Your Domain name with URL path or IP address with path
String serverName = "http://ayemadeoro.com/postmodulos/contadorpollos.aspx";

//SoftwareSerial mySerial(13, 15); // RX, TX

//DEFINE LAS CREDENCIALES DE LA RED WIFI PARA COMUNICACION

//#define STASSID "OFICINA"
//#define STAPSK  "Ayosa*2020*"
#define STASSID "San Francisco"
#define STAPSK  "MATSAN2022"
//#define STASSID "Familia Espinoza"
//#define STAPSK  "KasaEspinoza2021"

//Indicar un ping para la Informacion de Grabado
const int Indicador_de_Coneccion = 5;

void setup()
{
  //ESTABLECE EL PING EN MODO SALIDA
  pinMode(Indicador_de_Coneccion, OUTPUT);
  //INICIA EL PUERTO SERIAL EN LA VELOCIDAD INDICADA
  Serial.begin(9600);//9600

  //CONECTA CON LA SEÑAL WIFI
  ConectarconWifi();
  //Mandar un primer valor en 0
  WiFiClient client;
  HTTPClient http;
  String serverPath = serverName + "?ConteoPollo=0";
  http.begin(client, serverPath.c_str());
  // Send HTTP GET request
  int httpResponseCode = http.GET();
  if (httpResponseCode > 0) {
    String payload = http.getString();
  }
  //FINALIZA LA CARGA DEL SITIO WEB
  http.end();
  Serial.print("Se envio correctamente un valor 0 a la web");
  Serial.println("");
}
/*
  Para que comunicacion funcione entre el arduino y el Modelo de placa ESP8266 V3.0 NodeMCU
  En la PLaca arduino se debe usar a libreria SoftwareSerial.h, para usar los pines 2 y 3 como Rx y Tx, respectivamente
  se establece Un SoftwareSerial (Rx,Tx) usando los pines lo demas esta en el codigo del archivo VercionParaArduinoUnoFinal-ContadorPollo, lo escencial
  conectar los Pines 2 y 3 a los pines Rx y Tx de la placa 8266 directamente sin usar los pines GPIO, lo demas esta aqui este codigo
*/
void loop()
{
  //Serial.listen();
  if (Serial.available() > 0)
  {
    String TotalPollo = Serial.readString();
    if ((WiFi.status() == WL_CONNECTED)) {

      WiFiClient client;
      HTTPClient http;
      int TotalPollo_int = TotalPollo.toInt();
      //Serial.println(serverName + "?ConteoPollo="+TotalPollo_int);
      String serverPath = serverName + "?ConteoPollo=" + String(TotalPollo_int);
      http.begin(client, serverPath.c_str());

      // Send HTTP GET request
      int httpResponseCode = http.GET();

      if (httpResponseCode > 0) {
        String payload = http.getString();
        // 2 blink, Grabado en el servidor
        digitalWrite(Indicador_de_Coneccion, HIGH);
        delay(50);
        digitalWrite(Indicador_de_Coneccion, LOW);
        delay(50);
        digitalWrite(Indicador_de_Coneccion, HIGH);
        delay(50);
        digitalWrite(Indicador_de_Coneccion, LOW);
        delay(50);
        // Serial.println("Valor Grabado en el Sistema: " + TotalPollo);
        //Serial.println(String(TotalPollo));
        //}
      } else {
        //Serial.print("Error code: ");
        //  Serial.println(httpResponseCode);
      }
      //FINALIZA LA CARGA DEL SITIO WEB
      http.end();
    } else {
      //CONECTA CON LA SEÑAL WIFI
      ConectarconWifi();
      // Serial.println("Error de Coneccion wifi");
    }
    //En Teoria Limpia el Buffer
    serialFlush();
  } else {
    //Serial.println("No hay Datos en el Buffer Serial");
    delay(10);
  }
  delay(10);
}
void serialFlush() {
  while (Serial.available() > 0) {
    char t = Serial.read();
  }
}
void ConectarconWifi() {
  /*
    Intentar Conectarse con la red WIFI
  */
  Serial.println();
  Serial.println();
  Serial.println();
  WiFi.begin(STASSID, STAPSK);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    /*
       Blinking para indicar conectando con el servicio wifi
    */
    digitalWrite(Indicador_de_Coneccion, LOW);
    delay(50);
    digitalWrite(Indicador_de_Coneccion, HIGH);
    delay(50);
    digitalWrite(Indicador_de_Coneccion, LOW);
    delay(50);
    digitalWrite(Indicador_de_Coneccion, HIGH);
    delay(50);
    digitalWrite(Indicador_de_Coneccion, LOW);
    delay(50);
  }
  Serial.print("CONECTADO EXITOSAMENTE! DIRECCION IP ASIGNADA: ");
  Serial.println(WiFi.localIP());
  //INDICADOR DE LUZ PROLONGADO PARA INDICAR CONEXION EXITOZA!!
  digitalWrite(Indicador_de_Coneccion, HIGH);
  delay(2000);
  digitalWrite(Indicador_de_Coneccion, LOW);
}
