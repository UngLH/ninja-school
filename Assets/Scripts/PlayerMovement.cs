using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator amin;
    public Heartbar hearthBar;
    float dirX;
    private bool                m_rolling = false;
    private bool m_attacking = false;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    [SerializeField] private float moveSpeed = 4.5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float m_rollForce = 6.0f;
    public int maxHealth = 100;
    public int currentHealth;
    private enum MovementState { idle, running, jumping, failing, rolling }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        amin = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        hearthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        m_attacking = amin.GetCurrentAnimatorStateInfo(0).IsName("Player_attack1") ||
                      amin.GetCurrentAnimatorStateInfo(0).IsName("Player_attack2") || 
                      amin.GetCurrentAnimatorStateInfo(0).IsName("Player_attack3") || 
                      amin.GetCurrentAnimatorStateInfo(0).IsName("Player_blocking");
        dirX = Input.GetAxisRaw("Horizontal");
        if(m_attacking)
            moveSpeed = 0.0f;
        else
            moveSpeed = 6.0f;
        rb.velocity = new Vector2(dirX*moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown("w") && IsGrounded() && !amin.GetCurrentAnimatorStateInfo(0).IsName("Player_rolling") ) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        
        updateAnimationMove();
        Debug.Log(m_attacking);
    }

    private void updateAnimationMove() {
        MovementState state;
        if(dirX > 0)
        {
            sprite.flipX = false;
            state = MovementState.running;
        }
        else if ( dirX < 0 )
        {
            sprite.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if( rb.velocity.y > .1f) 
        {
            state = MovementState.jumping;
        }
        else if ( rb.velocity.y < -.1f )
        {
            state = MovementState.failing;
        }

        if(Input.GetKeyDown("l"))
        {
            m_rolling = true;
            rb.velocity = new Vector2(dirX*m_rollForce, rb.velocity.y);
            state = MovementState.rolling;
        }

        amin.SetInteger("stateMove", (int)state);
    }

    private bool IsGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
