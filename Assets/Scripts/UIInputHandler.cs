using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void OnNumberButtonClicked(int index)
    {
        playerController.OnNumberButtonClicked(index);
    }

    public void ConfirmInput()
    {
        playerController.ConfirmInput();
    }
}
