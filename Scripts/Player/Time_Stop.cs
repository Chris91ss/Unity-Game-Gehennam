using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Stop : MonoBehaviour
{


    private float Speed;
    private bool RestoreTime;

    public ParticleSystem ImpactEffect;

    private void Start()
    {
        RestoreTime = false;
    }

    private void Update()
    {
        if(RestoreTime)
        {
            if(Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * Speed;        
            }
            else                                               
            {
                Time.timeScale = 1f;
                RestoreTime = false;
            }
        }
    }

    public void StopTime(float ChangeTime, int RestoreSpeed, float Delay)   
    {
        Speed = RestoreSpeed;

        if(Delay > 0)
        {
            StopCoroutine(StartTimeAgain(Delay));
            StartCoroutine(StartTimeAgain(Delay));
        }
        else
        {
            RestoreTime = true;
        }

        ImpactEffect.Play();

        Time.timeScale = ChangeTime;
    }

    IEnumerator StartTimeAgain(float amt)
    {
        yield return new WaitForSecondsRealtime(amt);
        RestoreTime = true;
    }
}
