using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private float movementInputDirection;      
    private int amountOfJumpsLeft;    
    public int facingDirection = 1;   
    private int lastWallJumpDirection;  

    public bool isFacingRight = true;   
    public bool isGrounded;             
    private bool isWalking;           
    private bool isTouchingWall;        
    private bool isWallSliding;         
    private bool canNormalJump;         
    private bool canWallJump;           
    private bool isAttemptingToJump;    
    private bool checkJumpMultiplier;   
    private bool canMove;               
    private bool canFlip;              
    private bool hasWallJumped;         

    private Rigidbody2D rb;           
    private Animator anim;          

    public int amountOfJumps = 1;       

    public float movementSpeed = 10.0f;    
    public float jumpForce = 16.0f;     
    public float groundCheckRadius;        
    public float wallCheckDistance;        
    public float wallSlideSpeed;           
    public float movementForceInAir;       
    public float airDragMultiplier = 0.95f;   
    public float variableJumpHeightMultiplier = 0.5f;       
    public float wallJumpForce;                             
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;
    public float dashSpeed;                   
    private float dashTime;                 
    public float startDashTime;                
    private int dashDirection;                
    public float DashRate = 1.2f;             
    private float nextDashTime = 0f;          



    public Vector2 wallJumpDirection;          

    public Transform groundCheck;              
    public Transform wallCheck;                

    public LayerMask whatIsGround;              

    public GameObject TriggerCollider;          

    
    public ParticleSystem JumpDust;     
    public ParticleSystem WallJumpDust;      
    public ParticleSystem WallSlideSparks;      

    
    [SerializeField] private AudioClip JumpSound;      
    [SerializeField] private AudioClip WalkSound;      
    [SerializeField] private AudioClip DashSound;       

    private float jumpTimer;  
    private float turnTimer;
    private float wallJumpTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();   
        amountOfJumpsLeft = amountOfJumps;  
        wallJumpDirection.Normalize();      
        dashTime = startDashTime;          
    }

    
    void Update()   
    {
        CheckInput();               
        CheckMovementDirection();   
        UpdateAnimations();        
        CheckIfCanJump();           
        CheckIfWallSliding();      
        CheckJump();               
        ApplyMovement();           
        CheckSurroundings();        
        IsGoingToDash();            
    }


    private void IsGoingToDash()
    {
        if(dashDirection == 0) 
        {
            if(Time.time >= nextDashTime)   
            {
            if(Input.GetButtonDown("Roll") && !isWallSliding && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))  
            {
                if(facingDirection < 0)  
                {
                    dashDirection = 1;
                    anim.SetBool("IsDashing", true);      
                    SoundManager.instanceSound.PlaySound(DashSound);    
                    TriggerCollider.active = false;             
                }
                else if(facingDirection > 0)     
                {
                    dashDirection = 2;
                    anim.SetBool("IsDashing", true);     
                    SoundManager.instanceSound.PlaySound(DashSound); 
                    TriggerCollider.active = false;        
                }

               nextDashTime = Time.time + 1f / DashRate; 
            }
            }
        }
        else          
        {
            if(dashTime <= 0)     
            {
                TriggerCollider.active = true;   
                dashDirection = 0;              
                dashTime = startDashTime;       
                rb.velocity = Vector2.zero;      
            }
            else
            {
                dashTime -= Time.deltaTime;     

                if(dashDirection == 1)  
                {
                    rb.velocity = Vector2.left * dashSpeed;   
                    anim.SetBool("IsDashing", false);
                }
                else if(dashDirection == 2)    
                {
                    rb.velocity = Vector2.right * dashSpeed;   
                    anim.SetBool("IsDashing", false);
                }
            }
        }
    }


    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)   
        {
            isWallSliding = true;    
        }
        else                      
        {
            isWallSliding = false;      
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);  

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);   
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0.01f)  
        {
            amountOfJumpsLeft = amountOfJumps;  
        }

        if (isTouchingWall)                     
        {
            checkJumpMultiplier = false;       
            canWallJump = true;              
        }

        if(amountOfJumpsLeft <= 0)     
        {
            canNormalJump = false;    
        }
        else
        {
            canNormalJump = true;   
        }
      
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)      
        {
            Flip();  
        }
        else if(!isFacingRight && movementInputDirection > 0)   
        {
            Flip();  
        }

        if(movementInputDirection != 0) 
        {
            isWalking = true;  
        }
        else
        {
            isWalking = false;  
        }

        if(isWalking && isGrounded)    
        {
            if(!SoundManager.instanceSound.source.isPlaying)  
                {
                    SoundManager.instanceSound.PlaySound(WalkSound);  
                }
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal"); 

        if (Input.GetButtonDown("Jump"))       
        {
            if(isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))  
            {
                NormalJump();   
            }
            else  
            {
                jumpTimer = jumpTimerSet; 
                isAttemptingToJump = true; 
            }
        }

        if(Input.GetButtonDown("Horizontal") && isTouchingWall)        
        {
            if(!isGrounded && movementInputDirection != facingDirection)  
            {
                canMove = false;    
                canFlip = false; 

                turnTimer = turnTimerSet; 
            }
        }

        if (!canMove)      
        {
            turnTimer -= Time.deltaTime;  

            if(turnTimer <= 0)   
            { 
                canMove = true;   
                canFlip = true; 
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))   
        {
            checkJumpMultiplier = false;     
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);  
        }

    }

    private void CheckJump()
    {
        if(jumpTimer > 0)     
        {
            if(!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection) 
            {
                WallJump();
            }
            else if (isGrounded)         
            {
                NormalJump(); 
            }
        }
       
        if(isAttemptingToJump)   
        {
            jumpTimer -= Time.deltaTime;  
        }

        if(wallJumpTimer > 0)  
        {
            if(hasWallJumped && movementInputDirection == -lastWallJumpDirection)   
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }else if(wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;     
            }
        }
    }

    private void NormalJump()
    {
        if (canNormalJump)      
        {
            CreateDustJump(); 
            SoundManager.instanceSound.PlaySound(JumpSound);     
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  
            amountOfJumpsLeft--;  
            jumpTimer = 0;         
            isAttemptingToJump = false;     
            checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if (canWallJump)   
        {
            CreateDustWallJump();   
            SoundManager.instanceSound.PlaySound(JumpSound);  
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);   
            isWallSliding = false;            
            amountOfJumpsLeft = amountOfJumps; 
            amountOfJumpsLeft--;                
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y); 
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);  
            jumpTimer = 0;                       
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;

        }
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);    
        }
        else if(canMove)  
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);  
        }
        

        if (isWallSliding)    
        {
            if(rb.velocity.y < -wallSlideSpeed)   
            {
                WallSlideSparks.Play();            
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);  
            }
        }
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip)      
        {
            facingDirection *= -1;          
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);   
        }
    }

    private void OnDrawGizmos()         
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);  

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));  
    }

    private void CreateDustJump()         
    {
        JumpDust.Play();
    }

    private void CreateDustWallJump()     
    {
        WallJumpDust.Play();    
    }
    

}
