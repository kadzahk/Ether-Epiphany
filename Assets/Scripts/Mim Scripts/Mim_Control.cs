using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mim_Control : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float TargetPos;
    [SerializeField] Transform PlayerTransform;
    private Rigidbody2D Rigidbody;
    private Animator Animator;
    Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("MimTarget").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetFollow();
        FlipMim();
        Physics2D.IgnoreLayerCollision(7, 8);
    }

    // TargetFollow is called every time the player is moving
    void TargetFollow()
    {
        if (Vector2.Distance(transform.position, Target.position) > TargetPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            Animator.SetBool("Moving", true);
        }
        else
        {
            Animator.SetBool("Moving", false);
        }
    }

    // FlipMim is called to check the position where Mim will face at
    void FlipMim()
    {
        if (PlayerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 1.0f);
        }
        else if (PlayerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
        }
    }
}
