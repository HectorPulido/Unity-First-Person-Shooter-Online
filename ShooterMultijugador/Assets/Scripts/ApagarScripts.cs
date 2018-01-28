using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ApagarScripts : NetworkBehaviour
{
    public MonoBehaviour[] Componentes;
    public Camera Camara;
    public Camera Camara2;
    public AudioListener audioListener;
    public GameObject Arma;

    public Renderer[] renderersLocal;
    public Renderer[] renderesOTROS;


    const string LayerArma = "Arma";

    public int Vida = 100;
    public Text Vida_txt;


    public override void OnStartLocalPlayer()
    {        
        Vida_txt = GameObject.Find("Vida").GetComponent<Text>();
        Arma.layer = LayerMask.NameToLayer(LayerArma);
        Inicio();

        gameObject.name = "LOCALPLAYER";
    }

    void Inicio()
    {
        if (isLocalPlayer)
        {            
            Vida_txt.text = "Vida: " + Vida.ToString();
            ToggleScripts(true);
        }
        Vida = 100;
        ToggleRenderer(true);
    }

    void ToggleScripts(bool Estado)
    {
        for (int i = 0; i < Componentes.Length; i++)
        {
            Componentes[i].enabled = Estado;
        }
        Camara.enabled = Estado;
        Camara2.enabled = Estado;
        audioListener.enabled = Estado;
       
    }


    void ToggleRenderer(bool estado)
    {
        if (isLocalPlayer)
        {
            for (int i = 0; i < renderersLocal.Length; i++)
            {
                renderersLocal[i].enabled = estado;
            }

            for (int i = 0; i < renderesOTROS.Length; i++)
            {
                renderesOTROS[i].enabled = false;
            }

        }
        else
        {
            for (int i = 0; i < renderesOTROS.Length; i++)
            {
                renderesOTROS[i].enabled = estado;
            }

            for (int i = 0; i < renderersLocal.Length; i++)
            {
                renderersLocal[i].enabled = false;
            }
        }
        

    }


    [ClientRpc]
    public void RpcDano()
    {
        Vida -= 20;
        if (Vida <= 0)
        {
            ToggleScripts(false);
            ToggleRenderer(false);
            Invoke("Inicio", 5);
            //Spawn
            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }
        if (isLocalPlayer)
            Vida_txt.text = "Vida: " + Vida.ToString();
    }
}
