using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region FloatVariables
    [SerializeField] private float speed;
    public float jumpForce;
    private float horizontalValue;
    #endregion

    #region IntegerVariables
    private int jumpCount;
    [SerializeField] private int maxJumpCount;
    #endregion

    #region EnumVariables
    private enum State {
        Idle, Running , Jumping , Falling , Hurt
    }

    private State state;
    #endregion

    #region OtherVariables
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private PlayerAnimationControl playerAnimationControl;
    [SerializeField] private LayerMask groundLayer;
    private AudioManager audioManager;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimationControl = GetComponent<PlayerAnimationControl>();

        audioManager = AudioManager.Instance;

        state = State.Idle;
        jumpCount = 0;
    }   

    void Update()
    {
        if(state != State.Hurt)
        {
            MoveLeftAndRight();
            Jump(); 
        }

        CheckState();
        playerAnimationControl.SetAnimation((int) state);
    }

    void MoveLeftAndRight()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if(horizontalValue < 0) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            audioManager.Play("Footstep");
        }   

        if(horizontalValue > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            audioManager.Play("Footstep");
        } 
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump")) 
        {
            if(jumpCount < maxJumpCount)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                
                jumpCount++;
                state = State.Jumping;
            }
        }

        if(playerCollider.IsTouchingLayers(groundLayer) && state == State.Idle) jumpCount = 0;
    }

    void CheckState()
    {
        if(state == State.Jumping) 
        {
            if(rb.velocity.y < 0.1f) state = State.Falling;
        }
        else if(state == State.Falling)
        {
            if(playerCollider.IsTouchingLayers(groundLayer)) state = State.Idle;
        }
        else if(state == State.Hurt) 
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f) state = State.Idle;
        }
        else if(horizontalValue != 0) state = State.Running;
        else state = State.Idle;
    }

    public bool IsFalling()
    {
        return state == State.Falling;
    }

    public void GetHurt()
    {
        state = State.Hurt;
    }
}
