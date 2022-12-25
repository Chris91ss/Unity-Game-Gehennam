using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;


    public string LoadMENU;
    private GameObject[] destroy_player;
    private GameObject[] destroy_camera;
    private GameObject[] DestroyHealthBar;

    public Image img;
    public GameObject victoryAchieved;
    public GameObject ThanksText;
    public GameObject scoreTextObject;
    public GameObject mainMenuButton;
    public GameObject quitbutton;

    public void Setup(int score)
    {
        victoryAchieved.SetActive(true);
        ThanksText.SetActive(true);
        scoreTextObject.SetActive(true);
        mainMenuButton.SetActive(true);
        quitbutton.SetActive(true);
        img.enabled = true;
        scoreText.text = "Score " +  score.ToString();
        DestroyHealthBar = GameObject.FindGameObjectsWithTag("HealthBar");
    }


    public void LoadMenuButtonGameOver()
    {
        SceneManager.LoadScene(LoadMENU);   
        destroy_camera = GameObject.FindGameObjectsWithTag("MainCamera");
        destroy_player = GameObject.FindGameObjectsWithTag("Player");
        Destroy(destroy_camera[0]);   
        Destroy(destroy_player[0]);       
        Destroy(DestroyHealthBar[0]);    
    }

    public void QuitGameButtonGameOver()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();        
    }
}
