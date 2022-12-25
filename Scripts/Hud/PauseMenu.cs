using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public string LoadMENU;

    private GameObject[] destroy_player;
    private GameObject[] destroy_camera;
    private GameObject[] DestroyHealthBar;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();            
            }
            else
            {
                Pause();            
            }
        }
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);   
        Time.timeScale = 0f;         
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);  
        Time.timeScale = 1f;          
        GameIsPaused = false;
    }

    public void LoadMenuButton()
    {
        Time.timeScale = 1f;                
        SceneManager.LoadScene(LoadMENU);   
        GameIsPaused = false; 
        destroy_camera = GameObject.FindGameObjectsWithTag("MainCamera");
        destroy_player = GameObject.FindGameObjectsWithTag("Player");
        DestroyHealthBar = GameObject.FindGameObjectsWithTag("HealthBar");
        Destroy(destroy_camera[0]);   
        Destroy(destroy_player[0]);       
        Destroy(DestroyHealthBar[0]);    
    }

    public void QuitGameButton()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();      
    }
}
