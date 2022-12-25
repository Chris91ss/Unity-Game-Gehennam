using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceivingDamage : MonoBehaviour
{
    [SerializeField]
    private bool damageable = true;
    [SerializeField]
    private int healthAmount = 100;
    public bool giveUpwardForce = true;
    private bool hit;
    public int currentHealth;
    [SerializeField]
    private float invulnerabilityTime = .1f;


    public HealthBarEnemy healthparentEnemy;

    public bool Enemyisalive = true;

    public ParticleSystem Hit_Enemy_Particles;

    private void Start()
    {
        currentHealth = healthAmount; 
        healthparentEnemy.setMaxHealthEnemy(currentHealth);   
    }

    public void Damage(int amount)
    {
        if (damageable && !hit && currentHealth > 0)
        {
            hit = true;
            currentHealth -= amount;     
            Hit_Enemy_Particles.Play();  
            healthparentEnemy.setHealthEnemy(currentHealth);  
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Enemyisalive = false;
                giveUpwardForce = false;
            }
            else   
            {
                StartCoroutine(TurnOffHit());
            }
        }
    }


    private IEnumerator TurnOffHit()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        hit = false;
    }

}
