using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class GameController : MonoBehaviour {

    private int _aventurierLevel = 1;
    private int _hazardMaxLevel = 3;
    private int _notorietePoints = 20;
    [SerializeField]
    private Text _notorietePointText = null;
    [SerializeField]
    private Text _aventurierLevelText = null;

    [SerializeField]
    private GameObject _donjonDeckObject = null;
    private DonjonDeckManager _bdm = null;

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

    void Start ()
    {
        _bdm = _donjonDeckObject.GetComponent<DonjonDeckManager>();
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

    //Fonction pour lire le fichier Xml chargé
    //et récupérer le deck de base du donjon
    void LoadDonjonDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            Debug.Log(node.Attributes["name"].Value);
            if(node.Attributes["name"].Value == "Base")
            {
                XmlNodeList cards = node.ChildNodes;
                foreach(XmlNode card in cards)
                {

                    int number = int.Parse(card.Attributes["nombre"].Value);
                    Debug.Log(number);
                    for (int i = 0; i< number; i++)
                    {
                        GameObject cardPrefab = Instantiate(_bdm._cardModel);
                        PlayableCard datas = cardPrefab.GetComponent<PlayableCard>();
                        datas._name = card.Attributes["name"].Value;
                        datas._battleValue = int.Parse(card.Attributes["resistance"].Value);
                        datas._discardPrice = int.Parse(card.Attributes["defausse"].Value);
                        datas._effet = int.Parse(card.Attributes["effet"].Value);
                        _bdm._donjonDeck.Add(cardPrefab);
                    }

                }
            }
        }
        _bdm.ShuffleDeck(_bdm._donjonDeck);

    }

    //Fonction pour lire le fichier Xml chargé
    //et récupérer le deck aventurier
    void LoadAventurierDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            Debug.Log(node.Attributes["name"].Value);
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
                    _ccm._aventurierDeck.Add(cardPrefab);
                    datas2._ddm = this._ccm;
                }

            }
        }
        _ccm.ShuffleDeck(_ccm._aventurierDeck);
    }

    //Permet de vérifier si le joueur peux piocher gratuitement
    public bool CanDrawDonjonCard()
    {
        bool canDraw = false;
        if (_cam._choosenOne == null)
        {
            return canDraw;
        }
        if (_drawnDonjonCard < _cam._choosenOne.GetComponent<AventurierCard>()._maxFreeCards && GameTurnManager._canDrawDonjonCard == true)
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
        foreach (GameObject gO in _pcm._playedCardlist)
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

    public void UpdateAventurierCombatValue()
    {
        ChooseGoodAventurierBattleValue();
        _aventurierBattleValueText.text = "" + _aventurierBattleValue;
    }
}
