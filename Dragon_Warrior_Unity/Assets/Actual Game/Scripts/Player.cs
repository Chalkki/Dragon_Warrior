using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementX;
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float rollForce = 15f;
    // Start is called before the first frame update

    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D myBody;

    //the parameter for the character to run
    private string Run_Anim = "is_run";
    private string Roll_Anim = "is_roll";
    private string Jump_Anim = "jumping";
    private bool canRoll = true;
    private bool isRolling = false;
    private float rollCooldown = 1f;
    private float rollingTime = 0.2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
    }



    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0, 0) * moveForce * Time.deltaTime;

        if(movementX > 0)
        {
            // move right
            anim.SetBool(Run_Anim, true);
            sr.flipX = false;
        }else if (movementX < 0)
        {
            // move left
            anim.SetBool(Run_Anim, true);
            sr.flipX = true;
        }
        else
        {
            // stable

            anim.SetBool(Run_Anim, false);
        }
    }
    void PlayerJump()
    {
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        anim.SetTrigger(Jump_Anim);
    }

    private IEnumerator PlayerRoll()
    {
        canRoll = false;
        isRolling = true;
        anim.SetBool("is_roll", true);
        if (!sr.flipX)
        {
            myBody.velocity = new Vector2(transform.localScale.x * rollForce, 0f);
        }
        else
        {
            myBody.velocity = new Vector2(-transform.localScale.x * rollForce, 0f);
        }
        yield return new WaitForSeconds(rollingTime);
        myBody.velocity = new Vector2(0f, 0f);
        anim.SetBool("is_roll", false);
        isRolling = false;
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;

    }
}
