using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : Enemy
{
    #region FloatVariables
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    #endregion

    #region TransformVariables
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    #endregion

    #region BoolVariables
    private bool facingLeft = true;
    #endregion

    #region OtherVariables
    [SerializeField] private LayerMask groundLayer;
    private Collider2D frogCollider;
    private FrogAnimationControl frogAnimationControl;
    private GameManager gm;
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        frogCollider = GetComponent<Collider2D>();
        frogAnimationControl =GetComponent<FrogAnimationControl>();
    }

    void Start()
    {
        gm = GameManager.Instance;
    }

    void MoveAI()
    {
        if(gm.IsPlaying())
        {
            if(facingLeft)
            {
                if(transform.position.x > leftPosition.position.x)
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
                if(transform.position.x < rightPosition.position.x)
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
}
