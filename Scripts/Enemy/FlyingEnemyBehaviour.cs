using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBehaviour : MonoBehaviour
{
    public float speed, circleRadius;     

    private Rigidbody2D enemyRB; 
    public GameObject rightCheck, roofCheck, groundCheck;  
    public LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, rightTouch;   
    public float dirX = 1, dirY = 0.25f;       


    public EnemyReceivingDamage EnemyHealthScript; 
    public GameObject HealthBarCanvas;

    [SerializeField] private AudioClip EnemyDied;
    private Animator anim;     
    public KnockBackEnemy KnockBackStun;
    private float InitialMoveSpeed; 

    private Health_Player playerCurrentHealth;  

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InitialMoveSpeed = speed;
        playerCurrentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();
    }

    void Update()
    {
        enemyRB.velocity = new Vector2(dirX, dirY) * speed; 
        HitDetection();

        if(EnemyHealthScript.currentHealth <= 0)     
        {
            anim.Play("Enemy_Died");
            playerCurrentHealth.score += 25; 
            this.enabled = false;
            HealthBarCanvas.SetActive(false);
            SoundManager.instanceSound.PlaySound(EnemyDied);
        }

        /// logica pentru stun si knockback;
        if(KnockBackStun.stunTime <= 0)         
        {
            speed = InitialMoveSpeed;
        }
        else
        {
            KnockBackStun.stunTime -= Time.deltaTime;
            speed = 0;

            if(KnockBackStun.PlayerWeaponScript.KnockBackDirEnemyY == -1)
                enemyRB.velocity = new Vector2(0, -KnockBackStun.knockbackSpeedY);
            else if(KnockBackStun.PlayerWeaponScript.KnockBackDirEnemyY == 1)
                enemyRB.velocity = new Vector2(0, KnockBackStun.knockbackSpeedY);
            else
                enemyRB.velocity = new Vector2(KnockBackStun.knockbackSpeedX * KnockBackStun.PlayerWeaponScript.KnockBackDirEnemyX, 0);
        }
    }

    private void HitDetection()   
    {
        rightTouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle(roofCheck.transform.position, circleRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        HitLogic();
    }

    private void HitLogic()    
    {
        if(rightTouch && facingRight)
        {
            Flip();
        }
        else if(rightTouch && !facingRight)
        {
            Flip();
        }
        if(roofTouch)
        {
            dirY = -0.25f;
        }
        else if(groundTouch)
        {
            dirY = 0.25f;
        }
    }

    private void Flip()       
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;
    }

    private void OnDrawGizmosSelected()      
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rightCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(roofCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }
}
