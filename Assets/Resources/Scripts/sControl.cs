using UnityEngine;
using System.Collections;

public class sControl : MonoBehaviour {

	private static sControl instancia;

	public GameObject[] letrasPrefabs;
	public GameObject[] letrasObjects;
	public string cadena;
	public float tiempoLimite;
	public bool finalEspera;

	// Use this for initialization
	void Start (){
		instancia = this;
		letrasObjects = new GameObject[4];
		finalEspera = false;
		tiempoLimite = 1.5f;
		generarCadena();
		StartCoroutine("esperarCadena");
	}

	public IEnumerator esperarCadena(){
		yield return new WaitForSeconds(tiempoLimite);
		finalEspera = true;
	}

	public void generarCadena(){
		int rand;
		cadena = "";
		float posX = -1.5f;
		for(int i=0; i<4; i++){
			rand = Random.Range(0,4);
			
			switch(rand){
				case 0: 
					letrasObjects[i] = Instantiate(letrasPrefabs[0], new Vector3(posX+i*1,0,0), Quaternion.identity) as GameObject; 
					letrasObjects[i].name = "A";
					cadena = cadena + "a";
					break;
				case 1: 
					letrasObjects[i] = Instantiate(letrasPrefabs[1], new Vector3(posX+i*1,0,0), Quaternion.identity) as GameObject; 
					letrasObjects[i].name = "W";
					cadena = cadena + "w";
					break;
				case 2: 
					letrasObjects[i] = Instantiate(letrasPrefabs[2], new Vector3(posX+i*1,0,0), Quaternion.identity) as GameObject; 
					letrasObjects[i].name = "S";
					cadena = cadena + "s";
					break;
				case 3: 
					letrasObjects[i] = Instantiate(letrasPrefabs[3], new Vector3(posX+i*1,0,0), Quaternion.identity) as GameObject; 
					letrasObjects[i].name = "D";
					cadena = cadena + "d";
					break;
			}
		}
	}

	public static sControl getInstancia{
		get{
			return instancia;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void restartRound(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
