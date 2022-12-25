using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIFplayerCollide : MonoBehaviour
{
    private Time_Stop stopTime;
    private Health_Player damageIfTouchedENEMY;
    public EnemyReceivingDamage ENEMYALIVE;
    public int damage;

    private bool isColliding = false;

    void Start()
    {
       damageIfTouchedENEMY = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();      
       stopTime = GameObject.FindGameObjectWithTag("Player").GetComponent<Time_Stop>();
    }

    private void OnTriggerStay2D(Collider2D collision)   
    {
        if(collision.gameObject.CompareTag("DamagePlayer") && ENEMYALIVE.Enemyisalive)
        {
            if(damageIfTouchedENEMY.currentHealth > 0)
            {
            Invoke("SetBoolBack", 2);
            if(isColliding) 
                return;

            stopTime.StopTime(0.05f, 10, 0.1f);
            damageIfTouchedENEMY.TakeDamageOnCollision(damage);

            isColliding = true;
            }
        }
    }

    private void SetBoolBack()
    {
     isColliding = false;
    }
}
