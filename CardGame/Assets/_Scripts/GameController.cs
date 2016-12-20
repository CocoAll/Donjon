using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class GameController : MonoBehaviour {

    private int _aventurierLevel = 1;
    private int _aventurierMaxLevel = 3;
    private int _notorietePoints = 20;
    [SerializeField]
    private Text _notorietePointText = null;
    [SerializeField]
    private Text _aventurierLevelText = null;

    [SerializeField]
    private GameObject _donjonDeckObject = null;
    private DonjonDeckManager _ddm = null;

    [SerializeField]
    private GameObject _aventurierDeckObject = null;
    private AventurierDeckManager _ccm = null;

    [SerializeField]
    private GameObject _playedCardObject = null;
    private PlayedCard _pcm = null;

    private string _xmlPath = null;
    private XmlDocument _xmlDoc = null;

    public Canvas _screen = null;

    public int _drawnDonjonCard = 0;
    [SerializeField]
    private GameObject _choosenAventurierSpot = null;
    private ChoosenAventurier _cam = null;

    [SerializeField]
    private Text _donjonBattleValueText = null;
    [SerializeField]
    private Text _aventurierBattleValueText = null;

    public int _aventurierBattleValue = 0;
    public int _donjonBattleValue = 0;

    public bool _hasKillCritique1 = false;
    public bool _hasKillCritique2 = false;

    void Start ()
    {
        _ddm = _donjonDeckObject.GetComponent<DonjonDeckManager>();
        _ccm = _aventurierDeckObject.GetComponent<AventurierDeckManager>();
        _pcm = _playedCardObject.GetComponent<PlayedCard>();
        _cam = _choosenAventurierSpot.GetComponent<ChoosenAventurier>();

        _xmlPath = Application.dataPath + @"/CardsList.xml";
        _xmlDoc = new XmlDocument();
        if (File.Exists(_xmlPath))
        {
            Debug.Log("J'ai trouvé le ficher");
            _xmlDoc.Load(_xmlPath);
        }

        LoadDonjonDeck();
        LoadAventurierDeck();

        _notorietePointText.text = "" + _notorietePoints;
        _aventurierLevelText.text = "" + _aventurierLevel;


        GameTurnManager.ChangeState(GameState.ChoisirAventurier);
    }

    void Update()
    {
        if(GameTurnManager._actualGameState == GameState.PreparerDonjon)
        {
            UpdateAventurierCombatValue();
        }

        if(_notorietePoints == 0)
        {
            GameTurnManager.ChangeState(GameState.PartiePerdu);
            GameOver();
        }
    }

    //Fonction pour lire le fichier Xml chargé
    //et récupérer le deck de base du donjon
    void LoadDonjonDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            if(node.Attributes["name"].Value == "Base")
            {
                XmlNodeList cards = node.ChildNodes;
                foreach(XmlNode card in cards)
                {

                    int number = int.Parse(card.Attributes["nombre"].Value);
                    for (int i = 0; i< number; i++)
                    {
                        GameObject cardPrefab = Instantiate(_ddm._cardModel);
                        PlayableCard datas = cardPrefab.GetComponent<PlayableCard>();
                        datas._name = card.Attributes["name"].Value;
                        datas._battleValue = int.Parse(card.Attributes["resistance"].Value);
                        datas._discardPrice = int.Parse(card.Attributes["defausse"].Value);
                        datas._effet = int.Parse(card.Attributes["effet"].Value);
                        DonjonDeckManager._donjonDeck.Add(cardPrefab);
                    }

                }
            }
        }
        _ddm.ShuffleDeck(DonjonDeckManager._donjonDeck);

    }

    //Fonction pour lire le fichier Xml chargé
    //et récupérer le deck aventurier
    void LoadAventurierDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            if (node.Attributes["name"].Value == "Confrontation")
            {
                XmlNodeList cards = node.ChildNodes;
                foreach (XmlNode card in cards)
                {
                    GameObject cardPrefab = Instantiate(_ccm._cardModel);
                    PlayableCard datas1 = cardPrefab.GetComponent<PlayableCard>(); ;
                    AventurierCard datas2 = cardPrefab.GetComponent<AventurierCard>();
                    datas1._name = card.Attributes["newCardName"].Value;
                    datas1._battleValue = int.Parse(card.Attributes["resistance"].Value);
                    datas1._discardPrice = int.Parse(card.Attributes["defausse"].Value);
                    datas1._effet = int.Parse(card.Attributes["effet"].Value);
                    datas2._aventurierName = card.Attributes["name"].Value;
                    datas2._level1BattleValue = int.Parse(card.Attributes["nv1"].Value);
                    datas2._level2BattleValue = int.Parse(card.Attributes["nv2"].Value);
                    datas2._level3BattleValue = int.Parse(card.Attributes["nv3"].Value);
                    datas2._maxFreeCards = int.Parse(card.Attributes["pioche"].Value);
                    datas2._aventurierCardSpot = _ccm._aventurierCardSpot;
                    datas2._selectedAventurierCardSpot = _ccm._selectedAventurierCardSpot;
                    AventurierDeckManager._aventurierDeck.Add(cardPrefab);
                    datas2._ddm = this._ccm;
                }

            }
        }
        _ccm.ShuffleDeck(AventurierDeckManager._aventurierDeck);
    }

    //Permet de vérifier si le joueur peux piocher gratuitement
    public bool CanDrawDonjonCard()
    {
        bool canDraw = false;
        if (_cam._choosenOne == null)
        {
            return canDraw;
        }
        if (_drawnDonjonCard < _cam._choosenOne.GetComponent<AventurierCard>()._maxFreeCards && GameTurnManager._actualGameState == GameState.PreparerDonjon)
        {
            canDraw = true;
        }

        return canDraw;
    }


    //Fonction appelé après chaque pioche dans le deck donjon
    public void HasDraw()
    {
        UpdateDonjonCombatValue();
    }

    //Fonction mettant a jour les valeurs pour le combat
    public void UpdateDonjonCombatValue()
    {
        int donjonValue = 0;
        foreach (GameObject gO in PlayedCard._playedCardlist)
        {
            donjonValue += gO.GetComponent<PlayableCard>()._battleValue;
        }

        _donjonBattleValueText.text = "" + donjonValue;
        _donjonBattleValue = donjonValue;

    }

    //Fonction pour prendre la bonne Valeur de combat pour l'aventurier
    public void ChooseGoodAventurierBattleValue()
    {
        switch (_aventurierLevel)
        {
            case 1:
                _aventurierBattleValue = _cam._choosenOne.GetComponent<AventurierCard>()._level1BattleValue;
                break;
            case 2:
                _aventurierBattleValue = _cam._choosenOne.GetComponent<AventurierCard>()._level2BattleValue;
                break;
            case 3:
                _aventurierBattleValue = _cam._choosenOne.GetComponent<AventurierCard>()._level3BattleValue;
                break;
        }
    }

    //Permettre de mettre a jour la valeur de combat de l'aventurier
    public void UpdateAventurierCombatValue()
    {
        ChooseGoodAventurierBattleValue();
        _aventurierBattleValueText.text = "" + _aventurierBattleValue;
    }

    //Fonction permettant de resoudre le combat et de décider 
    //ce que le joueur peut faire ensuite
    public void DoBattleResolution()
    {
        if (GameTurnManager._actualGameState == GameState.PreparerDonjon && _drawnDonjonCard > 0 ) {
            if (_donjonBattleValue >= _aventurierBattleValue)
            {
                GameTurnManager.ChangeState(GameState.Victoire);
                foreach (GameObject gO in PlayedCard._playedCardlist)
                {
                    DonjonDeckManager._donjonDefausseDeck.Add(gO);
                    gO.transform.SetParent(this.transform.parent);
                    gO.transform.position = new Vector3(-100, -100, 0);
                }
                PlayedCard._playedCardlist.Clear();

                DonjonDeckManager._donjonDefausseDeck.Add(_cam._choosenOne);
                _cam._choosenOne.transform.SetParent(this.transform.parent);
                _cam._choosenOne.transform.position = new Vector3(-100, -100, 0);
                _cam._choosenOne = null;

            }
            else if (_donjonBattleValue < _aventurierBattleValue)
            {
                GameTurnManager.ChangeState(GameState.Defaite);
            }
        }
    }

    //Fonction qui gère la fin de tour 
    //Et comment doit se dérouler la suite.
    public void EndOfTurn()
    {
        if (GameTurnManager._actualGameState == GameState.Defaite || GameTurnManager._actualGameState == GameState.Victoire) {
            //Vide le terrain si une défaite a été subit
            if (GameTurnManager._actualGameState == GameState.Defaite)
            {
                foreach (GameObject gO in PlayedCard._playedCardlist)
                {
                    DonjonDeckManager._donjonDefausseDeck.Add(gO);
                    gO.transform.SetParent(this.transform.parent);
                    gO.transform.position = new Vector3(-100, -100, 0);
                }
                PlayedCard._playedCardlist.Clear();

                AventurierDeckManager._aventurierDefausseDeck.Add(_cam._choosenOne);
                _cam._choosenOne.transform.SetParent(this.transform.parent);
                _cam._choosenOne.transform.position = new Vector3(-100, -100, 0);
                _cam._choosenOne = null;
            }

            GameTurnManager.ChangeState(GameState.FinDeTour);

            //Verifie si le deck des aventurier est vide
            if (AventurierDeckManager._aventurierDeck.Count == 0)
            {
                //Si aventurier niveau 3 terminé, on passe aux combat de boss finaux
                if (_aventurierLevel == _aventurierMaxLevel)
                {
                    //Combat 1er Boss, et si deja fait alors 2e
                    if (_hasKillCritique1 == false)
                    {
                        GameTurnManager.ChangeState(GameState.CombatCritique1);
                    }
                    else if (_hasKillCritique2 == false)
                    {
                        GameTurnManager.ChangeState(GameState.CombatCritique2);
                    }
                }
                else if (_aventurierLevel < _aventurierMaxLevel)
                {
                    _aventurierLevel++;
                    _aventurierLevelText.text = "" + _aventurierLevel;
                    _ccm.ResetDeck();
                    GameTurnManager.ChangeState(GameState.ChoisirAventurier);
                }
            } else if (AventurierDeckManager._aventurierDeck.Count != 0)
            {
                _drawnDonjonCard = 0;
                GameTurnManager.ChangeState(GameState.ChoisirAventurier);
            }
        }
    }

    public void GameOver()
    {

    }
}
