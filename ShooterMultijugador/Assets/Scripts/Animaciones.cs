using UnityEngine;
using System.Collections;

public class Animaciones : MonoBehaviour {

    public Animator anim;


	void Update ()
    {

        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }
}
