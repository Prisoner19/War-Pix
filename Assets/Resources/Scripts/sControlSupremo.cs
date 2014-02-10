using UnityEngine;
using System.Collections;

public class sControlSupremo : MonoBehaviour {

	private static sControlSupremo instancia;

	public bool[] resultados;
	public int ronda;
	private int numRondas;
	public int combo;

	// Use this for initialization
	void Start () {
		instancia = this;
		numRondas = 5;
		resultados = new bool[numRondas];
		ronda = 0;
		combo = 0;
		DontDestroyOnLoad(gameObject);
		Debug.Log("ad");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static sControlSupremo getInstancia{
		get{
			return instancia;
		}
	}

	public void ganarRonda(){
		resultados[ronda] = true;
		combo++;
	}

	public void perderCombo(){
		combo = 0;
	}

	public bool verificarFin(){
		if(ronda>=numRondas)
			return false;
		return true;
	}

}
