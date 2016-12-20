using UnityEngine;
using System.Collections;

public class PlayableCard : MonoBehaviour {

    public string _name = "";

    public int _battleValue = 0;

    public int _discardPrice = 1;

    public int _effet = 0;

    public bool _canBeDoubleClick = false;
    public bool _hasBeenDoubleClick = false;

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
                //Faut exiler la cartes !!!
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
