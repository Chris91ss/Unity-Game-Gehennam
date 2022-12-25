using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider slider;
    public void setMaxHealthEnemy(int healthEnemy)   
    {
        slider.maxValue = healthEnemy;
        slider.value = healthEnemy;
    }
    public void setHealthEnemy(int healthEnemy)   
    {
        slider.value = healthEnemy;               
    }
    
}
