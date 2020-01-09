/*
 * 31 mar 2015
 * This sketch display UDP packets coming from an UDP client.
 * On a Mac the NC command can be used to send UDP. (nc -u 192.168.1.101 2390). 
 *
 * Configuration : Enter the ssid and password of your Wifi AP. Enter the port number your server is listening on.
 *
 */

#include <ESP8266WiFi.h>
#include <WiFiUDP.h>
#include <WiFiClient.h> 

extern "C" {  //required for read Vdd Voltage
#include "user_interface.h"
  // uint16 readvdd33(void);
}

int status = WL_IDLE_STATUS;
const char* ssid = "junin";  //  your network SSID (name)
const char* pass = "";       // your network password

unsigned int localPort = 12345;      // local port to listen for UDP packets

byte packetBuffer[1]; //buffer to hold incoming and outgoing packets

int comando;

// A UDP instance to let us send and receive packets over UDP
WiFiUDP Udp;

void setup()
{
  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  Serial.print("Configuring access point...");
  /* You can remove the password parameter if you want the AP to be open. */
  WiFi.softAP(ssid, pass);

  IPAddress myIP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(myIP);



printWifiStatus();

  Serial.println("Connected to wifi");
  Serial.print("Udp server started at port ");
  Serial.println(localPort);
  Udp.begin(localPort);
}

void loop()
{
  int noBytes = Udp.parsePacket();
  String received_command = "";
 
  if ( noBytes ) {
     Udp.read(packetBuffer,noBytes);
 /*
    // We've received a packet, read the data from it
    // read the packet into the buffer

    // display the packet contents in HEX
    for (int i=1;i<=noBytes;i++)
    {
      received_command = received_command + char(packetBuffer[i - 1]);
    }

    */
    Udp.beginPacket(Udp.remoteIP(), 8051);
    
    Serial.println(packetBuffer[0]);
    Udp.write("@");
    Udp.endPacket();

    
    
  }


}

void printWifiStatus() {
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your WiFi shield's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);
}
