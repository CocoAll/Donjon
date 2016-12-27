using UnityEngine;
using System.Collections;

public class PlayableCard : MonoBehaviour {

    public string _name = "";

    public int _battleValue = 0;

    public int _discardPrice = 1;

    public int _effet = 0;
    public bool _hasUseEffet = false;

    public bool _canBeDoubleClick = false;
    public bool _hasBeenDoubleClick = false;

    public GameController _gcm = null;

    void Start()
    {
        _gcm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {

    }

    //Fonction permettant la résolution d'un clic
    //sur une carte jouer
    public void ClickResolution()
    {
        if(GameTurnManager._actualGameState == GameState.PreparerDonjon)
        {
            if(_hasUseEffet == false)
            {
                CardEffetResolution();
                if(_effet != 0)
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
                    DonjonDeckManager._donjonExileDeck.Add(this.gameObject);
                    this.transform.SetParent(this.transform.parent.parent);
                    this.transform.position = new Vector3(-100, -100, 0);
                    _gcm._notorietePoints = _gcm._notorietePoints - _discardPrice;
                    _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
                }
            }

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
                //ajouter deux PdV
                _gcm._notorietePoints = _gcm._notorietePoints + 2;
                _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
                break;
            case 2:
                break;
            case 3:
                break;
        }
        _hasUseEffet = true;
    }

    //***************************************************************************//
    //Accesseurs//
    //***************************************************************************//
    public int GetBattleValue()
    {
        return _battleValue;
    }

    public int GetDiscardPrice()
    {
        return _discardPrice;
    }

}
