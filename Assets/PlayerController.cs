using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    [SerializeField] int speed = 2;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
        Debug.Log(speed);
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        speed = 6;
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

}


    