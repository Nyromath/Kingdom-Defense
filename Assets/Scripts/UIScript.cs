using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    //declaring variables
    float score;
    float hp;
    bool gameOver;
    TextMeshProUGUI uitext;

    private void Start()
    {
        //initializing variables
        uitext = gameObject.GetComponent<TextMeshProUGUI>();
        score = 0;
        hp = 10;
        gameOver = false;
    }

    private void Update()
    {
        if (!gameOver)
        {
            //displays current score and castle HP on the UI
            uitext.text = "Score: " + score.ToString() + "\nCastleHP: " + hp.ToString();
        }
        else
        {
            //displays game over message and final score on the UI
            uitext.alignment = TextAlignmentOptions.Midline;
            uitext.text = "GAME OVER\nScore: " + score.ToString();
        }
    }

    //called by PlayerScript when goblin is killed
    public void ChangeScore()
    {
        score += 1;
    }

    //called by DefenseScript when damage is taken
    public void ChangeHP(float damage)
    {
        hp -= damage;
    }

    //called by DefenseScript when HP is 0 to trigger game over
    public void GameOver()
    {
        gameOver = true;
    }
}
