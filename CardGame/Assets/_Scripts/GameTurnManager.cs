using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameState
{
    None = 0,
    ChoisirAventurier = 1,
    PreparerDonjon = 2,
    Combattre = 3,
    Victoire = 4,
    Defaite = 5,
    FinDeTour = 6,
    CombatCritique1 = 7,
    CombatCritique2 = 8,
    PartieGagné = 9,
    PartiePerdu = 10
}
//C'est la classe qui gère les tours de jeu, et les actions possibles par le joueur
public class GameTurnManager : MonoBehaviour {

    //Etat du tour de jeu actuel
    public static GameState _actualGameState = 0;
    public Text _gameStateText = null;

    void Start () {
        ChangeState(GameState.ChoisirAventurier) ;
        _gameStateText.text = "" +_actualGameState;
    }

    void Update()
    {
        if(_gameStateText.text != "" + _actualGameState)
        {
            _gameStateText.text = "" + _actualGameState;
        }
    }

    //Fonction qui permet de changer d'état le jeu, puis et gérer les actions possibles du joueur
    public static void ChangeState(GameState gs)
    {
        _actualGameState = gs;
    }
}
