using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 10;
    private Rigidbody2D rgBody;
    private Vector2 change;
    private Vector2 pos;
    private Animator anim;
    public PlayerState currentState;

    private enum DiagonalDirection
    {
        Up = 4, Left = 2, Right = 3, Down = 1
    }

    private DiagonalDirection dir;

    void Start()
    {
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
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
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
        currentState = PlayerState.walk;

    }

    void UpdateAndMove()
    {

        if (change != Vector2.zero)
        {
            MoveCharacter();
            FindDiag();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);

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
    }


