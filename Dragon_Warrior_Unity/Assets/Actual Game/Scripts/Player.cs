using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementX;
    [SerializeField]
    private float
        moveForce,
        jumpForce,
        rollForce,
    // damage reaction
        repelForceX ,
        repelForceY;
    [SerializeField]
    private bool
        facingRight = true;

    // Start is called before the first frame update

    private Animator anim;
    private Rigidbody2D myBody;
    private CapsuleCollider2D coll;
    private SpriteRenderer sprite;
    private Inventory inventory;

    //animator parameter
    private string
        Run_Anim = "is_run",
        Roll_Anim = "is_roll",
        Jump_Anim = "jumping";
    // parameters for rolling
    private bool
        canRoll = true,
        isRolling = false;
    private float
        rollCooldown = 1f,
        rollingTime = 0.2f;
    // layermask for ground jumping only
    [SerializeField]
    private LayerMask jumpableGround;
    // inventory
    [SerializeField]
    private
        UI_Inventory uiInventory;
    // the time that allowed player to press before touching the ground
    private float jumpPressedTime = 0.2f;
    private float jumpPressed;
    private float justLeaveGroundTime = 0.1f;
    private float justLeaveGround;
    private bool isJumping = false;
    private float jumpTimeCounter;
    public float maxJumpTime;
    // parameters for climbing
    public bool
        isLadder = false,
        isClimbing = false;
    private float
        vertical = 0f,
        climbSpeed = 5f;
       
    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        jumpPressed = 0f;
        justLeaveGround = 0f;

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            return;
        }
        PlayerMoveKeyboard();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll)
        {
            StartCoroutine(PlayerRoll());
        }

        // give the player a time gap to jump
        jumpPressed -= Time.deltaTime;
        justLeaveGround -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = jumpPressedTime;
        }
        if (IsGrounded())
        {
            justLeaveGround = justLeaveGroundTime;
            if(jumpPressed > 0f)
            {
                jumpPressed = 0f;
                PlayerJump();
                isJumping = true;
                jumpTimeCounter = maxJumpTime;
            }
        }
        if (jumpPressed> 0f && justLeaveGround > 0f)
        {
            jumpPressed = 0f;
            justLeaveGround = 0f;
            PlayerJump();
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
        }
        // let the player to hold for some time for a higher jump

        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            jumpTimeCounter -= Time.deltaTime;
            if(jumpTimeCounter > 0f)
            {
                PlayerJump();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        // check the player climbing the ladder
        vertical = Input.GetAxis("Vertical");
        if (isLadder && vertical != 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            myBody.gravityScale = 0f;
            myBody.velocity = new Vector2(myBody.velocity.x, vertical * climbSpeed);
        }
        else
        {
            // change back to the original gravity scale
            myBody.gravityScale = 1f;
        }
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        

        if (myBody.bodyType == RigidbodyType2D.Dynamic)
        {
            myBody.velocity = new Vector2(movementX * moveForce, myBody.velocity.y);
        }

        if(movementX > 0)
        {
            // move right
            anim.SetBool(Run_Anim, true);
            if (!facingRight)
            {
                facingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }else if (movementX < 0)
        {
            // move left
            anim.SetBool(Run_Anim, true);
            if (facingRight)
            {
                facingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            // stable

            anim.SetBool(Run_Anim, false);
        }
    }
    void PlayerJump()
    {
        myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.CapsuleCast(coll.bounds.center, coll.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, .1f, jumpableGround); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            //Vector2 repelForceDirection = coll.bounds.center - collision.collider.bounds.center;
            //if(repelForceDirection.x <= 0)
            //{
            //    // repel left
            //    myBody.AddForce(new Vector2(-repelForceX, 0f), ForceMode2D.Force);
            //}
            //else
            //{
            //    // repel right
            //myBody.AddForce(new Vector2(repelForceX, repelForceY), ForceMode2D.Force);
            //}
            float repel_direc_x = Mathf.Sign(transform.position.x - collision.gameObject.transform.position.x);
            float repel_direc_y = Mathf.Sign(transform.position.y - collision.gameObject.transform.position.y);
            if (myBody.bodyType == RigidbodyType2D.Dynamic)
            {
                myBody.AddForce(new Vector2(repel_direc_x * repelForceX, repel_direc_y * repelForceY), ForceMode2D.Impulse);
            }

            //myBody.velocity = new Vector2(repelForceX, repelForceY);

            Debug.Log(myBody.velocity);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
            if (itemWorld != null)
            {
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
    private IEnumerator PlayerRoll()
    {
        canRoll = false;
        isRolling = true;
        anim.SetBool(Roll_Anim, true);
        if (facingRight)
        {
            myBody.velocity = new Vector2(transform.localScale.x * rollForce, 0f);
        }
        else
        {
            myBody.velocity = new Vector2(-transform.localScale.x * rollForce, 0f);
        }
        yield return new WaitForSeconds(rollingTime);
        myBody.velocity = new Vector2(0f, 0f);
        anim.SetBool(Roll_Anim, false);
        isRolling = false;
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

}
