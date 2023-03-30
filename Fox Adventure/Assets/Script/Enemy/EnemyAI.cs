using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ISP*/
public interface IFrogAI
{
    void AI(Transform transform, Transform leftPosition, Transform rightPosition, float jumpLength, float jumpHeight, Rigidbody2D rigidbody2D, Collider2D collider, LayerMask layerMask, FrogAnimationControl frogAnimationControl);
}

public interface IOpposumAI
{
    void AI(Transform transform, Transform leftPosition, Transform rightPosition, float speed);
}

public class Frog : IFrogAI 
{
    private bool facingLeft = true;

    public void AI(Transform transform, Transform leftPosition, Transform rightPosition, float jumpLength, float jumpHeight, Rigidbody2D rb,Collider2D frogCollider, LayerMask groundLayer, FrogAnimationControl frogAnimationControl)
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

public class Opposum : IOpposumAI
{
    private bool facingLeft = true;

    public void AI(Transform transform, Transform leftPosition, Transform rightPosition, float speed)
    {
        if(facingLeft)
        {
            if(Vector2.Distance(transform.position, leftPosition.position) > 0)
            {
                if(transform.localScale.x != 1) transform.localScale = new Vector3(1, 1, 1);

                transform.position = Vector3.MoveTowards(transform.position, leftPosition.position, speed * Time.deltaTime);
            }
            else facingLeft = false;
        }
        else if(!facingLeft)
        {
            if(Vector2.Distance(transform.position, rightPosition.position) > 0)
            {
                if(transform.localScale.x != -1) transform.localScale = new Vector3(-1, 1, 1);

                transform.position = Vector3.MoveTowards(transform.position, rightPosition.position, speed * Time.deltaTime);
            }
            else facingLeft = true;
        }
    }
}

public class EnemyAI : Enemy
{
    #region EnumVariables
    private enum Type {
        Frog , Opposum
    }

    [SerializeField] private Type enemyType;
    #endregion

    #region InterfaceVariables
    private IFrogAI frog;
    private IOpposumAI opposum;
    #endregion

    #region OtherVariables
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    [Header("For Frog")]
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Collider2D frogCollider;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private FrogAnimationControl frogAnimationControl;

    [Header("For Opposum")]
    [SerializeField] private float speed;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        frog = new Frog();
        opposum = new Opposum();
    }

    void Update()
    {
        if(GameManager.Instance.IsPlaying())
        {
            if(enemyType == Type.Opposum) opposum.AI(transform, leftPosition, rightPosition, speed);
        }
    }

    //For Animation Event
    void FrogAI()
    {
        if(enemyType == Type.Frog) frog.AI(transform, leftPosition, rightPosition, jumpLength, jumpHeight, rb, frogCollider, layerMask, frogAnimationControl);
    }
}
