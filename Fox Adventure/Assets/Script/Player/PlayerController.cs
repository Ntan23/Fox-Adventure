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
    private GameManager gm;
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
    }   

    void Update()
    {   
        if(state != State.Hurt && gm.IsPlaying())
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
        if(state == State.Jumping) 
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
}
