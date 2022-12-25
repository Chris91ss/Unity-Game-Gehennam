using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    public int currentHealth;
    public HealthBar healthparent;
    private int OnlyHitOnce = 1;

    [HideInInspector] public bool HasDied = false;
    [HideInInspector] public bool diedBySpikes = false;

    [HideInInspector] public int score = 0;


    public GameOverScreen GameOver;

    private Animator animPlayer;

    [SerializeField] private AudioClip DeathSound;


    private Rigidbody2D rb;


    [SerializeField] private float invulnerabilityDuration;
    [SerializeField] private float numberofFlashes;
    private SpriteRenderer spriteRend;


    private void Start()
    {
        animPlayer = GetComponent<Animator>();
        currentHealth = startingHealth;
        healthparent.SetMaxHealth(currentHealth);
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ///score += (int)Time.time;
    }

    
    public void TakeDamageOnCollision(int _damage)
    {
        if(currentHealth > 0)
        {
        currentHealth -= _damage; 
        healthparent.SetHealth(currentHealth);  
        StartCoroutine(Invulnerability());      
        }
        if(currentHealth <= 0 && !HasDied && !diedBySpikes)
        {
            Die(); 
        }
        else if(currentHealth <= 0 && !HasDied && diedBySpikes)
        {
            DieBySpikes();  
        }
    }

    private void Die()
    {
        currentHealth = 0;
        HasDied = true;
        rb.velocity = new Vector2(0, 0);
        animPlayer.SetTrigger("IsDead");
        SoundManager.instanceSound.PlaySound(DeathSound);
        GetComponent<NewPlayerMovement>().enabled = false;
        GetComponent<NewCombatAttackManager>().enabled = false;
        GameOver.Setup(score);
        this.enabled = false;
    }

    private void DieBySpikes()
    {
        currentHealth = 0;
        HasDied = true;
        rb.velocity = new Vector2(0, 0);
        animPlayer.SetTrigger("IsDead");
        SoundManager.instanceSound.PlaySound(DeathSound);
        GetComponent<NewPlayerMovement>().enabled = false;
        GetComponent<NewCombatAttackManager>().enabled = false;
        GameOver.Setup(score);
        this.enabled = false;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(0, 7, true);      
        Physics2D.IgnoreLayerCollision(0, 8, true);
        for(int i = 0; i < numberofFlashes; i++)
        {
            spriteRend.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSecondsRealtime(invulnerabilityDuration / (numberofFlashes * 2)); 
            spriteRend.color = Color.white;
            yield return new WaitForSecondsRealtime(invulnerabilityDuration / (numberofFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(0, 7, false);    
        Physics2D.IgnoreLayerCollision(0, 8, false);
    }


   
}
