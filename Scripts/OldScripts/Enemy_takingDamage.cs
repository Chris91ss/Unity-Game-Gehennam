using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_takingDamage : MonoBehaviour
{
    [SerializeField] private int startingHealthEnemy = 100;
    public int currentHealthEnemy; 
    public HealthBarEnemy healthparentEnemy;

    public bool Enemyisalive = true;

    public ParticleSystem Hit_Enemy_Particles;

    private void Start()
    {
        currentHealthEnemy = startingHealthEnemy;
        healthparentEnemy.setMaxHealthEnemy(currentHealthEnemy);
    }


    public void TakeDamageEnemy(int damage)
    {
        currentHealthEnemy -= damage;
        if(currentHealthEnemy > 0)
        {
        Hit_Enemy_Particles.Play();
        healthparentEnemy.setHealthEnemy(currentHealthEnemy);
        }
        if(currentHealthEnemy <= 0)
            Enemyisalive = false;
    }

}
