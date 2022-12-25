using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;             
    private Time_Stop stopTime;
    private Health_Player HealthParent;

    private bool isColliding = false;

    void Start()
    {
        stopTime =  GameObject.FindGameObjectWithTag("Player").GetComponent<Time_Stop>();         
        HealthParent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("DamagePlayer"))
        {
            if(HealthParent.currentHealth > 0)
            {
            Invoke("SetBoolBack", 2);
            if(isColliding) 
                return;

            stopTime.StopTime(0.3f, 10, 0.1f); 
            HealthParent.TakeDamageOnCollision(damage);

            isColliding = true;
            }
        }
    }

    private void SetBoolBack()
    {
     isColliding = false;
    }

}
