using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    
    void Update()
    {
            
    }

    public void Smash()
    {

        anim.SetBool("isHit", true);
        StartCoroutine(WaitAndDestroy());

    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);

    }
}
