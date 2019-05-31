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

  
    //Vector2 change;
    //Vector2 pos;
   // Rigidbody2D rgbody;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        change = Vector2.zero;
        rgbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector2.Distance(transform.position, targetPos.position) <= chaseRadius)
        {
          
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            anim.SetFloat("moveX",transform.position.x)
        }
    }
}
