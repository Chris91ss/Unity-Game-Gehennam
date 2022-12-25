using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    private Transform PlayerPosition;
    private Animator PlayerAnimator;
    private Health_Player healthParent;
    private NewPlayerMovement playerMovement;
    private NewCombatAttackManager playerCombat;


    public string LoadMENU;
    private GameObject[] destroy_player;
    private GameObject[] destroy_camera;
    private GameObject[] DestroyHealthBar;

    public Image img;
    public GameObject youDiedText;
    public GameObject scoreTextObject;
    public GameObject mainMenuButton;
    public GameObject restartButton;
    public GameObject quitbutton;

    private void Start()
    {
        PlayerPosition = GameObject.Find("Player_Dante").transform;
        PlayerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        healthParent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Player>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerMovement>();
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombatAttackManager>();
    }

    public void Setup(int score)
    {
        /// activam toate componentele pentru meniu;
        youDiedText.SetActive(true);
        scoreTextObject.SetActive(true);
        mainMenuButton.SetActive(true);
        restartButton.SetActive(true);
        quitbutton.SetActive(true);
        img.enabled = true;
        scoreText.text = "Score " +  score.ToString();  
    }


    public void LoadMenuButtonGameOver()
    {
        SceneManager.LoadScene(LoadMENU);   
        destroy_camera = GameObject.FindGameObjectsWithTag("MainCamera");
        destroy_player = GameObject.FindGameObjectsWithTag("Player");
        DestroyHealthBar = GameObject.FindGameObjectsWithTag("HealthBar");
        Destroy(destroy_camera[0]);  
        Destroy(destroy_player[0]);       
        Destroy(DestroyHealthBar[0]);    
    }

    public void RespawnButton()
    {
        youDiedText.SetActive(false);       
        scoreTextObject.SetActive(false);
        mainMenuButton.SetActive(false);
        restartButton.SetActive(false);
        quitbutton.SetActive(false);
        img.enabled = false;
        healthParent.currentHealth = 100;   
        healthParent.HasDied = false;      
        healthParent.diedBySpikes = false;
        healthParent.TakeDamageOnCollision(0);  
        playerMovement.enabled = true;          
        playerCombat.enabled = true;
        healthParent.enabled = true;
        PlayerAnimator.SetTrigger("hasRespawned");      
        PlayerPosition.transform.position = GameObject.FindWithTag("StartPos").transform.position;  
    }

    public void QuitGameButtonGameOver()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();        
    }

}
