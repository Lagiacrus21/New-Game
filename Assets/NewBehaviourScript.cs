using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Vector2 input;
    public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    bool facingRight = true;
    
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input.x != 0 )
        {
            input.y = 0;
        }

        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);
        animator.SetFloat("speed", input.sqrMagnitude);

        if (input.x > 0 && !facingRight)
        {
            Flip();
        }
        if (input.x < 0 && facingRight)
        {
            Flip();
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
