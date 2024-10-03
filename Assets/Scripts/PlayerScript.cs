using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScritpt : MonoBehaviour
{
    // Start is called before the first frame update

    public float MaxSpeed; //limiting maximum speed of the player so that the player doesn't fly away from the screen
    public float JumpHeight;
    Rigidbody2D rigid;
    SpriteRenderer SpriteRenderer;
    Animator ani;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);

        //stopping the player when the arrow key is released so that it doesn't slide away
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Changing the direction of the player depending on right/left arrow key
        if (Input.GetButtonDown("Horizontal"))
            SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //Animation transition between PlayerBounce(idle) and PlayerWalk(walking)
        if (Mathf.Abs (rigid.velocity.x) < 0.3 )
            ani.SetBool("PlayerWalk", false);
        else
            ani.SetBool("PlayerWalk", true);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movements by pressing arrow keys
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > MaxSpeed) //maximum speed of the right side
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < MaxSpeed * (-1)) //maximum speed of the left side
            rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);
    }

    //Making coins disappear when player approaches
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }
        
    }

}
