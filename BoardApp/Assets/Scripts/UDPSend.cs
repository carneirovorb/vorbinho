using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{

	// prefs
	private string IP="192.168.4.1";  // define in init
	private int port = 12345;  // define in init

	// "connection" things
	IPEndPoint remoteEndPoint;
	UdpClient client;



	// call it from shell (as program)
	private static void Main()
	{
		UDPSend sendObj = new UDPSend();
		sendObj.init();

		// testing via console
		// sendObj.inputFromConsole();

		// as server sending endless
		sendObj.sendEndless(" endless infos \n");

	}
	// start from unity3d
	public void Start()
	{
		init();
	}


	// init
	public void init()
	{

		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
		client = new UdpClient();

		// status
		print(IP+" : "+port);



	}


	// sendData
	public void sendString(string message)
	{
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
		client = new UdpClient();

		try
		{
			byte[] data = Encoding.UTF8.GetBytes(message);
			client.Send(data, data.Length, remoteEndPoint);

		}
		catch (Exception err)
		{
			print(err.ToString());
		}
	}


	// endless test
	private void sendEndless(string testStr)
	{
		do
		{
			sendString(testStr);


		}
		while(true);

	}

}

