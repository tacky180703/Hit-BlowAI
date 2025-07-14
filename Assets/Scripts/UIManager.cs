using System.Collections;
using System.Collections.Generic;
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

    //数字アイコン画像
    [SerializeField]
    private Sprite[] numIcons; //10番目は空スロット用

    [SerializeField]
    private TurnManager turnManager;


    void Start()
    {

    }

    void Update()
    {

    }

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
}
