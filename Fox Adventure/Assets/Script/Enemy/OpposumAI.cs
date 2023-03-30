using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpposumAI : Enemy
{
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    [SerializeField] private float speed;
    private bool facingLeft = true;
    private GameManager gm;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if(gm.IsPlaying())
        {
            MoveAI();
        }
    }

    private void MoveAI()
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
