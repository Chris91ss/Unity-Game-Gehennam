using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy_AI enemyParent;   

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_AI>();  
    }

    private void OnTriggerEnter2D(Collider2D collider)    
    {
        if(collider.gameObject.CompareTag("PlayerHitBox")) 
        {
            gameObject.SetActive(false);   
            enemyParent.target = collider.transform;  
            enemyParent.inRange = true;   
            enemyParent.hotZone.SetActive(true);  
        }
    }
}
