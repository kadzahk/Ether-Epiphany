using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    Vector2 Enemypos;
    public GameObject target;
    bool perseguirP;
    public float Vel;
    public Animator Ani;
    public bool atacando;
    public GameObject rango;
    public GameObject Hit;

    private void Start()
    {
        Ani = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
    }
  
    public void Final_ani()
    {
        Ani.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);
    }
    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        if (perseguirP)
        {
            transform.position = Vector2.MoveTowards(transform.position, Enemypos, Vel * Time.deltaTime);
        }

        if (Vector2.Distance (transform.position, Enemypos) > 12F)
        {
            perseguirP = false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Enemypos = target.transform.position;
            perseguirP = true;

            Vector3 direction = target.transform.position - transform.position;
            if (direction.x >= 0.0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
