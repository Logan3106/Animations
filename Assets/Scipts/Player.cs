using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    public bool touchingPlatform;
    private Animator anim;
    bool isJumping;
    bool attackReady;
    Player playerScript;
    public Transform attackpoint;
    public int attackDamage = 20;
    public float attackRange = 1f;
    HelperScript helper;
    public GameObject range;
    int ammo;

    // Start is called before the first frame update
    void Start()
    {

       ammo = 10;
        

        helper = gameObject.AddComponent<HelperScript>();
        rb = GetComponent<Rigidbody2D>();
        touchingPlatform = false;
        anim = GetComponent<Animator>();
        {

            playerScript = GetComponent<Player>();
            attackReady = false;


        }
      
    }

    // Update is called once per frame
    void Update()
    {

        CheckForLanding();
        anim.SetBool("walk", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Attack", false);
        {
            Attack();

            print("attackready=" + attackReady);
        }
        void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("attacking");
                attackReady = false;
            }
        }
        void OnTriggerStay2D(Collider2D col)
        {
            if ((col.gameObject.tag == "enemy") && attackReady)
            {
               Enemy enemyScript = col.gameObject.GetComponent<Enemy>();

                enemyScript.TakeDamage(attackDamage);
                attackReady=false;
                print("collision with " + col.gameObject.tag);
            }
        }

        {
            Vector2 vel = rb.velocity;


            if (Input.GetKeyDown(KeyCode.Space) && (touchingPlatform == true))
            {
                isJumping = true;

                vel.y = 40;
                anim.SetBool("Jump", true);
                anim.SetBool("walk", false);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                vel.y = -20;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //rb.velocity = new Vector2(-10, 0);
                vel.x = -20;
                anim.SetBool("walk", true);
                helper.FlipObject(true);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //rb.velocity = new Vector2(10, 0);
                vel.x = 20;
                anim.SetBool("walk", true);
                helper.FlipObject(false);
            }
            int moveDirection = 1;
            if (ammo > 0)
            {
                if (Input.GetMouseButtonDown(1))
                            {
                                // Instantiate the bullet at the position and rotation of the player
                                GameObject clone;
                                clone = Instantiate(range, transform.position, transform.rotation);

                                // get the rigidbody component
                                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

                                // set the velocity
                                rb.velocity = new Vector3(15 * moveDirection, 0, 0);

                                // set the position close to the player
                                rb.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + 1);

                                Thread.Sleep(1);
                                Destroy(range, 0f);
                                ammo--;
                            }
            }
            

            if (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0))
            {

                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            rb.velocity = vel;
        }

        if (isJumping == true)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
        {
            Attack();

            print("attackready=" + attackReady);
        }

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            touchingPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            touchingPlatform = false;
        }
    }

    //check for player landing on platform
    void CheckForLanding()
    {
        if ((isJumping == true) && touchingPlatform && rb.velocity.y <= 0)
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
        }
    }
    
    
    public void StartOfAttack()
    {
        attackReady = true;
    }
    public void EndOfAttack()
    {
        attackReady = false;
    }
}

