using UnityEngine;
using System.Collections;

public class AventurierCard : MonoBehaviour {


    public string _aventurierName = "";
   
    public int _level1BattleValue = 0;

    public int _level2BattleValue = 0;

    public int _level3BattleValue = 0;

    public string _donjonBonusName = "";

    public int _battleValueAdded = 0;

    public int _battleEffect = 0;

    public int _defausseCost = 0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
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
