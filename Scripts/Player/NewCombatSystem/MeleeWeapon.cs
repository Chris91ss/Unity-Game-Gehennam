using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 20;
    private NewPlayerMovement character;
    private Rigidbody2D rb;
    private NewCombatAttackManager meleeAttackManager;
    private Vector2 direction;
    private bool collided;
    private bool downwardStrike;

    private void Start()
    {
        character = GetComponentInParent<NewPlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
        meleeAttackManager = GetComponentInParent<NewCombatAttackManager>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyReceivingDamage>())
        {
            HandleCollision(collision.GetComponent<EnemyReceivingDamage>());
        }
    }

    private void HandleCollision(EnemyReceivingDamage objHealth)
    {
        if (objHealth.giveUpwardForce && Input.GetAxis("Vertical") < 0 && !character.isGrounded)
        {
            direction = Vector2.up;
            downwardStrike = true;
            collided = true;
        }
        if (Input.GetAxis("Vertical") > 0 && !character.isGrounded)
        {
            direction = Vector2.down;
            collided = true;
        }
        if ((Input.GetAxis("Vertical") <= 0 && character.isGrounded) || Input.GetAxis("Vertical") == 0)
        {
            if (!character.isFacingRight) 
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
            }
            collided = true;
        }
        objHealth.Damage(damageAmount);
        StartCoroutine(NoLongerColliding());
    }
    private void HandleMovement()
    {
        if (collided)
        {
            if (downwardStrike && direction != Vector2.down)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(direction * meleeAttackManager.upwardsForce);
            }
            else if (!downwardStrike && direction != Vector2.down && character.isGrounded)
            {
                rb.AddForce(direction * meleeAttackManager.defaultForce);
            }
        }
    }

    private IEnumerator NoLongerColliding()
    {
        yield return new WaitForSeconds(meleeAttackManager.movementTime);
        collided = false;
        downwardStrike = false;
    }
}
