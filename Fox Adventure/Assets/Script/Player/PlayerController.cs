using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region FloatVariables
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float horizontalValue;
    #endregion

    #region IntegerVariables
    private int jumpCount;
    [SerializeField] private int maxJumpCount;
    #endregion

    #region EnumVariables
    private enum State {
        Idle, Running , Jumping , Falling
    }

    private State state;
    #endregion

    #region OtherVariables
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private PlayerAnimationControl playerAnimationControl;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimationControl = GetComponent<PlayerAnimationControl>();

        state = State.Idle;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeftAndRight();

        Jump();

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
        }   

        if(horizontalValue > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        } 
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump")) 
        {
            jumpCount++;

            if(jumpCount < maxJumpCount)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                state = State.Jumping;
            }
        }

        if(playerCollider.IsTouchingLayers(groundLayer)) jumpCount = 0;
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
        else if(horizontalValue != 0) state = State.Running;
        else state = State.Idle;
    }
}
