using UnityEngine;
using UnityEngine.UI;

public class AventurierCard : MonoBehaviour {

    //Données pour le combats
    public string _aventurierName = "";
   
    public int _level1BattleValue = 0;

    public int _level2BattleValue = 0;

    public int _level3BattleValue = 0;

    public int _maxFreeCards = 0;

    [SerializeField]
    private Text _nameText = null;
    [SerializeField]
    private Text _effetPlayableText = null;
    [SerializeField]
    private Text _battleValueText = null;

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
                foreach (GameObject gO in PlayedCard._playedCardlist)
                {
                    if(gO != this.gameObject)
                    {
                        AventurierDeckManager._aventurierDefausseDeck.Add(gO);
                        gO.transform.SetParent(null);

                    }

                }
                _selectedAventurierCardSpot.GetComponent<ChoosenAventurier>()._choosenOne = this.gameObject;
                this.transform.SetParent(_selectedAventurierCardSpot.transform);
                PlayedCard._playedCardlist.Clear();
                GameTurnManager.ChangeState(GameState.PreparerDonjon);
            }
        }

    }


    public void UpdateAventurierTexts()
    {
        _nameText.text = _aventurierName;
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>()._aventurierLevel == 1)
        {
            _battleValueText.text = "" + _level1BattleValue;
        }
        else if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>()._aventurierLevel == 2)
        {
            _battleValueText.text = "" + _level2BattleValue;
        }
        else
        {
            _battleValueText.text = "" + _level3BattleValue;
        }

        _effetPlayableText.text = "Max draw : " + _maxFreeCards;

    }
}
