using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class GameController : MonoBehaviour {

    public int _aventurierLevel = 1;
    private int _aventurierMaxLevel = 3;

    public int _notorietePoints = 20;
    public Text _notorietePointText = null;
    [SerializeField]
    private Text _aventurierLevelText = null;

    [SerializeField]
    private GameObject _donjonDeckObject = null;
    public DonjonDeckManager _ddm = null;

    [SerializeField]
    private GameObject _aventurierDeckObject = null;
    private AventurierDeckManager _ccm = null;

    [SerializeField]
    private GameObject _usureDeckObject = null;
    private UsureDeckManager _udm = null;

    private string _xmlPath = null;
    private XmlDocument _xmlDoc = null;

    public Canvas _screen = null;

    public int _drawnDonjonCard = 0;

    public  GameObject _choosenAventurierSpot = null;
    public ChoosenAventurier _cam = null;

    [SerializeField]
    private Text _donjonBattleValueText = null;
    public Text _aventurierBattleValueText = null;

    public int _aventurierBattleValue = 0;
    public int _donjonBattleValue = 0;
    public int _donjonBonus = 0;

    public bool _hasKillCritique1 = false;
    public bool _hasKillCritique2 = false;

    public static int _maxExilePoints = 0;
    public static int _exilePointsSpend = 0;

    public GameObject _pausePanel = null;
    public GameObject _gameOverPanel = null;
    public GameObject _victoirePanel = null;

    public GameObject _critique1 = null;
    public GameObject _critique2 = null;

    public int _waitingEffet = 0;
    public GameObject _cardUsingEffet = null;

    public GameObject _regardSpot = null;

    void Start ()
    {
        _ddm = _donjonDeckObject.GetComponent<DonjonDeckManager>();
        _ccm = _aventurierDeckObject.GetComponent<AventurierDeckManager>();
        _cam = _choosenAventurierSpot.GetComponent<ChoosenAventurier>();
        _udm = _usureDeckObject.GetComponent<UsureDeckManager>();

        _xmlPath = Application.dataPath + @"/CardsList.xml";
        _xmlDoc = new XmlDocument();
        if (File.Exists(_xmlPath))
        {
            Debug.Log("J'ai trouvé le ficher");
            _xmlDoc.Load(_xmlPath);
        }

        LoadDonjonDeck();
        LoadAventurierDeck();
        LoadUsureDeck();

        _notorietePointText.text = "" + _notorietePoints;
        _aventurierLevelText.text = "" + _aventurierLevel;


        GameTurnManager.ChangeState(GameState.ChoisirAventurier);
    }

    void Update()
    {
        if (GameTurnManager._actualGameState == GameState.PreparerDonjon)
        {
            UpdateAventurierCombatValue();
            if (_exilePointsSpend != 0)
            {
                _exilePointsSpend = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F10))
        {
            _pausePanel.GetComponent<PauseController>()._previousState = GameTurnManager._actualGameState;
            GameTurnManager.ChangeState(GameState.Pause);
            _pausePanel.SetActive(true);
        }

        if (_notorietePoints <= 0)
        {
            GameTurnManager.ChangeState(GameState.PartiePerdu);
            GameOver();
        }

        if(_hasKillCritique2 == true)
        {
            GameTurnManager.ChangeState(GameState.Victoire);
            Victoire();
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

    //Fonction pour lire le fichier Xml chargé
    //et récupérer le deck d'usure
    void LoadUsureDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            if (node.Attributes["name"].Value == "Usure")
            {
                XmlNodeList cards = node.ChildNodes;
                foreach (XmlNode card in cards)
                {
                    GameObject cardPrefab = Instantiate(_udm._cardModel);
                    PlayableCard datas1 = cardPrefab.GetComponent<PlayableCard>(); ;
                    datas1._name = card.Attributes["name"].Value;
                    datas1._battleValue = int.Parse(card.Attributes["resistance"].Value);
                    datas1._discardPrice = int.Parse(card.Attributes["defausse"].Value);
                    datas1._effet = int.Parse(card.Attributes["effet"].Value);
                    UsureDeckManager._usureDeck.Add(cardPrefab);
                }
                _udm.ShuffleDeck(UsureDeckManager._usureDeck);
            }
        }
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
            if(gO.GetComponent<PlayableCard>()._hasBeenDestroyed == false)
                donjonValue += gO.GetComponent<PlayableCard>()._battleValue;
        }
        donjonValue += _donjonBonus;
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
        //Cas ou on est pas contre un des 2 critiques de fin
        if (GameTurnManager._actualGameState == GameState.PreparerDonjon && _drawnDonjonCard > 0 ) {
            //On effectue les effets des cartes d'usures, si il y en as
            foreach (GameObject gO in PlayedCard._playedCardlist)
            {
                if(gO.GetComponent<PlayableCard>()._hasBeenDestroyed != true)
                {
                    if (gO.GetComponent<PlayableCard>()._effet < 0)
                    {
                        if (gO.GetComponent<PlayableCard>()._effet == -1)
                        {
                            this._notorietePoints--;
                            _notorietePointText.text = "" + _notorietePoints;
                        }
                        else if (gO.GetComponent<PlayableCard>()._effet == -2)
                        {
                            this._notorietePoints -= 2;
                            _notorietePointText.text = "" + _notorietePoints;
                        }
                    }
                }
               
            }
            if (_donjonBattleValue >= _aventurierBattleValue)
            {
                GameTurnManager.ChangeState(GameState.Victoire);
                foreach (GameObject gO in PlayedCard._playedCardlist)
                {
                    DonjonDeckManager._donjonDefausseDeck.Add(gO);
                    gO.transform.SetParent(this.transform.parent);
                    gO.transform.position = new Vector3(-100, -100, 0);
                    gO.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                PlayedCard._playedCardlist.Clear();

                DonjonDeckManager._donjonDefausseDeck.Add(_cam._choosenOne);
                _cam._choosenOne.transform.SetParent(this.transform.parent);
                _cam._choosenOne.transform.position = new Vector3(-100, -100, 0);
                _cam._choosenOne = null;

            }
            else if (_donjonBattleValue < _aventurierBattleValue)
            {
                _maxExilePoints = _aventurierBattleValue - _donjonBattleValue;
                this._notorietePoints -= _maxExilePoints;
                _notorietePointText.text = "" + _notorietePoints;
                GameTurnManager.ChangeState(GameState.Defaite);
            }
        }
        //Cas contre le premier critique
        else if(GameTurnManager._actualGameState == GameState.CombatCritique1 && _drawnDonjonCard > 0)
        {
            if(_donjonBattleValue >= _critique1.GetComponent<CritiqueCard>()._battleValue)
            {
                _hasKillCritique1 = true;

                GameTurnManager.ChangeState(GameState.Victoire);

                foreach (GameObject gO in PlayedCard._playedCardlist)
                {
                    DonjonDeckManager._donjonDefausseDeck.Add(gO);
                    gO.transform.SetParent(this.transform.parent);
                    gO.transform.position = new Vector3(-100, -100, 0);
                    gO.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                PlayedCard._playedCardlist.Clear();

                _cam._choosenOne.transform.SetParent(this.transform.parent);
                _cam._choosenOne.transform.position = new Vector3(-100, -100, 0);
                _cam._choosenOne = null;
            }
            else if (_donjonBattleValue < _critique1.GetComponent<CritiqueCard>()._battleValue && _drawnDonjonCard == _critique1.GetComponent<CritiqueCard>()._maxDraw)
            {
                GameTurnManager.ChangeState(GameState.PartiePerdu);
                GameOver();
            }
            else if (_donjonBattleValue < _critique1.GetComponent<CritiqueCard>()._battleValue && _drawnDonjonCard < _critique1.GetComponent<CritiqueCard>()._maxDraw)
            {
                Debug.Log("Can still draw cards !!!");
            }
        }
        //Cas contre le deuxième critique
        else if (GameTurnManager._actualGameState == GameState.CombatCritique2 && _drawnDonjonCard > 0)
        {
            if (_donjonBattleValue >= _critique2.GetComponent<CritiqueCard>()._battleValue)
            {
                _hasKillCritique2 = true;
            }
            else if (_donjonBattleValue < _critique1.GetComponent<CritiqueCard>()._battleValue && _drawnDonjonCard == _critique1.GetComponent<CritiqueCard>()._maxDraw)
            {
                GameTurnManager.ChangeState(GameState.PartiePerdu);
                GameOver();
            }
            else if (_donjonBattleValue < _critique1.GetComponent<CritiqueCard>()._battleValue && _drawnDonjonCard < _critique1.GetComponent<CritiqueCard>()._maxDraw)
            {
                Debug.Log("Can still draw cards !!!");
            }
        }
    }

    //Fonction qui gère la fin de tour 
    //Et comment doit se dérouler la suite.
    public void EndOfTurn()
    {
        //On verifie si l'etat permet de mettre fin au tour
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
                        SetFistBossBattle();
                    }
                    else if (_hasKillCritique2 == false)
                    {
                        GameTurnManager.ChangeState(GameState.CombatCritique2);
                        SetSecondBossBattle();
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

    //Prepare le combat contre le premier critique
    public void SetFistBossBattle()
    {
        _cam._choosenOne = Instantiate(_critique1);
        _aventurierBattleValue = _critique1.GetComponent<CritiqueCard>()._battleValue;
        _aventurierBattleValueText.text = "" + _aventurierBattleValue;
    }

    //Prepare le combat contre le premier critique
    public void SetSecondBossBattle()
    {
        _cam._choosenOne = Instantiate(_critique2);
        _aventurierBattleValue = _critique1.GetComponent<CritiqueCard>()._battleValue;
        _aventurierBattleValueText.text = "" + _aventurierBattleValue;
    }

    //Fonction Qui gère le GameOver
    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    //Fonction qui gère la victoire
    public void Victoire()
    {
        _victoirePanel.SetActive(true);
    }

    //Permet de mettre en place la zone de regard
    public void FaireRegard()
    {
        _regardSpot.SetActive(true);
        for(int i = 1; i <= 3; i++)
        {
            DonjonDeckManager._donjonDeck[DonjonDeckManager._donjonDeck.Count - i].transform.SetParent(_regardSpot.transform);
        }
    }
}