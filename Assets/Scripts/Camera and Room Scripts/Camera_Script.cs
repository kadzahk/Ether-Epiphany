using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public Transform player;
    public Transform Active_Room;
    public float dampSpeed;

    [Range(-50, 50)]
    public float minmod_x, maxmod_x, minmod_y, maxmod_y;

    public static Camera_Script instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Active_Room = player;
        transform.position = new Vector3(player.position.x, player.position.y, - 3f);
    }

    void Update()
    {
        var minpos_y = Active_Room.GetComponent<BoxCollider2D>().bounds.min.y + minmod_y;
        var maxpos_y = Active_Room.GetComponent<BoxCollider2D>().bounds.max.y + maxmod_y;
        var minpos_x = Active_Room.GetComponent<BoxCollider2D>().bounds.min.x + minmod_x;
        var maxpos_x = Active_Room.GetComponent<BoxCollider2D>().bounds.max.x + maxmod_x;

        Vector3 ClamPos = new Vector3(
            Mathf.Clamp(player.position.x, minpos_x, maxpos_x),
            Mathf.Clamp(player.position.y + 3f, minpos_y, maxpos_y),
            Mathf.Clamp(player.position.z, -15f, -15f)
            );

        Vector3 smoothPos = Vector3.Lerp(transform.position, ClamPos, dampSpeed * Time.deltaTime);

        transform.position = smoothPos;
    }
}
