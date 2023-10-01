using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class NewBehaviourScript : MonoBehaviour
{
    
    
    
    
    
    
    public float speed;

    private bool isMoving;
    private Vector2 input; 
    public LayerMask solidObjectsLayer;
    private Animator animator;
    bool facingRight = true;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if(input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                if (input.x > 0 && !facingRight)
                {
                    Flip();
                }
                if (input.x < 0 && facingRight)
                {
                    Flip();
                }
                
                

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);

    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

        
    }
    private bool IsWalkable(Vector3 targetpos)
    {
        if(Physics2D.OverlapCircle(targetpos, 0.1f, solidObjectsLayer ) != null)
        {
            return false;
        }
        return true;
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
