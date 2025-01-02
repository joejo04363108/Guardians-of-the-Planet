using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBar : MonoBehaviour{

	public Slider slider;

	public void SetMaxLight(int health)
	{
		slider.maxValue = health;
		slider.value = health;

	}

    public void SetLight(int health)
	{		slider.value = health;

	}
	
    public void addLight(int health)
	{		slider.value += health;

	}
    public void subLight(int health)
	{		slider.value -= health;

	}
	public float getLight(){
		return slider.value;
	}
	
	 void Start()
    {
        InvokeRepeating("RestoreLight", 1f, 1f); // 每 1 秒執行一次
    }

    void RestoreLight()
    {
        if (slider.value < slider.maxValue)
        {
            slider.value += 1; // 每秒增加 1
        }
    }

}