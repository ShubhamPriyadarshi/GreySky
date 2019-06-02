using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public GameObject enemy;
    public float damage; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if ((obj.gameObject.CompareTag("Enemy") && !this.gameObject.CompareTag("Enemy")) || obj.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = obj.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust* Time.deltaTime;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (obj.gameObject.CompareTag("Enemy") && hit.GetComponent<Enemy>().currentState != EnemyState.stagger && obj.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    obj.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if (obj.gameObject.CompareTag("Player") && hit.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    obj.GetComponent <PlayerMovement>().Knock(knockTime);
                }
                
              
               
            }
        }
    }
}
