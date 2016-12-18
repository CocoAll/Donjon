using UnityEngine;
using System.Collections;

public class AventurierCard : MonoBehaviour {

    //Données pour le combats
    public string _aventurierName = "";
   
    public int _level1BattleValue = 0;

    public int _level2BattleValue = 0;

    public int _level3BattleValue = 0;

    public int _maxFreeCards = 0;

    //Pour accéder aux informations pour les transitions entre les
    //différents GameObject
    public GameObject _selectedAventurierCardSpot = null;

    public GameObject _aventurierCardSpot = null;

    public AventurierDeckManager _ddm = null;


    //Fonction pour choisir une des carte Aventurier
    //Lorsque deux cartes sont présenté au joueur
    public void ChooseCard()
    {
        if (GameTurnManager._actualGameState == GameState.ChoisirAventurier)
        {

            if (this.transform.parent == _aventurierCardSpot.transform && _selectedAventurierCardSpot.GetComponent<ChoosenAventurier>()._choosenOne == null)
            {
                foreach (GameObject gO in _aventurierCardSpot.GetComponent<PlayedCard>()._playedCardlist)
                {
                    if(gO != this.gameObject)
                    {
                        _ddm._aventurierDefausseDeck.Add(gO);
                        gO.transform.SetParent(null);

                    }

                }
                _selectedAventurierCardSpot.GetComponent<ChoosenAventurier>()._choosenOne = this.gameObject;
                this.transform.SetParent(_selectedAventurierCardSpot.transform);
                _aventurierCardSpot.GetComponent<PlayedCard>()._playedCardlist.Clear();
                GameTurnManager.ChangeState(GameState.PreparerDonjon);
            }
        }

    }


    //***************************************************************************//
    //Accesseurs//
    //***************************************************************************//

    public int GetLevel1BattleValue()
    {
        return _level1BattleValue;
    }

    public int GetLevel2BattleValue()
    {
        return _level2BattleValue;
    }

    public int GetLevel3BattleValue()
    {
        return _level3BattleValue;
    }
}
