using UnityEngine;
using System.Collections;

public class GameTurnManager : MonoBehaviour {

    public enum GameState
    {
        ChoisirAventurier,
        PreparerDonjon,
        Combattre,
        Victoire,
        Defaite,
        FinDeTour,
        CombatCritique1,
        CombateCritique2
    }

    public GameState _actualGameState;

	void Start () {
        _actualGameState = GameState.ChoisirAventurier;

	}
	
	void Update () {
	
	}
}
