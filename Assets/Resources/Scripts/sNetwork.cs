using UnityEngine;
using System.Collections;

public class sNetwork : MonoBehaviour {
	
	public string IP = "";
	public int port = 25001;
	private bool quiereCliente;	

	// Use this for initialization
	void OnGUI(){
		if(Network.peerType == NetworkPeerType.Disconnected){

			if(GUI.Button(new Rect(100, 150, 200,25), "Conectar a server")){
				quiereCliente = true;
			}

			if(quiereCliente == false){
				if(GUI.Button(new Rect(100, 200, 200,25), "Crear Server")){
					Network.InitializeServer(2,port);
				}
			}

			if(quiereCliente){
				GUI.Label(new Rect(400, 110, 200,25), "Ingresa IP del server");
				IP = GUI.TextField(new Rect(400, 150, 200, 25), IP);
				if(GUI.Button(new Rect(450, 190, 100,25), "Conectar")){
					Network.Connect(IP, port);
				}
				if(GUI.Button(new Rect(150, 190, 100,25), "Atras")){
					quiereCliente = false;
				}
			}
		}
		else{
			if(Network.peerType == NetworkPeerType.Client){
				GUI.Label(new Rect(100, 100, 200,25), "Conectado a server");
				Application.LoadLevel("1Level");
				if(GUI.Button(new Rect(100, 200, 200,25), "Desconectar")){
					Network.Disconnect(250);
				}
			}
			else if(Network.peerType == NetworkPeerType.Server){
				GUI.Label(new Rect(100, 100, 400,25), "Tu IP es "+Network.player.ipAddress);
				GUI.Label(new Rect(100, 150, 200,25), "Conexiones: "+Network.connections.Length);
				if(GUI.Button(new Rect(100, 200, 200,25), "Desconectar")){
					Network.Disconnect(250);
				}
				if(Network.connections.Length > 0){
					Application.LoadLevel("1Level");
				}
			}
		}
	}
}
