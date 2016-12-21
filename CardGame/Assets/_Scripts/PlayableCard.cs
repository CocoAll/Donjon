using UnityEngine;
using System.Collections;

public class PlayableCard : MonoBehaviour {

    public string _name = "";

    public int _battleValue = 0;

    public int _discardPrice = 1;

    public int _effet = 0;

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

    public void ClickResolution()
    {
        if(GameTurnManager._actualGameState == GameState.PreparerDonjon)
        {
          if(_effet > 0)
          {
    
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
                DonjonDeckManager._donjonExileDeck.Add(this.gameObject);
                this.transform.SetParent(this.transform.parent.parent);
                this.transform.position = new Vector3(-100, -100, 0);
                _gcm._notorietePoints = _gcm._notorietePoints - _discardPrice;
                _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
            }

        }
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
