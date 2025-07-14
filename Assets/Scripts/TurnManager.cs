using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState{
    Player,
    Bot,
}

public class TurnManager : MonoBehaviour
{
    public int turnIndex = 0;
    public TurnState turnState = TurnState.Player;
    void Start()
    {

    }

    public void NextTurn()
    {
        turnIndex++;
        // turnState = (turnState == TurnState.Player) ? TurnState.Bot : TurnState.Player;
    }
    void Update()
    {

    }
}
