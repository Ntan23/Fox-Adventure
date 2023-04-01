using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region EnumVariables
    private enum State {
        Idle, Running, Jumping, Falling, Hurt, Climb
    }

    private State state;
    #endregion

    #region FloatVariables
    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed;
    public float jumpForce;
    private float horizontalValue;
    private float verticalValue;
    private float intialGravityScale;
    #endregion

    #region IntegerVariables
    private int jumpCount;
    [SerializeField] private int maxJumpCount;
    #endregion

    #region BoolVariables
    private bool canClimb = false;
    private bool atTopLadder = false;
    private bool atBottomLadder = false;
    #endregion

    #region OtherVariables
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private PlayerAnimationControl playerAnimationControl;
    [SerializeField] private LayerMask groundLayer;
    private AudioManager audioManager;
    private GameManager gm;
    private Ladder ladder;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimationControl = GetComponent<PlayerAnimationControl>();

        audioManager = AudioManager.Instance;
        gm = GameManager.Instance;

        state = State.Idle;
        jumpCount = 0;
        intialGravityScale = rb.gravityScale;
    }   

    void Update()
    {   
        if(gm.IsPlaying())
        {
            if(state == State.Climb) Climb();
            else if(state != State.Hurt)
            {
                Move();
                Jump(); 
            }   
        }

        CheckState();
        playerAnimationControl.SetAnimation((int) state);
    }

    void Move()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");

        //Climb
        if(canClimb && Mathf.Abs(verticalValue) > 0.1f) 
        {
            state = State.Climb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0;
        }

        //Move Left
        if(horizontalValue < 0) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }   

        //MoveRight
        if(horizontalValue > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        } 
    }

    private void Climb()
    {
        float verticalValue = Input.GetAxisRaw("Vertical");

        //Check If Jump 
        if(Input.GetButtonDown("Jump"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = intialGravityScale;

            playerAnimationControl.SetAnimationSpeed(1.0f);

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.Jumping;
            audioManager.PlayJumpSFX();

            return;
        }

        if(verticalValue > 0.1f && !atTopLadder)
        {
            //Climbing Up
            rb.velocity = new Vector2(0, verticalValue * climbSpeed);
            playerAnimationControl.SetAnimationSpeed(1.0f);
        }
        else if(verticalValue < -0.1f && !atBottomLadder)
        {
            //Climbing Down
            rb.velocity = new Vector2(0, verticalValue * climbSpeed);
            playerAnimationControl.SetAnimationSpeed(1.0f);
        }
        else 
        {
            playerAnimationControl.SetAnimationSpeed(0.0f);
            rb.velocity = Vector2.zero;
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
                audioManager.PlayJumpSFX();
            }
        }

        if(playerCollider.IsTouchingLayers(groundLayer) && state == State.Idle) jumpCount = 0;
    }

    void CheckState()
    {
        if(state == State.Climb)
        {

        }
        else if(state == State.Jumping) 
        {
            if(rb.velocity.y < 0.1f) 
            {
                state = State.Falling;
                audioManager.PlayFallSFX();
            }
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

    public bool IsRunning()
    {
        return state == State.Running;
    }

    public void GetHurt()
    {
        state = State.Hurt;
    }

    public void ChangeSpeedAndJumpForce(float speedValue, float jumpForceValue)
    {
        speed = speedValue;
        jumpForce = jumpForceValue;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public void ChangeCanClimb(bool value)
    {
        canClimb = value;
    }

    public void ChangeTopLadder(bool value)
    {
        atTopLadder = value;
    }

    public void ChangeBottomLadder(bool value)
    {
        atBottomLadder = value;
    }

    public void SetLadder(Ladder ladderScript)
    {
        ladder = ladderScript;
    }
}
