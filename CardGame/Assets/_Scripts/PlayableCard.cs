using UnityEngine;
using UnityEngine.UI;

public class PlayableCard : MonoBehaviour {

    public string _name = "";

    public int _battleValue = 0;

    public int _discardPrice = 1;

    public int _effet = 0;
    public bool _hasUseEffet = false;

    public bool _canBeDoubleClick = false;
    public bool _hasBeenDoubleClick = false;

    public GameController _gcm = null;

    public bool _hasBeenDestroyed = false;
    public bool _canCopyEffect = false;

    [SerializeField]
    private Text _nameText = null;
    [SerializeField]
    private Text _battleValueText = null;
    [SerializeField]
    private Text _effetText = null;

    void Start()
    {
        _gcm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    //Fonction permettant la résolution d'un clic
    //sur une carte jouer
    public void ClickResolution()
    {
        if(GameTurnManager._actualGameState == GameState.PreparerDonjon && this.transform.parent != _gcm._choosenAventurierSpot.transform)
        {
            if(_hasUseEffet == false)
            {
                CardEffetResolution();
                if(_effet > 0)
                {
                    this.transform.rotation = Quaternion.Euler(0,0,90);
                }
            }

        }
        else if(GameTurnManager._actualGameState == GameState.Defaite)
        {
            if (_canBeDoubleClick == false)
            {
                foreach (GameObject gO in PlayedCard._playedCardlist)
                {
                    gO.GetComponent<PlayableCard>()._canBeDoubleClick = false;
                }
                _canBeDoubleClick = true;
            }
            else
            {
                if(GameController._exilePointsSpend + _discardPrice <= GameController._maxExilePoints)
                {
                    GameController._exilePointsSpend += _discardPrice;
                    PlayedCard._playedCardlist.Remove(this.gameObject);
                    DonjonDeckManager._donjonExileDeck.Add(this.gameObject);
                    this.transform.SetParent(this.transform.parent.parent);
                    this.transform.position = new Vector3(-100, -100, 0);
                }
            }

        }
        //Permet de gerer le clique si l'effet en cour est le 3
        else if(GameTurnManager._actualGameState == GameState.EffetEnCours && _gcm._waitingEffet == 3)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = new Vector3(-100, -100, 0);
            PlayedCard._playedCardlist.Remove(this.gameObject);
            DonjonDeckManager._donjonDeck.Insert(0, this.gameObject);
            if(this._hasUseEffet == true)
            {
                this._hasUseEffet = false;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            _gcm.UpdateDonjonCombatValue();
            _gcm._waitingEffet = 0;
            GameTurnManager.ChangeState(GameState.PreparerDonjon);
        }
        //Permet de gerer le clique si l'effet en cour est le 4
        else if (GameTurnManager._actualGameState == GameState.EffetEnCours && _gcm._waitingEffet == 4)
        {
            _hasBeenDestroyed = true;
            _gcm.UpdateDonjonCombatValue();
            _gcm._waitingEffet = 0;
            GameTurnManager.ChangeState(GameState.PreparerDonjon);
        }
        //Permet de gerer le clique si l'effet en cour est le 5
        else if (GameTurnManager._actualGameState == GameState.EffetEnCours && _gcm._waitingEffet == 5)
        {
            if(_gcm._cardUsingEffet != this.gameObject)
            {
                _gcm._cardUsingEffet.GetComponent<PlayableCard>()._effet = this._effet;
            }
            GameTurnManager.ChangeState(GameState.PreparerDonjon);
        }
        //Permet de gerer le clique si l'effet en cour est le 8
        else if (GameTurnManager._actualGameState == GameState.EffetEnCours && _gcm._waitingEffet == 8)
        {
            DonjonDeckManager._donjonDefausseDeck.Add(this.gameObject);
            this.transform.SetParent(_gcm.transform.parent);
            _gcm._drawnDonjonCard--;
            _gcm._ddm.DrawCard();
            _gcm.UpdateDonjonCombatValue();
            GameTurnManager.ChangeState(GameState.PreparerDonjon);
        }
        //Permet de gerer le clique si l'effet en cour est le 9
        else if (GameTurnManager._actualGameState == GameState.EffetEnCours && _gcm._waitingEffet == 9)
        {
            _gcm._donjonBonus += this._battleValue;
            _gcm.UpdateDonjonCombatValue();
            GameTurnManager.ChangeState(GameState.PreparerDonjon);
        }
        //Permet de resoudre le regard 3 (effet 11)
        else if(GameTurnManager._actualGameState == GameState.Regard3 && this.transform.parent == _gcm._regardSpot.transform)
        {
            for (int i = 1; i <= 3; i++)
            {
                DonjonDeckManager._donjonDeck[DonjonDeckManager._donjonDeck.Count - i].transform.SetParent(_gcm.transform.parent);
            }
            DonjonDeckManager._donjonDefausseDeck.Add(this.gameObject);
            DonjonDeckManager._donjonDeck.Remove(this.gameObject);
            GameTurnManager.ChangeState(GameState.PreparerDonjon);
        }

    }

    //Fonction permettant de gérer que faire en cas de click en fonction de l'état du tour
    public void CardEffetResolution()
    {
        switch (_effet)
        {
            default :
                Debug.Log("Pas d'effet de carte");
                break;
            //Si le int effet est superieur a 0, alors on applique l'effet associé
            case 1:
                //ajouter deux Points de notoriété
                if(_gcm._notorietePoints <= 20)
                {
                    _gcm._notorietePoints = _gcm._notorietePoints + 2;
                    _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
                }
                else if (_gcm._notorietePoints == 21)
                {
                    _gcm._notorietePoints = _gcm._notorietePoints + 1;
                    _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
                }
                _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
                break;
            case 2:
                //Permet de réduire la difficulté de 1 le combat en cours si 
                //On estau niveau 2 ou 3.
                if (_gcm._aventurierLevel > 2)
                {
                    AventurierCard aC = _gcm._cam._choosenOne.GetComponent<AventurierCard>();
                    if(aC != null)
                    {
                        if (_gcm._aventurierLevel == 2)
                            _gcm._aventurierBattleValue = aC._level1BattleValue;
                        if (_gcm._aventurierLevel == 3)
                            _gcm._aventurierBattleValue = aC._level1BattleValue;

                        _gcm._aventurierBattleValueText.text = "" + _gcm._aventurierBattleValue;
                    }

                }
                break;
            case 3:
                //Permet de mettre une des cartes déjà piocher en dessous du
                //Deck donjon
                _gcm._waitingEffet = 3;
                _gcm._cardUsingEffet = this.gameObject;
                GameTurnManager.ChangeState(GameState.EffetEnCours);
                //Suite Dans clickResolution
                break;
            case 4:
                //Permet de détruire une carte jouer
                //le temps du combat (valeur = 0 et pas d'effet)
                _gcm._waitingEffet = 4;
                _gcm._cardUsingEffet = this.gameObject;
                GameTurnManager.ChangeState(GameState.EffetEnCours);
                //Suite Dans clickResolution
                break;
            case 5:
                //Permet de copier l'effet d'une carte déjà piocher
                _canCopyEffect = true;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                _gcm._waitingEffet = 5;
                _gcm._cardUsingEffet = this.gameObject;
                GameTurnManager.ChangeState(GameState.EffetEnCours);
                //Suite Dans clickResolution
                break;
            case 6:
                //Plus 1 point de notoriété
                if (_gcm._notorietePoints < 22)
                {
                    _gcm._notorietePoints = _gcm._notorietePoints + 1;
                    _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
                }
                break;
            case 7:
                //Deux pioches supplémentaires possible
                _gcm._drawnDonjonCard -= 2;
                break;
            case 8:
                //Premet de defausser jusqu'a 1 carte
                //Et d'en repiocher à la place
                _gcm._waitingEffet = 8;
                _gcm._cardUsingEffet = this.gameObject;
                GameTurnManager.ChangeState(GameState.EffetEnCours);
                break;
            case 9:
                //Permet de doublé la valeur de combat
                //D'une carte donnée
                _gcm._waitingEffet = 9;
                _gcm._cardUsingEffet = this.gameObject;
                GameTurnManager.ChangeState(GameState.EffetEnCours);
                break;
            case 10:
                //Une pioche supplémentaire possible
                _gcm._drawnDonjonCard -= 1;
                break;
            case 11:
                //Permet de regardé les 3 cartes du dessus 
                //du deck et de pouvoir en defausser une
                GameTurnManager.ChangeState(GameState.Regard3);
                _gcm.FaireRegard();
                break;
        }
        if(_effet != 5)
            _hasUseEffet = true;

        //On s'assure de remettre bien la valeur de l'effet
        //après copie
        if (this._canCopyEffect == true && this._effet != 5 && _hasUseEffet == true)
        {
            this._effet = 5;
        }

    }

    public void UpdatePlayableTexts()
    {
        _nameText.text = "" + _name;
        _battleValueText.text = "" + _battleValue;
        switch (_effet)
        {
            case -2:
                _effetText.text = "-2 point sde notoriété";
                break;
            case -1:
                _effetText.text = "-1 point de notoriété";
                break;
            case 0:
                _effetText.text = "Pas d'effet";
                break;
            case 1:
                _effetText.text = "+2 points de notoriété";
                break;
            case 2:
                _effetText.text = "-1 Niveau";
                break;
            case 3:
                _effetText.text = "Réserve du Donjon";
                break;
            case 4:
                _effetText.text = "Destruction";
                break;
            case 5:
                _effetText.text = "Copie";
                break;
            case 6:
                _effetText.text = "+1 point de notoriété";
                break;
            case 7:
                _effetText.text = "+2 pioches";
                break;
            case 8:
                _effetText.text = "Echange";
                break;
            case 9:
                _effetText.text = "Double";
                break;
            case 10:
                _effetText.text = "+1 Carte";
                break;
            case 11:
                _effetText.text = "Regard 3";
                break;
        }
    }
}
