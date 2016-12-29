using UnityEngine;
using UnityEngine.UI;

public class ListeActionsTextManager : MonoBehaviour {

    public Text _actionsText = null;
    public GameState _gS = 0;

	// Use this for initialization
	void Start () {
        _gS = GameTurnManager._actualGameState;
        UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
	    if(_gS != GameTurnManager._actualGameState)
        {
            _gS = GameTurnManager._actualGameState;
            UpdateText();
        }
	}


    void UpdateText()
    {
        switch (_gS)
        {
            default:
                Debug.Log("Cas de texte non gérer");
                break;
            case GameState.ChoisirAventurier:
                _actionsText.text = "-Piocher une carte aventurier  -Choisir une carte aventurier   -Regarder les cartes défaussées";
                break;
            case GameState.PreparerDonjon:
                _actionsText.text = "-Piocher une carte donjon      -Résoudre  le combat    -Regarder les cartes défaussées";
                break;
            case GameState.Victoire:
                _actionsText.text = "-Finir le tour    -Regarder cartes défausses";
                break;
            case GameState.Defaite:
                _actionsText.text = "-Vendre carte (Double click)   -Finir le tour   -Regarder cartes défausses";
                break;
            case GameState.FinDeTour:
                _actionsText.text = "";
                break;
            case GameState.CombatCritique1:
                _actionsText.text = "";
                break;
            case GameState.CombatCritique2:
                _actionsText.text = "";
                break;
        }
    }
}
