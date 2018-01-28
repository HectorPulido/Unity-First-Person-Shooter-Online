using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.Networking;

public class Disparar : NetworkBehaviour {

    public Camera cam;
    GameObject DisparoGO;
    GameObject ExplosionGO;

    public GameObject DisparoPrefab;
    public GameObject ExplosionPrefab;

    public Transform Cañon;

    private RaycastHit hit;



    void Start()
    {
        if (DisparoGO == null)
        {
            DisparoGO = (GameObject)Instantiate(DisparoPrefab);
            DisparoGO.SetActive(false);

            DisparoGO.transform.position = Cañon.position;
            DisparoGO.transform.SetParent(Cañon);


        }
        if (ExplosionGO == null)
        {
            ExplosionGO = (GameObject)Instantiate(ExplosionPrefab);
            ExplosionGO.SetActive(false);
        }
    }

	void Update ()
    {
		if (CrossPlatformInputManager.GetButton("Fire1"))//.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(new Ray(cam.transform.position + cam.transform.forward * .5f, cam.transform.forward), out hit, 20))
            {
                DisparoGO.SetActive(true);

                ExplosionGO.transform.position = hit.point;
                ExplosionGO.SetActive(true);

                Invoke("ApagarEfectos", 2);

                if (hit.collider.CompareTag("Player"))
                {
                    CmdQuitarVida(hit.collider.gameObject);
                }              

            }
        }
	}

    [Command]
    void CmdQuitarVida(GameObject go)
    {
        go.GetComponent<ApagarScripts>().RpcDano();
    }



    void ApagarEfectos()
    {
        ExplosionGO.SetActive(false);
        DisparoGO.SetActive(false);
    }

}
