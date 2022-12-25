using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float  nextAttackTime = 0f;
    public float attackTimeRate = 2f;

    private Animator animPlayer;
    [SerializeField] private AudioClip swordSlashSound;
    [SerializeField] private AudioClip ManYellSound;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    void Awake()
    {
        animPlayer = GetComponent<Animator>();    
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {   
            if(Input.GetKey(KeyCode.E) && !this.animPlayer.GetCurrentAnimatorStateInfo(0).IsName("Player_Dash"))
            {
                SoundManager.instanceSound.PlaySound(swordSlashSound);
                SoundManager.instanceSound.PlaySound(ManYellSound);
                animPlayer.SetTrigger("isAttacking");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy_takingDamage>().TakeDamageEnemy(damage);
                    enemiesToDamage[i].GetComponent<KnockBackEnemy>().KnockBack(); 
                }
                nextAttackTime = Time.time + 2f / attackTimeRate;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPos == null)
            return;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
