using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    [SerializeField] public GameObject dialogBox;
    [SerializeField] public Text text;
    [SerializeField] public string placeText;
    [SerializeField] public bool inRange;

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKey(("joystick button 0")))
        {
            dialogBox.SetActive(true);
            text.text = placeText;
        }

        else
        {
            dialogBox.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            inRange = false;
        }
    }
}
