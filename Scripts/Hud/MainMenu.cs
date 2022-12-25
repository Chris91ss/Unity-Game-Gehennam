using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animMenu;

    private void Awake()
    {
        animMenu = GetComponent<Animator>();
    }

    public void PlayGame ()
    {
        Physics2D.IgnoreLayerCollision(0, 7, false);  
        Physics2D.IgnoreLayerCollision(0, 8, false);
        Time.timeScale = 1f;
        animMenu.SetTrigger("FadeOut");
    }


    public void LoadScene()    
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }

}
