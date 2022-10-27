using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoenemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;

    [SerializeField] private Transform controladorSuelo;

    [SerializeField] private Transform controladorPared;

    [SerializeField] private float distancia;

    [SerializeField] private float distancia2;

    [SerializeField] private bool movimientoDerecha;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        RaycastHit2D informacionPared = Physics2D.Raycast(controladorPared.position, Vector2.right, distancia2);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if (informacionSuelo == false)
        {
            //Girar
            Girar();
        }
        if (informacionPared == true)
        {
            //Girar
            Girar();
        }
    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
        Gizmos.DrawLine(controladorPared.transform.position, controladorPared.transform.position + Vector3.right * distancia2);
    }
}
