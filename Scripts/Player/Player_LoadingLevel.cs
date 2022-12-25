using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_LoadingLevel : MonoBehaviour
{
    private GameObject[] players;    

    public GameObject CanvasHealthBar;  
    private GameObject[] HealthBARS;    
    public GameObject keepCamera;   
    private GameObject[] Cameras;   
    public GameObject GameOVERScreen;
    private GameObject[] GOScreens;


    
    void Start()
    {
        DontDestroyOnLoad(CanvasHealthBar);
        DontDestroyOnLoad(gameObject);     
        DontDestroyOnLoad(keepCamera);  
        DontDestroyOnLoad(GameOVERScreen);
    }


    private void OnLevelWasLoaded(int level)
    {
            FindStartPos();
            Cameras = GameObject.FindGameObjectsWithTag("MainCamera"); 
            players = GameObject.FindGameObjectsWithTag("Player");  
            HealthBARS = GameObject.FindGameObjectsWithTag("HealthBar");
            GOScreens = GameObject.FindGameObjectsWithTag("GameOverMenu");

            if (players.Length > 1)  
            {
                Destroy(players[0]);
            }

            if(Cameras.Length > 1)  
            {
                Destroy(Cameras[0]);
            }

            if(HealthBARS.Length > 1)  
            {
                Destroy(HealthBARS[0]);
            }

            if(GOScreens.Length > 1)  
            {
                Destroy(GOScreens[0]);
            }
    }

    void FindStartPos()   
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
