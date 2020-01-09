using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TouchScript.Gestures;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class buttonPress : MonoBehaviour {



	private string[] passosTrilha = new string[16];
	private string[] passosFunc = new string[5];
	private int elementoInt;         

	//SendUDP
	private string IP="127.0.0.1";
	private int portOut = 8052;
	IPEndPoint remoteEndPoint;
	UdpClient clientOut = new UdpClient ();

	//ReceiveUDP
	Thread receiveThread;
	public int portIn=8051;
	UdpClient clientIn;


	public void passos(string elemento, string comando, string local){
		
		Debug.Log(elemento+" "+comando+" "+local);

		int.TryParse(elemento , out elementoInt);
	
		if(local=="trilha"){
			passosTrilha[elementoInt-1] = comando;
		}else if(local=="funcao"){
			passosFunc[elementoInt-1] = comando;
		}
		
	}


	public void cleanAll(){
		for(int i=0 ; i<14; i++){
			passosTrilha[i]=null;
		}
		for(int i=0 ; i<4; i++){
			passosFunc[i]=null;
		}
	}




	void Start () {
		init();
		GetComponent<PressGesture>().StateChanged += HandleStateChanged;

	}





	void Update () 
	{




	}


	public void init()
	{
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), portOut);
		print(IP+" : "+portOut);

		receiveThread = new Thread(new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();

	}


	// receive thread
	private  void ReceiveData()
	{
		clientIn = new UdpClient (portIn);
		while (true)
		{
			try
			{
				// Bytes empfangen.
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = clientIn.Receive(ref anyIP);

				// Bytes mit der UTF8-Kodierung in das Textformat kodieren.
				string text = Encoding.UTF8.GetString(data);

				// Den abgerufenen Text anzeigen.
				print(">> " + text);
				if(text=="@"){
					sendNext();
				}

			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}



	// sendData
	public void sendString(string message)
	{
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), portOut);

		try
		{
			byte[] data = Encoding.UTF8.GetBytes(message);
			clientOut.Send(data, data.Length, remoteEndPoint);

		}
		catch (Exception err)
		{
			print(err.ToString());
		}
	}





	/*
		! -> representa representa o final do algoritmo
		@ -> representa o fim da execuçao de um comando pelo arduino
		* -> representa o inicio do envio dos comandos

	*/



	/*

		A função send next é responsável por enviar os comandos um a um até que os vetores da trilha principal e de função tenham sido enviados

	*/

	int pos, sop;

	public void sendNext()
	{

		if (pos > 15) {
			sendString ("!");
		} else if (passosTrilha [pos] != null) {
						
				if (passosTrilha [pos] == "4") {
					if (passosFunc [sop] != null) {
						sendString (passosFunc [sop]);
						Debug.Log ("enviadoFunçao: " + passosFunc [sop]);
						sop++;
					} else {
						sop++;
					}

					if (sop == 4) {
						pos++;
						sop = 0;
					}

				} else {

				if(passosTrilha [pos]=="5"){pos--;}
					sendString (passosTrilha [pos]);
					Debug.Log ("enviadoPrincipal: " + passosTrilha [pos]);
					pos++;
				}

			} else {
				pos++;
				sendNext ();
			}

		



	}


	void HandleStateChanged (object sender, GestureStateChangeEventArgs e)
	{


		if(e.State == Gesture.GestureState.Ended)
		{
			pos =0;
			sop=0;
			sendString ("*");
		}

	}
	

	

}
