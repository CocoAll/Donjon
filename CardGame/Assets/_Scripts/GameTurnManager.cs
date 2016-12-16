using UnityEngine;
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
    CombatCritique2 = 8
}
//C'est la classe qui gère les tours de jeu, et les actions possibles par le joueur
public class GameTurnManager : MonoBehaviour {




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

    //Fonction qui permet de changer d'état le jeu, puis et gérer les actions possibles du joueur
    public static void ChangeState(GameState gs)
    {
        _actualGameState = gs;
        switch (_actualGameState)
        {
            default:
                Debug.Log("Cas non gérer");
                break;
            case GameState.ChoisirAventurier:
                SetChoisirAventurier();
                break;
            case GameState.PreparerDonjon:
                SetPreparerDonjon();
                break;
            case GameState.Combattre:
                SetCombattre();
                break;
            case GameState.Victoire:
                SetVictoire();
                break;
            case GameState.Defaite:
                SetDefaite();
                break;
            case GameState.FinDeTour:
                SetFinDeTour();
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

    private static void SetChoisirAventurier()
    {
        _canDrawDonjonCard = false;
        _canDrawAventurierCard = true;
        _canResolveBattle = false;
        _canChooseAventurierCard = true;
        _canAtivateEffectOfDonjonCard = false;
        _canSellDonjonCard = false;
    }

    private static void SetPreparerDonjon()
    {
        _canDrawDonjonCard = true;
        _canDrawAventurierCard = false;
        _canResolveBattle = true;
        _canChooseAventurierCard = false;
        _canAtivateEffectOfDonjonCard = true;
        _canSellDonjonCard = false;
    }

    private static void SetCombattre()
    {
        _canDrawDonjonCard = false;
        _canDrawAventurierCard = false;
        _canResolveBattle = false;
        _canChooseAventurierCard = false;
        _canAtivateEffectOfDonjonCard = false;
        _canSellDonjonCard = false;
    }

    private static void SetVictoire()
    {
        _canDrawDonjonCard = false;
        _canDrawAventurierCard = false;
        _canResolveBattle = false;
        _canChooseAventurierCard = false;
        _canAtivateEffectOfDonjonCard = false;
        _canSellDonjonCard = false;
    }

    private static void SetDefaite()
    {
        _canDrawDonjonCard = false;
        _canDrawAventurierCard = false;
        _canResolveBattle = false;
        _canChooseAventurierCard = false;
        _canAtivateEffectOfDonjonCard = false;
        _canSellDonjonCard = true;
    }

    private static void SetFinDeTour()
    {
        _canDrawDonjonCard = false;
        _canDrawAventurierCard = false;
        _canResolveBattle = false;
        _canChooseAventurierCard = false;
        _canAtivateEffectOfDonjonCard = false;
        _canSellDonjonCard = false;
    }
}
