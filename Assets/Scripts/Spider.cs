using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public Transform targetPos;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePos;
    Animator anim;
    Vector3 change;
    Vector3 temp;
    float dist;
    Rigidbody2D rgbody;

    //Vector2 change;
    //Vector2 pos;
    // Rigidbody2D rgbody;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        change = Vector2.zero;
        rgbody = GetComponent<Rigidbody2D>();
        //rgbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDIstance();

    }

    void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }

            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }

        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }

            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }

        }
    }
    void CheckDIstance()
    {
        dist = Vector2.Distance(transform.position, targetPos.position);
    //    if (currentState != EnemyState.stagger && currentState != EnemyState.dying)
      //  {
            if (dist <= chaseRadius && dist > attackRadius)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk)
                {

                    Vector3 temp = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    rgbody.MovePosition(temp);

                    anim.SetBool("moving", true);
                    ChangeState(EnemyState.walk);
                }
            }

            else if (dist > chaseRadius || dist < attackRadius)
            {
                ChangeState(EnemyState.idle);
                anim.SetBool("moving", false);
            }
        //}
        //else if (currentState == EnemyState.dying)
       // {
         //   rgbody.velocity = Vector2.zero;
       // }
    }
    

    void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void Dying()
    {
        anim.SetBool("moving", false);
        StartCoroutine(DyingCo());
    }

    private IEnumerator DyingCo()
    {
        ChangeState(EnemyState.dying);
        
        anim.SetBool("dying", true);
        
        
        yield return new WaitForSeconds(1.5f);
        rgbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
       

    }
}

