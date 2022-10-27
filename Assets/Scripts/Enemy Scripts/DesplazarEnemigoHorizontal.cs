using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarEnemigoHorizontal : MonoBehaviour
{
	
	public float minX;
	public float maxX;
	public float TiempoEspera = 2f;
	public float Velocidad = 1f;

	private GameObject _LugarObjetivo;


	// Se llama al inicio antes de la primera actualización de frames
	void Start()
    {
		UpdateObjetivo();
		StartCoroutine("Patrullar");
	}

	// La actualización se llama una vez por cuadro
	void Update()
    {
        
    }


	private void UpdateObjetivo()
	{
		// Si es la primera vez iniciar el patrullaje para la izquierda
		if (_LugarObjetivo == null) {
			_LugarObjetivo = new GameObject("Sitio_objetivo");
			_LugarObjetivo.transform.position = new Vector2(minX, transform.position.y);
			transform.localScale = new Vector3(-1, 1, 1);
			return;
		}

		// iniciar el patrullaje para la derecha
		if (_LugarObjetivo.transform.position.x == minX) {
			_LugarObjetivo.transform.position = new Vector2(maxX, transform.position.y);
			transform.localScale = new Vector3(1, 1, 1);
		}

		// Cambio de sentido de derecha a izquierda
		else if (_LugarObjetivo.transform.position.x == maxX) {
			_LugarObjetivo.transform.position = new Vector2(minX, transform.position.y);
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	private IEnumerator Patrullar()
	{
		// Co-rutina para mover el enemigo
		while(Vector2.Distance(transform.position, _LugarObjetivo.transform.position) > 0.05f) {
			// Se desplazará hasta el sitio objetivo
			Vector2 direction = _LugarObjetivo.transform.position - transform.position;
			float xDirection = direction.x;

			transform.Translate(direction.normalized * Velocidad * Time.deltaTime);

			yield return null;
		}

		// En este punto, se alcanzó el objetivo, se establece nuestra posición en la del objetivo.
		Debug.Log("Se alcanzo el Obejitvo");
		transform.position = new Vector2(_LugarObjetivo.transform.position.x, transform.position.y);

		// Esperamos un momento antes de volver a movernos
		Debug.Log("Esperando " + TiempoEspera + " segundos");
		yield return new WaitForSeconds(TiempoEspera);

		Debug.Log("Se espera lo necesario para que termine y vuelva a empezar movimiento");
		UpdateObjetivo();
		StartCoroutine("Patrullar");
	}
}
