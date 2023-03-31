using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ISP*/
public interface IFrogAI
{
    void AI(bool canMove, Transform transform, Transform leftPosition, Transform rightPosition, float jumpLength, float jumpHeight, float idleDuration, Rigidbody2D rigidbody2D, Collider2D collider, LayerMask layerMask, FrogAnimationControl frogAnimationControl);
}

public interface IOpposumAI
{
    void AI(bool canMove, bool withIdle, Transform transform, Transform leftPosition, Transform rightPosition, float speed, float idleDuration);
}

public class Frog : IFrogAI 
{
    private float idleTimer;
    private float maxIdleDuration;
    private bool facingLeft = true;
    private bool firstTimeCheck = true;

    public void AI(bool canMove, Transform transform, Transform leftPosition, Transform rightPosition, float jumpLength, float jumpHeight, float idleDuration, Rigidbody2D rb,Collider2D frogCollider, LayerMask groundLayer, FrogAnimationControl frogAnimationControl)
    {
        if(firstTimeCheck)
        {
            idleTimer = idleDuration;
            firstTimeCheck = false;
        }

        if(facingLeft && canMove)
        {
            idleTimer += Time.deltaTime;

            if(idleTimer > idleDuration)
            {
                idleTimer = 0;

                if(transform.position.x > leftPosition.position.x)
                {
                    if(transform.localScale.x != 1) transform.localScale = new Vector3(1, 1, 1);

                    if(frogCollider.IsTouchingLayers(groundLayer)) 
                    {
                        rb.velocity = new Vector2(-jumpLength, jumpHeight);

                        frogAnimationControl.SetJumpAnimation();
                    }
                }
                else 
                {
                    idleTimer = idleDuration;
                    facingLeft = false;
                }
            }
        }
        else if(!facingLeft)
        {
            idleTimer += Time.deltaTime;

            if(idleTimer > idleDuration)
            {
                idleTimer = 0;

                if(transform.position.x < rightPosition.position.x)
                {
                    if(transform.localScale.x != -1) transform.localScale = new Vector3(-1, 1, 1);

                    if(frogCollider.IsTouchingLayers(groundLayer)) 
                    {
                        rb.velocity = new Vector2(jumpLength, jumpHeight);

                        frogAnimationControl.SetJumpAnimation();
                    }
                }
                else 
                {
                    idleTimer = idleDuration;
                    facingLeft = true;
                }
            }
        }
    }
}

public class Opposum : IOpposumAI
{
    private float idleTimer = 0;
    private float maxIdleDuration;
    private bool facingLeft = true;

    public void AI(bool canMove, bool withIdle, Transform transform, Transform leftPosition, Transform rightPosition, float speed, float idleDuration)
    {
        if(withIdle) maxIdleDuration = idleDuration;
        if(!withIdle) maxIdleDuration = 0;

        if(facingLeft && canMove)
        {
            if(Vector2.Distance(transform.position, leftPosition.position) > 0)
            {
                if(transform.localScale.x != 1) transform.localScale = new Vector3(1, 1, 1);

                transform.position = Vector3.MoveTowards(transform.position, leftPosition.position, speed * Time.deltaTime);
            }
            else
            {
                idleTimer += Time.deltaTime;

                if(idleTimer > maxIdleDuration)
                {
                    idleTimer = 0;
                    facingLeft = false;
                }
            }
        }
        else if(!facingLeft && canMove)
        {
            if(Vector2.Distance(transform.position, rightPosition.position) > 0)
            {
                if(transform.localScale.x != -1) transform.localScale = new Vector3(-1, 1, 1);

                transform.position = Vector3.MoveTowards(transform.position, rightPosition.position, speed * Time.deltaTime);
            }
            else
            {
                idleTimer += Time.deltaTime;

                if(idleTimer > maxIdleDuration)
                {
                    idleTimer = 0;
                    facingLeft = true;
                }
            }
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
    [SerializeField] private float idleDuration;
    [Header("For Frog")]
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private FrogAnimationControl frogAnimationControl;

    [Header("For Opposum")]
    [SerializeField] private float speed;
    [SerializeField] private bool withIdle;
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
            if(enemyType == Type.Frog) frog.AI(canMove, transform, leftPosition, rightPosition, jumpLength, jumpHeight, idleDuration, rb, enemyCollider, layerMask, frogAnimationControl);
            if(enemyType == Type.Opposum) opposum.AI(canMove, withIdle, transform, leftPosition, rightPosition, speed, idleDuration);
        }
    }
}
