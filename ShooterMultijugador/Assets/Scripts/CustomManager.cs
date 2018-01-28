using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class CustomManager : NetworkManager {
	public Button conectar;
	public Button iniciarHost;

	public InputField ip;
	public InputField puerto;

	public GameObject CanvasOffline;
	public GameObject CanvasOnline;

	void Start () 
	{
		CanvasOnline.SetActive (false);


		conectar.onClick.RemoveAllListeners ();
		iniciarHost.onClick.RemoveAllListeners ();

		conectar.onClick.AddListener (IniciarCliente);
		iniciarHost.onClick.AddListener(IniciarServidor);

	}

	bool SetPort()
	{
		if (puerto.text != "") {
			int _puerto;
			bool b = int.TryParse (puerto.text, out _puerto);
			NetworkManager.singleton.networkPort = _puerto;
			return b;
		} else {
			NetworkManager.singleton.networkPort = 5556;
			return true;
		}

	}
	void SetIp()
	{
		if (ip.text != "") {
			NetworkManager.singleton.networkAddress = ip.text;
		} else {
			NetworkManager.singleton.networkAddress = "localhost";
		}
	}


	public void IniciarCliente()
	{
		if (SetPort ()) 
		{
			SetIp ();
			NetworkManager.singleton.StartClient ();		
		}
	}
	public void IniciarServidor()
	{
		if (SetPort ()) 
		{
			NetworkManager.singleton.StartHost ();		
		}
	}

	void AlIniciar()
	{
		CanvasOffline.SetActive (false);
		CanvasOnline.SetActive (true);
	}

	public override void OnStartHost()
	{
		base.OnStartHost ();
		AlIniciar ();
	}

	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient (client);
		AlIniciar ();
	}


}
