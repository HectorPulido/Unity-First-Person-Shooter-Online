using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Botiquin : NetworkBehaviour 
{
	public int VidaACurar = 40;
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Player")) {
			ApagarScripts ap = col.GetComponent<ApagarScripts> ();
			ap.Vida += 20;
			if (ap.Vida >= 100)
				ap.Vida = 100;
			if(ap.Vida_txt != null)
				ap.Vida_txt.text = "Vida: " + ap.Vida.ToString();
			NetworkServer.Destroy (gameObject);
					
		}

	}

}
