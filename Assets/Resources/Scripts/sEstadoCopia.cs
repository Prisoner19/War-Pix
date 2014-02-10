using UnityEngine;
using System.Collections;

public class sEstadoCopia : MonoBehaviour {

	private static sEstadoCopia instancia;
	public bool presionado;
	public int numAciertosPlayer;
	public GameObject correctoPrefab;
	public GameObject wrongPrefab;

	// Use this for initialization
	void Start () {
		instancia = this;
		presionado = false;
		numAciertosPlayer = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[RPC]
	void updateCopy(){
		presionado = !presionado;
	}

	[RPC]
	void updateAciertos(int indice, bool resultado){
		if(resultado){
			Instantiate(correctoPrefab, new Vector3(-1.5f+indice*1,-1,0), Quaternion.identity);
			numAciertosPlayer ++;
		}
		else{
			Instantiate(wrongPrefab, new Vector3(-1.5f+indice*1,-1,0), Quaternion.identity);
		}
	}

	public void enviarInfo(){
		networkView.RPC("updateCopy",RPCMode.Others);
	}

	public void enviarAciertos(int indice, bool resultado){
		networkView.RPC("updateAciertos",RPCMode.Others, indice, resultado);
	}

	public static sEstadoCopia getInstancia{
		get{
			return instancia;
		}
	}

	void OnGUI(){
		if(presionado){
			GUI.Label(new Rect(100,100,300,20), "Presionado ");
		}
	}
}
