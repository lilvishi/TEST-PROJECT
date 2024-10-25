using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    [SerializeField] int speed = 4;
    bool isGrounded = true;
    private int score = 0;
    private SpriteRenderer sr;

    [SerializeField] Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * movementVector.x, speed * movementVector.y);
        Debug.Log("speed is" + speed);
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        animator.SetBool("Walk_Right", !Mathf.Approximately(movementVector.x, 0));
        if(!Mathf.Approximately(movementVector.x,0))
        {
            sr.flipX = movementVector.x < 0;
        }

    }

    void OnSprint(InputValue value)
    {
        speed = 7;
    }

    void OnSprintRelease(InputValue value)
    {
        speed = 4;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }  
  
   private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }  
  
    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0,35) * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("My score is: " + score);
        }
    }
}


    