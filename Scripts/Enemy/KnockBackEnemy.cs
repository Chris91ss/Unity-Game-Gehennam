using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEnemy : MonoBehaviour
{
    
    [HideInInspector] public NewPlayerMovement Pscript;
    [HideInInspector] public NewCombatAttackManager PlayerWeaponScript;
    public Rigidbody2D rbKnock;       
    public EnemyReceivingDamage EnemyrecDamage;

    public float knockbackSpeedX, knockbackSpeedY;    

    public float StartStunTime;     
    public float stunTime;          

    void Start()
    {
        Pscript = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerMovement>();               
        PlayerWeaponScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombatAttackManager>();
    }

    public void KnockBack()
    {
        if(EnemyrecDamage.Enemyisalive)     
        {
        if(PlayerWeaponScript.KnockBackDirEnemyY == -1)     
        {
            rbKnock.velocity = new Vector2(0, -knockbackSpeedY);  
            stunTime = StartStunTime;
        }
        else if(PlayerWeaponScript.KnockBackDirEnemyY == 1)       
        {
            rbKnock.velocity = new Vector2(0, knockbackSpeedY);
            stunTime = StartStunTime;
        }
        else                                                      
        {
        rbKnock.velocity = new Vector2(knockbackSpeedX * Pscript.facingDirection, 0);
        stunTime = StartStunTime;
        }
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerAttackCheck"))   
        {
            Debug.Log("KnockBack");
            KnockBack();
        }
    }


}
