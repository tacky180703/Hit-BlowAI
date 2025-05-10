using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int turnIndex = 0;
    private int guessCandidateIndex = 0;
    private int[] selectNums = new int[] { 0, 0, 0 };
    private int playerNum = 0;

    [SerializeField]
    private JudgeSystem judgeSystem;

    [SerializeField]
    private AIGuessSystem aIGuessSystem;

    [SerializeField]
    private TextMeshProUGUI playerNumText;

    [SerializeField]
    private Transform PlayerGuessResultTextParent;
    private TextMeshProUGUI[] playerGuessResultTexts;

    [SerializeField]
    private Transform PlayerGuessNumTextParent;
    private TextMeshProUGUI[] playerGuessNumTexts;

    [SerializeField]
    private Transform PlayerNumButtonParent;
    private Button[] playerNumButtons;

    void Start()
    {
        playerGuessResultTexts = PlayerGuessResultTextParent.GetComponentsInChildren<TextMeshProUGUI>();
        playerGuessNumTexts = PlayerGuessNumTextParent.GetComponentsInChildren<TextMeshProUGUI>();
        playerNumButtons = PlayerNumButtonParent.GetComponentsInChildren<Button>();
        for (int i = 0; i < playerNumButtons.Length; i++)
        {
            int index = i;
            playerNumButtons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index)
    {
        playerNumButtons[index].interactable = false;
        Debug.Log((index + 1) + "を選択");
        switch (guessCandidateIndex)
        {
            case 0:
                selectNums[0] = index + 1;
                guessCandidateIndex++;
                break;
            case 1:
                selectNums[1] = index + 1;
                guessCandidateIndex++;
                break;
            case 2:
                selectNums[2] = index + 1;
                StartNextTurn();
                break;
            default:
                Debug.Log("予測中の桁用変数エラー");
                break;
        }
    }

    private void StartNextTurn()
    {
        //初回ターン/それ以降のターン処理分岐
        if (turnIndex != 0)
        {
            playerGuessNumTexts[turnIndex - 1].text = $"{selectNums[0]}{selectNums[1]}{selectNums[2]}";
            (int hit, int blow) = judgeSystem.JudgePlayerNums(selectNums);
            playerGuessResultTexts[turnIndex - 1].text = $"{hit}-{blow}";
            aIGuessSystem.Guess(turnIndex);
        }
        else
        {
            judgeSystem.SetPlayerCorrectNum(selectNums);
            playerNumText.text = $"{selectNums[0]}{selectNums[1]}{selectNums[2]}";
        }
        //共通のターン終了処理
        turnIndex++;
        guessCandidateIndex = 0;
        for (int i = 0; i < selectNums.Length; i++)
        {
            playerNumButtons[selectNums[i] - 1].interactable = true;
            selectNums[i] = 0;
        }
    }
}
