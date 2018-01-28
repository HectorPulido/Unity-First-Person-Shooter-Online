using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Instanciador : NetworkBehaviour 
{
	public GameObject ObjetoAInstanciar;
	public float Periodo = 60;


	// Use this for initialization
	IEnumerator Start () 
	{
		while(true)
		{			
			Instanciar ();
			yield return new WaitForSeconds (Periodo);		
		}		
	}
	
	// Update is called once per frame
	void Instanciar () 
	{
		if (!isServer)
			return;
		GameObject go = Instantiate (ObjetoAInstanciar, transform.position, Quaternion.identity) as GameObject;
		NetworkServer.Spawn (go);
	}
}
