using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    dying
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public string baseAttack;
    public int moveSpeed;
    Animator anim;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<Spider>().Dying();
            
        }
    }

    public void Knock(Rigidbody2D rgbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(rgbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D rgbody, float knockTime)
    {
        if (rgbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rgbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.1f);
            if(rgbody.GetComponent<Enemy>().currentState != EnemyState.dying)
                rgbody.GetComponent<Enemy>().currentState = EnemyState.idle;
            //enemy.isKinematic = true;
        }
    }

  
}
