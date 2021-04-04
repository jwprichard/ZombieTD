using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI RoundText;
    public TextMeshProUGUI TimeText;

    public void UpdateCash(int money)
    {
        MoneyText.text = money.ToString();
    }

    public void UpdateRound(int round)
    {
        RoundText.text = "Round: " + round.ToString();
    }

    public void UpdateTime(float time)
    {
        TimeText.text = "Time: " + time.ToString();
    }

    public void GameOver()
    {
        GameOverText.SetText("!GAME OVER!");
    }
    
}
