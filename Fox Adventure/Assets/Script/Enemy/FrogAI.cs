using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour
{
    #region FloatVariables
    [SerializeField] private float xLeftCap;
    [SerializeField] private float xRightCap;
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    #endregion

    #region BoolVariables
    private bool facingLeft = true;
    #endregion

    #region OtherVariables
    [SerializeField] private LayerMask groundLayer;
    private Collider2D frogCollider;
    private Rigidbody2D rb;
    private FrogAnimationControl frogAnimationControl;
    #endregion
    
    void Awake()
    {
        frogCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        frogAnimationControl =GetComponent<FrogAnimationControl>();
    }

    void MoveAI()
    {
        if(facingLeft)
        {
            if(transform.position.x > xLeftCap)
            {
                if(transform.localScale.x != 1) transform.localScale = new Vector3(1, 1, 1);

                if(frogCollider.IsTouchingLayers(groundLayer)) 
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);

                    frogAnimationControl.SetJumpAnimation();
                }
            }
            else facingLeft = false;
        }
        else if(!facingLeft)
        {
            if(transform.position.x < xRightCap)
            {
                if(transform.localScale.x != -1) transform.localScale = new Vector3(-1, 1, 1);

                if(frogCollider.IsTouchingLayers(groundLayer)) 
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);

                    frogAnimationControl.SetJumpAnimation();
                }
            }
            else facingLeft = true;
        }
    }
}
