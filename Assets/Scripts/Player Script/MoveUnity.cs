using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MoveUnity : MonoBehaviour
{
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private bool grounded;
    [SerializeField] private PlayerInput PlayerInput;
    public GameObject[] hearts;
    private int life;
    private bool doujump;
    static public bool Upgraded;
    public bool Crouch_Down = false;
    int Crouch_DownID;

    #region Movement
    private float Horizontal;
    public float Speed = 1;
    public float acceleration;
    public float decceleration;
    public float velPower;
    #endregion
    #region Dash
    public float dashSpeed;
    public float startDashTime;
    private float dashTime;
    private int direction;
    private float originalGravity;
    #endregion
    #region Attack
    public int combo;
    public bool attacking;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        life = hearts.Length;
        Upgraded = false;
        originalGravity = Rigidbody2D.gravityScale;
        Crouch_DownID = Animator.StringToHash("Crouch_Down");
    }

    public void StartCombo()
    {
        attacking = false;
        if (combo < 3) combo++;
    }

    public void FinishCombo()
    {
        attacking = false;
        combo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Down();
        Combo();

        Horizontal = Input.GetAxisRaw("Horizontal");

        Animator.SetBool("IsWalking", Horizontal != 0.0f);

        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-0.85f, 0.85f, 1.0f);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(0.85f, 0.85f, 1.0f);
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 5f))
        {
            grounded = true;
            doujump = true;
        }
        else
        {
            grounded = false;
        }

        Animator.SetBool("Jump", !grounded);

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        Animator.SetFloat("yVelocity", Rigidbody2D.velocity.y);

        
        Dash();

        Physics2D.IgnoreLayerCollision(3, 7);
        Physics2D.IgnoreLayerCollision(3, 8);
    }

    public void Jump()
    {
        if (grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
            Animator.SetBool("Jump", true);
        }
        else if (doujump && Upgraded)
        {
            Rigidbody2D.AddForce(Vector2.up * (JumpForce/2));
            doujump = false;
            Animator.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
    {
        #region Run
        float targetSpeed = Horizontal * Speed;
        float speedDif = targetSpeed - Rigidbody2D.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        Rigidbody2D.AddForce(movement * Vector2.right);
        #endregion
    }

    public void Combo()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            attacking = true;
            Animator.SetTrigger("" + combo);
        }
    }

    public void Dash()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (Horizontal < 0.0f) direction = 1;
                else if (Horizontal > 0.0f) direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                Animator.SetBool("IsDashing", false);
                Rigidbody2D.velocity = Vector2.zero;
                Rigidbody2D.gravityScale = originalGravity;
            }
            else
            {
                dashTime -= Time.deltaTime;
                Animator.SetBool("IsDashing", true);
                if (direction == 1) Rigidbody2D.velocity = Vector2.left * dashSpeed;
                if (direction == 2) Rigidbody2D.velocity = Vector2.right * dashSpeed;
                Rigidbody2D.gravityScale = 0f;
            }
        }
    }

    private void CheckLife()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
            Invoke("respawn", 0.4f);
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public void Hit()
    {
        life--;
        CheckLife();
    }

    protected void respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Down()
    {
        if (!grounded)
            return;

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            Crouch_Down = true;
        }
        else
        {
            Crouch_Down = false;
        }

        Animator.SetBool(Crouch_DownID, Crouch_Down);
    }
}