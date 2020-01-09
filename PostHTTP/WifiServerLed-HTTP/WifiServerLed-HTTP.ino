#include <ESP8266WiFi.h>
#include <ESP8266mDNS.h>
#include <WiFiClient.h>

const char* ssid = "dlink";
const char* password = "";
const int led=5;

WiFiServer server(80);

void setup() {
  Serial.begin(115200);
  delay(10);

  WiFi.mode(WIFI_STA);
  
  // prepare GPIO2
  pinMode(led, OUTPUT); digitalWrite(led, 0);
  
  // Connect to WiFi network
  Serial.println("");Serial.println("");Serial.print("Connecting to ");Serial.println(ssid);
  
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {delay(1000);Serial.print(".");}
  
  Serial.println("");
  Serial.println("WiFi connected");
  
  // Start the server
  server.begin();
  Serial.println("Server started");
  Serial.println(WiFi.localIP());
  
}

void loop() {
  
  WiFiClient client = server.available();
  if (!client) {return;}
  
  //Serial.println("new client");
  while(!client.available()){delay(1);}

    String tip = client.readStringUntil(' ');
    String req = client.readString();
    Serial.println(req);

    if(tip.equals("POST")){
      if      (req.charAt(req.length()-1)=='D'){
        digitalWrite(led, 0);
        Serial.println("off");
        client.write("HTTP/1.1 200 OK");
        client.stop();
      }else if (req.charAt(req.length()-1)=='L'){
        digitalWrite(led, 1);
        Serial.println("on");
        client.write("HTTP/1.1 200 OK");
      }else {
        Serial.println("invalid request");
        client.stop();
        Serial.println("Client disonnected");
      }
    }
    
}
