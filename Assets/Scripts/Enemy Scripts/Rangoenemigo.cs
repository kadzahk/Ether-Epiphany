using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangoenemigo : MonoBehaviour
{
    public Animator Ani;
    public Prueba enemigo;
    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Ani.SetBool("Walk", false);
            Ani.SetBool("run", false);
            Ani.SetBool("attack", true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}


