using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Controllers : MonoBehaviour
{
    public static Controllers instance;
    private float velocity;
    public bool isKeyboard2;
    public float moveSpeed = 8f;
    public float jumpForce = 15f;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    bool isGrounded;
    bool canDoubleJump = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public CapsuleCollider2D capCollider;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayersManager.instance.AddPlayer(this);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capCollider = GetComponent<CapsuleCollider2D>();

    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isKeyboard2)
        {
            velocity = 0f;

            if(Keyboard.current.rightArrowKey.isPressed)
            {
                velocity = 1f;
            }
            if(Keyboard.current.leftArrowKey.isPressed)
            {
                velocity = -1f;
            }

            if(IsGrounded() && Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                canDoubleJump = true;
                AudioManager.instance.Playa(1); //jump sound
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if(canDoubleJump && Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                AudioManager.instance.Playa(1); //jump sound
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }

        rb.velocity = new Vector2(velocity * moveSpeed, rb.velocity.y);
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        /*
        //jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        if(isGrounded)
        {
            canDoubleJump = true;
            if(Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            if(canDoubleJump)
            {
                if(Input.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }*/

        if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        //animation
        
        animator.SetFloat("running", Mathf.Abs(rb.velocity.x));
        animator.SetBool("onGround", IsGrounded());
    }

    public void move(InputAction.CallbackContext context){
        velocity = context.ReadValue<Vector2>().x;
    }

    private bool IsGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider.bounds.center, capCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public void jump(InputAction.CallbackContext context){
        /*if(context.performed && context.started) //if there is no isGrounded
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }*/
        if(IsGrounded())
        {
            if(context.started)
            {
                canDoubleJump = true;
                AudioManager.instance.Playa(1); //jump sound
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }     
        }
        else
        {
            if(canDoubleJump)
            {
                if(context.started)
                {
                    AudioManager.instance.Playa(1); //jump sound
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }
    }

    public void fall(InputAction.CallbackContext context){
        if(context.started)
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce * 0.4f);
        }
    }

}


