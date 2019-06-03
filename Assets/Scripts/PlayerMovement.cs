using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 10;
    private Rigidbody2D rgBody;
  
    private Vector2 change;
    private Vector2 pos;
    private Animator anim;
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signl playerHealthSignal;

    private enum DiagonalDirection
    {
        Up = 4, Left = 2, Right = 3, Down = 1
    }

    private DiagonalDirection dir;

    void Start()
    {
        currentHealth.initialValue = 100;
        dir = DiagonalDirection.Down;
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        rgBody = GetComponent<Rigidbody2D>();
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change.x != 0f && change.y != 0f)
        {
            FindDiag();
            anim.SetInteger("attackDir",(int) dir);

        }
        else if (change.x != 0f || change.y != 0f)
        {
            FindDir();
            anim.SetInteger("attackDir", (int)dir);
        }
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAndMove();
        }
    }
    private IEnumerator AttackCo()
    {
        anim.SetBool("moving", false);
        currentState = PlayerState.attack;
        anim.SetBool("attacking", true);
        yield return null;
        //yield return new WaitForSeconds(0.6f);
        anim.SetBool("attacking", false);

        yield return new WaitForSeconds(0.2f);
        currentState = PlayerState.idle;

    }

    void UpdateAndMove()
    {

        if (change != Vector2.zero)
        {
            currentState = PlayerState.walk;
            MoveCharacter();
            FindDiag();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
            currentState = PlayerState.idle;
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        pos = transform.position;
        rgBody.MovePosition(pos + change * speed * Time.deltaTime);
    }

    void FindDiag()
    {
        if (change.x > 0f & change.y > 0f)
        {
            if (change.x >= change.y)
                dir = DiagonalDirection.Right;
            else
                dir = DiagonalDirection.Up;
        }

        else if (change.x > 0f & change.y < 0f)
        {
            if (change.x >= -change.y)
                dir = DiagonalDirection.Right;
            else
                dir = DiagonalDirection.Down;
        }

        else if (change.x < 0f & change.y > 0f)
        {
            if (-change.x >= change.y)
                dir = DiagonalDirection.Left;
            else
                dir = DiagonalDirection.Up;
        }

        else if (change.x < 0f & change.y < 0f)
        {
            if (-change.x >= -change.y)
                dir = DiagonalDirection.Left;
            else
                dir = DiagonalDirection.Down;
        }
    }



        void FindDir()
        {
            if (change.x > 0f)
            {
                dir = DiagonalDirection.Right;
            }

            else if (change.x < 0f)
            {
                dir = DiagonalDirection.Left;
            }

            else if (change.y > 0f)
            {

                dir = DiagonalDirection.Up;

            }

            else if (change.y < 0f)
            {
                dir = DiagonalDirection.Down;
            }



        }
    public void Knock(float knockTime, float damage)
    {
        currentHealth.initialValue -= damage;
        if (currentHealth.initialValue > 0)
        {
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (rgBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rgBody.velocity = Vector2.zero;
            rgBody.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
            //enemy.isKinematic = true;
        }
    }
}


