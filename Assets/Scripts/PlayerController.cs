using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TurnManager turnManager;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private JudgeSystem judgeSystem;

    private int?[] slotValues;
    void Start()
    {
        slotValues = new int?[3];
    }

    //数字入力に関する処理
    public void OnNumberButtonClicked(int index)
    {
        if (turnManager.turnState == TurnState.Player)
        {
            //使われていればスロットを空ける
            for (int i = 0; i < 3; i++)
            {
                if (slotValues[i] == index)
                {
                    // 削除
                    slotValues[i] = null;
                    uiManager.ClearSlot(i);
                    return;
                }
            }

            //使われてなければ格納する
            for (int i = 0; i < 3; i++)
            {
                //空いてるスロットがあれば値を格納する。
                if (slotValues[i] == null)
                {
                    slotValues[i] = index;
                    uiManager.SetSlot(i, index);
                    return;
                }
            }

            //すでにスロットが全部埋まっていれば
            Debug.Log("すでにスロットが埋まっています。");
        }
    }

    public void ConfirmInput()
    {
        if (IsAllSlotsFilled())
        {
            judgeSystem.Judge(slotValues.Select(x => x.Value).ToArray());
            turnManager.NextTurn();
            slotValues = new int?[3];
            Debug.Log("次のターン");
        }
        else
        {
            Debug.Log("スロットが埋まってません");
        }
    }
    private bool IsAllSlotsFilled()
    {
        foreach (var val in slotValues)
        {
            if (val == null) return false;
        }
        return true;
    }
}
