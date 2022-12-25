using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.2f;  
    [SerializeField] private Transform[] wayPoints;  
    [SerializeField] public int index;             

    [SerializeField] private float rotSpeed;
    private bool isTurn;


    public EnemyReceivingDamage EnemyHealthScript;
    public GameObject HealthBarCanvas;
    [SerializeField] private AudioClip EnemyDied;
    private Animator anim;    


    private Health_Player playerCurrentHealth;  

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerCurrentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[index].position, moveSpeed * Time.deltaTime);  

        if (isTurn)
            TurnRotation();

        if (Vector2.Distance(transform.position, wayPoints[index].transform.position) <= 0.1f)  
        {
            index++;
            isTurn = true;
            if (index == wayPoints.Length) {
                index = 0;
            }
        }

        if(EnemyHealthScript.currentHealth <= 0)       
        {
            anim.Play("EnemyDied");
            playerCurrentHealth.score += 25;
            this.enabled = false;
            HealthBarCanvas.SetActive(false);
            SoundManager.instanceSound.PlaySound(EnemyDied);
        }
    }

    private void TurnRotation()        
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wayPoints[index].transform.rotation, rotSpeed * Time.deltaTime);
    }
}
