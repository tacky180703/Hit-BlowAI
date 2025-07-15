using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject result;
    [SerializeField]
    private TextMeshProUGUI winnerText;
    [SerializeField]
    private Image[] slots;
    [SerializeField]
    private Sprite[] numIcons;
    [SerializeField]
    private TurnManager turnManager;

    public void ShowResult(int[] answer)
    {
        result.SetActive(true);
        string winner = (turnManager.turnState == TurnState.Player) ? "Player" : "Bot";
        winnerText.text = $"{winner} WIN!!";

        slots[0].sprite = numIcons[answer[0] - 1];
        slots[1].sprite = numIcons[answer[1] - 1];
        slots[2].sprite = numIcons[answer[2] - 1];
    }
}
