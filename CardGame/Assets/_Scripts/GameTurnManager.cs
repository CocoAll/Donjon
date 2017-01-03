using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    None = 0,
    ChoisirAventurier = 1,
    PreparerDonjon = 2,
    Victoire = 3,
    Defaite = 4,
    FinDeTour = 5,
    CombatCritique1 = 6,
    CombatCritique2 = 7,
    PartieGagné = 8,
    PartiePerdu = 9,
    Pause = 10,
    EffetEnCours = 11,
    Regard3 = 12
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
