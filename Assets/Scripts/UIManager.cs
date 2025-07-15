using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //数字ボタン
    [SerializeField]
    private Button[] numButtons;

    [SerializeField]
    private GameObject[] bases;

    [SerializeField]
    private TextMeshProUGUI hitCounterText;

    [SerializeField]
    private TextMeshProUGUI blowCounterText;
    [SerializeField]
    private TextMeshProUGUI roundCounterText;
    [SerializeField]
    private TextMeshProUGUI candidateCounterText;

    //数字アイコン画像
    [SerializeField]
    private Sprite[] numIcons; //10番目は空スロット用

    [SerializeField]
    private TurnManager turnManager;
    [SerializeField]
    private JudgeSystem judgeSystem;

    public void SetSlot(int slotIndex, int numIndex)
    {
        Transform slot = bases[turnManager.turnIndex].transform.GetChild(slotIndex);
        Image numIcon = slot.GetComponent<Image>();
        numIcon.sprite = numIcons[numIndex];
    }

    public void ClearSlot(int slotIndex)
    {
        Transform slot = bases[turnManager.turnIndex].transform.GetChild(slotIndex);
        Image numIcon = slot.GetComponent<Image>();
        numIcon.sprite = numIcons[9];
    }

    public void SetResult(int turnIndex)
    {
        int hit = judgeSystem.results[turnIndex, 0];
        int blow = judgeSystem.results[turnIndex, 1];
        int none = judgeSystem.results[turnIndex, 2];

        if ((hit + blow + none) == 3)
        {
            hitCounterText.text = $"{hit}/3";
            blowCounterText.text = $"{blow}/3";
        }
        else
        {
            hitCounterText.text = $"N/3";
            blowCounterText.text = $"N/3";
        }
        roundCounterText.text = $"0{turnIndex + 1}";
    }

    public void SetCandidateCounter(int candidateCount)
    {
        candidateCounterText.text=$"{candidateCount}";
    }
}
