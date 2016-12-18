using UnityEngine;
using System.Collections;

public class PlayableCard : MonoBehaviour {

    public string _name = "";

    public int _battleValue = 0;

    public int _discardPrice = 1;

    public int _effet = 0;

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

        }
    }
}
