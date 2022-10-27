using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Playerposition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Upgrade Triggered");
            MoveUnity.Upgraded = true;
            Destroy(gameObject);
        }
    }
}
