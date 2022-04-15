using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseScript : MonoBehaviour
{
    //declaring variables
    float maxHP = 10;
    float currentHP;
    public GameObject ui;

    //opts script into event handled by GoblinScript
    private void OnEnable()
    {
        GoblinScript.attacked += Hit;
    }
    private void OnDisable()
    {
        GoblinScript.attacked -= Hit;
    }

    void Start()
    {
        //initialize currentHP variable
        currentHP = maxHP;
    }

    
    void Update()
    {
        //freezes game and calls public GameOver method from UIScript when HP reaches 0
        if(currentHP <= 0)
        {
            Time.timeScale = 0;
            ui.GetComponent<UIScript>().GameOver();
            enabled = false;
        }
    }

    void Hit(float damage)
    {
        //decrements HP and calls ChangeHP method from UIScript when damage from GoblinScript is taken
        currentHP -= damage;
        ui.GetComponent<UIScript>().ChangeHP(damage);
    }
}
