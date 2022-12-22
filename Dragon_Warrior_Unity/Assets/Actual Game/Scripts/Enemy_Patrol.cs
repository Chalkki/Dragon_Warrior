using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{

    Rigidbody2D rb;
    private Animator animator;
    float moveSpeed = 2.0f;
    public bool is_Patrol = true;
    private bool facingRight = true;
    //private Vector3 baseScale;
    [SerializeField]
    private Transform castTrans;
    [SerializeField]
    private float baseCastDist;

    // Start is called before the first frame update
    void Start()
    {
        //baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (is_Patrol)
        {
            float vX = moveSpeed;
            if (!facingRight)
            {
                vX = -moveSpeed;
            }
            rb.velocity = new Vector2(vX, rb.velocity.y);
            animator.SetBool("is_patrol", true);
            if (IsHittingWallOrEdge())
            {
                ChangeFacingDirection();
            }
        }
    }
    
    bool IsHittingWallOrEdge()
    {
        bool hit = false;
        float castDist = baseCastDist;
        // Check if hitting wall
        if (!facingRight)
        {
            castDist = -baseCastDist;
        }
        // determine the target destination (the wall edge that touches with the tip of the ray) based on the cast distance
        Vector3 targetPos = castTrans.position;
        targetPos.x += castDist;

        Debug.DrawLine(castTrans.position, targetPos, Color.blue);
        if (Physics2D.Linecast(castTrans.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            hit = true;
        }

        // Check if hitting edge
        targetPos = castTrans.position;
        targetPos.y -= baseCastDist;
        if (Physics2D.Linecast(castTrans.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            hit = false;
        }
        else
        {
            hit = true;
        }
        return hit;
    }

    void ChangeFacingDirection()
    {
        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
        transform.Rotate(0f, 180f, 0f);

    }
}
