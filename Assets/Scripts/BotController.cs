using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TurnManager turnManager;
    [SerializeField]
    private GuessSystem guessSystem;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private JudgeSystem judgeSystem;

    private bool hasGuessed = false;
    void Update()
    {
        if (turnManager.turnState == TurnState.Bot && !hasGuessed)
        {
            hasGuessed = true;
            int[] guess = guessSystem.Guess();
            StartCoroutine(ConfirmGuess(guess));
        }
        if (turnManager.turnState == TurnState.Player)
        {
            hasGuessed = false;
        }
    }

    private IEnumerator ConfirmGuess(int[] guess)
    {
        for (int i = 0; i < 3; i++)
        {
            uiManager.SetSlot(i, guess[i] - 1);
            yield return new WaitForSeconds(0.5f);
        }
        int[] result = judgeSystem.Judge(guess);
        if (result[0] == 3)
        {
            gameManager.FinishGame(guess);
        }
        else
        {
            Debug.Log(result[0] + "hit " + result[1] + "blow");
            guessSystem.UpdateCandidates(guess, result);
            judgeSystem.SaveResult(result);
            turnManager.NextTurn();
        }
    }
}
