using UnityEngine;
using System.Collections;

public class sCreador : MonoBehaviour {

	public GameObject p1Prefab;
	public GameObject copiaP1Prefab;
	public Transform spawnPoint1;
	public GameObject p2Prefab;
	public GameObject copiaP2Prefab;
	public Transform spawnPoint2;
	public GameObject controlSupremoPrefab;


	// Use this for initialization
	void Start () {

		GameObject p1;
		GameObject p2;
		GameObject controlSupremo;

		if(GameObject.Find("Control Supremo") == null){
			controlSupremo = Instantiate(controlSupremoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			controlSupremo.name = "Control Supremo";
		}

		if(Network.peerType == NetworkPeerType.Server){
			p1 = Instantiate(p1Prefab, spawnPoint1.position, Quaternion.identity) as GameObject;
			p2 = Instantiate(copiaP2Prefab, spawnPoint2.position, Quaternion.identity) as GameObject;
		}
		else{
			p1 = Instantiate(copiaP1Prefab, spawnPoint2.position, Quaternion.identity) as GameObject;
			p2 = Instantiate(p2Prefab, spawnPoint1.position, Quaternion.identity) as GameObject;
		}

		p1.name = "Player1";
		p2.name = "Player2";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
