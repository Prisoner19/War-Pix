using UnityEngine;
using System.Collections;

public class sPlayer : MonoBehaviour {

	private bool presionado;
	private bool[] aciertos;
	private int numAciertos;
	private char[] cadena;
	private int indice;
	private SpriteRenderer aux;
	private bool banderaRonda;
	private string resultado;
	public Font myFont;

	private Rect rectLabel;

	public GameObject wrongPrefab;
	public GameObject correctoPrefab;

	public sControlSupremo controlSupremo;

	//public int posy;

	// Use this for initialization
	void Start () {
		presionado = false;
		banderaRonda = true;
		resultado = "";
		indice = 0;
		aciertos = new bool[4];
		numAciertos = 0;
		string cad = sControl.getInstancia.cadena;
		cadena = cad.ToCharArray();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			presionado = !presionado;
			sEstadoCopia.getInstancia.enviarInfo();
		}
		if(!sControl.getInstancia.finalEspera)
			verificarCadena();
		else if(banderaRonda == true){
			terminarFaltantes();
			slideLabel();
			StartCoroutine("esperarResultado");
		}
	}

	void OnGUI(){
		GUI.skin.font = myFont;
		if(presionado){
			GUI.Label(new Rect(100,100,300,20), "Presionado "+gameObject.name);
		}
		if(resultado != ""){
			GUI.Label(rectLabel,resultado);
		}
	}

	void terminarFaltantes(){
		banderaRonda = false;
		for(int i=indice; i<4; i++){
			aciertos[indice] = false;
			aux = sControl.getInstancia.letrasObjects[indice].GetComponent("SpriteRenderer") as SpriteRenderer;
			aux.sprite = Resources.Load<Sprite>("Sprites/"+sControl.getInstancia.letrasObjects[indice].name+"M");
			sEstadoCopia.getInstancia.enviarAciertos(indice,false);
			Instantiate(wrongPrefab, new Vector3(-1.5f+indice*1,1,0), Quaternion.identity);
			indice++;
		}
		verificarVictoria();
	}

	void verificarCadena(){

		foreach (char c in Input.inputString) {
			//Debug.Log(c);
			if(indice < 4){
				aux = sControl.getInstancia.letrasObjects[indice].GetComponent("SpriteRenderer") as SpriteRenderer;
				if (c == cadena[indice]){
					aciertos[indice] = true;
					numAciertos ++;
					aux.sprite = Resources.Load<Sprite>("Sprites/"+sControl.getInstancia.letrasObjects[indice].name+"B");
					sEstadoCopia.getInstancia.enviarAciertos(indice,true);
					Instantiate(correctoPrefab, new Vector3(-1.5f+indice*1,1,0), Quaternion.identity);
				}
				else{
					aciertos[indice] = false;
					aux.sprite = Resources.Load<Sprite>("Sprites/"+sControl.getInstancia.letrasObjects[indice].name+"M");
					sEstadoCopia.getInstancia.enviarAciertos(indice,false);
					Instantiate(wrongPrefab, new Vector3(-1.5f+indice*1,1,0), Quaternion.identity);
				}
				Debug.Log(aciertos[indice]);
				indice++;
			}
		}
	}

	public void verificarVictoria(){
		if(numAciertos < sEstadoCopia.getInstancia.numAciertosPlayer){
			resultado = "LOSER";
			sControlSupremo.getInstancia.perderCombo();
		}
		else if(numAciertos == sEstadoCopia.getInstancia.numAciertosPlayer){
			sControlSupremo.getInstancia.perderCombo();
			resultado = "DRAW";
		}
		else{
			sControlSupremo.getInstancia.ganarRonda();
			resultado = "WINNER";
			/*switch(sControlSupremo.getInstancia.combo){
				case 1: resultado = "WINNER"; break;
				case 2: resultado = "WINNER COMBO X2"; break;
				case 3: resultado = "WINNER COMBO X3"; break;
				case 4: resultado = "WINNER COMBO x4"; break;
			}*/
		}
		//Debug.Log("Resultado: "+resultado);
	}

	public IEnumerator esperarResultado(){
		yield return new WaitForSeconds(1);
		sControlSupremo.getInstancia.ronda ++;
		if(sControlSupremo.getInstancia.verificarFin()){
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void slideLabel(){
		rectLabel = new Rect(350f, 130f, 400f, 50f);
        iTween.ValueTo(gameObject, iTween.Hash("from", offScreenLeft(rectLabel), "to", rectLabel, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRect"));
        // Fix iTween's 'from' position if using 'delay', as it seems to hold on the 'to' position until delay is over
        rectLabel = offScreenLeft(rectLabel);
	}

	Rect offScreenLeft ( Rect input ){
        return new Rect(-100f, input.y, input.width, input.height);
	}

	void updateRect ( Rect input ){
        rectLabel = input;
	}
}