using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.05f;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private LayerMask colissionMask;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetButton("Jump");

        _rigidbody.velocity = new Vector2(inputX * speed, _rigidbody.velocity.y);

        if (jumpInput)
        {
            System.Diagnostics.Trace.WriteLine("jump");
        }
        
        if (jumpInput && IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        }

        if (inputX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
        }
    }

    private bool IsGrounded()
    {
        System.Diagnostics.Trace.WriteLine("grouded");
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, colissionMask);
    }
}
