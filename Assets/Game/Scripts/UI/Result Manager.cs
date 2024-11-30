using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public GameObject resultPanel;
    public Text resultText;

    public FightingController[] fightingControllers;
    public OpponentAI[] opponentAIs;


    private void Update()
    {
        foreach(FightingController fightingController in fightingControllers)
        {
            if(fightingController.gameObject.activeSelf  && fightingController.currentHealth <= 0)
            {
                SetResult("You Lose");
                return;
            }
        }

        foreach(OpponentAI opponentAI in opponentAIs)
        {
            if(opponentAI.gameObject.activeSelf && opponentAI.currentHealth <= 0)
            {
                SetResult("You Win");
                return;
            }
        }
    }
    void SetResult(string result)
    {
        resultText.text = result;
        resultPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
