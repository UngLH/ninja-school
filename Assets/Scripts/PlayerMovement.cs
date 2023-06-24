using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator amin;
    float dirX;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private enum MovementState { idle, running, jumping, failing }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        amin = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        updateAnimationMove();
        
    }

    private void updateAnimationMove() {
        MovementState state;
        if(dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if ( dirX < 0 )
        {
            state = MovementState.running;
            sprite.flipX = true;
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

        amin.SetInteger("stateMove", (int)state);

    }
}
