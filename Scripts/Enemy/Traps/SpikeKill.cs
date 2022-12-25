using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeKill : MonoBehaviour
{
    private Health_Player HealthParent;             
    private EnemyReceivingDamage EnemyHealthScript;
    private GameOverScreen gameOverScript;

    private void Start()
    {
        HealthParent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();            
        gameOverScript = GameObject.FindGameObjectWithTag("GameOverMenu").GetComponent<GameOverScreen>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerHitBox"))      
        {
            HealthParent.diedBySpikes = true;           
            HealthParent.TakeDamageOnCollision(HealthParent.currentHealth);    
            gameOverScript.Setup(HealthParent.score);  
        }
        if(collider.gameObject.CompareTag("Enemy"))    
        {
            EnemyHealthScript = collider.gameObject.GetComponent<EnemyReceivingDamage>();  
            EnemyHealthScript.Damage(EnemyHealthScript.currentHealth);     
        }
    }
}
