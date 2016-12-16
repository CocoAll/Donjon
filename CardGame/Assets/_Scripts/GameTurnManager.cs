using UnityEngine;
using System.Collections;


//C'est la classe qui gère les tours de jeu, et les actions possibles par le joueur
public class GameTurnManager : MonoBehaviour {

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
        CombatCritique2 = 8
    }


    //Etat du tour de jeu actuel
    public static GameState _actualGameState = 0;

    //Actions possibles par le Joueur
    public static bool _canDrawDonjonCard = false;
    public static bool _canDrawAventurierCard = false;
    public static bool _canResolveBattle = false;
    public static bool _canChooseAventurierCard = false;
    public static bool _canAtivateEffectOfDonjonCard = false;
    public static bool _canSellDonjonCard = false;

    void Start () {
        ChangeState(GameState.ChoisirAventurier) ;
	}
	
	void Update () {
	
	}

    //Fonction qui permet de changer d'état le jeu, puis et gérer les actions possibles du joueur
    public void ChangeState(GameState gs)
    {
        _actualGameState = gs;
        switch (_actualGameState)
        {
            default:
                Debug.Log("Cas non gérer");
                break;
            case GameState.ChoisirAventurier:
                break;
            case GameState.PreparerDonjon:
                break;
            case GameState.Combattre:
                break;
            case GameState.Victoire:
                break;
            case GameState.Defaite:
                break;
            case GameState.FinDeTour:
                break;
            case GameState.CombatCritique1:
                break;
            case GameState.CombatCritique2:
                break;
        }
    }

    //*********************************************************************************//
    //Fonctions qui gèrent les booleens 
    //*********************************************************************************//

}
