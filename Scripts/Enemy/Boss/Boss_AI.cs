using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour
{
    public float attackDistance;   
    public float moveSpeed;        
    public float timer;           
    public Transform leftLimit;   
    public Transform rightLimit;
    [HideInInspector] public Transform target;  
    [HideInInspector] public bool inRange;      
    public GameObject hotZone;    
    public GameObject triggerArea;  
    public GameObject weaponCollider;
    public EnemyReceivingDamage EnemyHealthScript;
    public GameObject HealthBarCanvas;

    [SerializeField] private AudioClip EnemyAttack;
    [SerializeField] private AudioClip EnemyRangeAttack;
    [SerializeField] private AudioClip EnemyBurn;
    [SerializeField] private AudioClip EnemyScream;
    private bool hasPlayed = false;

    private int random; 


    
    private Animator anim;      
    private float distance;    
    private bool attackMode;   
    private bool cooling;      
    private float intTimer;    

    private Health_Player playerCurrentHealth;  
    private VictoryScreen VictoryAchieved;      

        
    void Awake()
    {
        SelectTarget();   
        intTimer = timer;  
        anim = GetComponent<Animator>();  
        playerCurrentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();
        VictoryAchieved = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryScreen>();
    }
   

    void Update()
    {
        if(!attackMode)
        {
            Move();
        }

        if(!InsideofLimits() && !inRange && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Range_Attack")))       
        {
            SelectTarget();
        }

        if(inRange)
        {
            EnemyLogic();
        }
        if(EnemyHealthScript.currentHealth <= 0)
        {
            anim.Play("Enemy_Died");
            playerCurrentHealth.score += 2500; 
            this.enabled = false;
            weaponCollider.SetActive(false);
            HealthBarCanvas.SetActive(false);
            SoundManager.instanceSound.PlaySound(EnemyBurn);
            SoundManager.instanceSound.PlaySound(EnemyScream);
            VictoryAchieved.Setup(playerCurrentHealth.score);
        }
    }



    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);  

        if(distance > attackDistance)   
        {
            StopAttack();
        }
        else if(attackDistance - 0.05f >= distance && cooling == false && playerCurrentHealth.currentHealth > 0)   
        {
            random = Random.Range(0, 2);
            if(random == 0)
            {
                Attack();
            }
            else
            {
                RangeAttack();
            }
        }

        if(cooling)
        {
            attackMode = true;
            Cooldown();
            anim.SetBool("Attack", false);   
            anim.SetBool("RangeAttack", false);
            weaponCollider.SetActive(false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Range_Attack"))   
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);  
        }
    }

    void Attack()
    {
        weaponCollider.SetActive(true);

        timer = intTimer; 
        attackMode = true; 
        
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
        if(hasPlayed == false)
        {
        SoundManager.instanceSound.PlaySound(EnemyAttack);
        hasPlayed = true;
        }
    }


    void RangeAttack()
    {
        weaponCollider.SetActive(true);

        timer = intTimer; 
        attackMode = true; 
        
        anim.SetBool("canWalk", false);
        anim.SetBool("RangeAttack", true);
        if(hasPlayed == false)
        {
        SoundManager.instanceSound.PlaySound(EnemyRangeAttack);
        hasPlayed = true;
        }
    }


    void Cooldown()             
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            attackMode = false;
            cooling = false;
            hasPlayed = false;
            timer = intTimer;
        }
    }

    void StopAttack()        
    {
        weaponCollider.SetActive(false);

        anim.SetBool("Attack", false);
        anim.SetBool("RangeAttack", false);
        hasPlayed = false;
    }

    public void TriggerCooling()
    {
        cooling = true;
    }


    private bool InsideofLimits()    
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;   
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);   
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);  

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        if(EnemyHealthScript.currentHealth > 0) 
        {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
        }
    }
}
