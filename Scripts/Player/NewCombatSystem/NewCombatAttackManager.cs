using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCombatAttackManager : MonoBehaviour
{
    public float defaultForce = 300;
    public float upwardsForce = 600;
    public float movementTime = .1f;
    private bool meleeAttack;
    private Animator meleeAnimator;

    private Animator anim;
    private NewPlayerMovement character;

    private float  nextAttackTime = 0f;
    public float attackTimeRate = 2f;

    [SerializeField] private AudioClip swordSlashSound;
    [SerializeField] private AudioClip ManYellSound;

    [HideInInspector] public float KnockBackDirEnemyY;
    [HideInInspector] public float KnockBackDirEnemyX;

    private void Start()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<NewPlayerMovement>();
        meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.time >= nextAttackTime) 
        {
        CheckInput();
        }
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Attack") && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Dash") && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("WallSlide"))
        {
            meleeAttack = true;
        }
        else
        {
            meleeAttack = false;
        }
        if (meleeAttack && Input.GetAxis("Vertical") > 0)
        {
            anim.SetTrigger("UpwardMelee");
            meleeAnimator.SetTrigger("UpwardMeleeSwipe");

            nextAttackTime = Time.time + 2f / attackTimeRate;

            SoundManager.instanceSound.PlaySound(swordSlashSound);
            SoundManager.instanceSound.PlaySound(ManYellSound);

            KnockBackDirEnemyY = 1;
        }
        if (meleeAttack && Input.GetAxis("Vertical") < 0 && !character.isGrounded)
        {
            anim.SetTrigger("DownwardMelee");
            meleeAnimator.SetTrigger("DownwardMeleeSwipe");

            nextAttackTime = Time.time + 2f / attackTimeRate;

            SoundManager.instanceSound.PlaySound(swordSlashSound);
            SoundManager.instanceSound.PlaySound(ManYellSound);

            KnockBackDirEnemyY = -1;
        }
        if ((meleeAttack && Input.GetAxis("Vertical") == 0)
            || meleeAttack && (Input.GetAxis("Vertical") < 0 && character.isGrounded))
        {
            anim.SetTrigger("ForwardMelee");
            meleeAnimator.SetTrigger("ForwardMeleeSwipe");

            nextAttackTime = Time.time + 2f / attackTimeRate;

            SoundManager.instanceSound.PlaySound(swordSlashSound);
            SoundManager.instanceSound.PlaySound(ManYellSound);

            KnockBackDirEnemyY = 0;
            KnockBackDirEnemyX = character.facingDirection;
        }
    }
}
