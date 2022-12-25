using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{        
    public string sLevelToLoad;
    public string currentLevelIndex;
    private Animator animTransition;

    private void Awake()
    {
        animTransition = GetComponentInParent<Animator>();      
    }

    private void OnTriggerEnter2D(Collider2D collision)             
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "HitBox")
        {
            FadeToLevel(currentLevelIndex);
        }   
    }

    public void FadeToLevel(string currentLevelIndex)        
    {
        animTransition.SetTrigger("FadeOut");
    }

    public void LoadScene()     
    {
        SceneManager.LoadScene(sLevelToLoad);
    }
}
