using UnityEngine;
using System.Collections;

public class ConfrontationCard : MonoBehaviour {

    [SerializeField]
    private int _level1BattleValue = 0;
    [SerializeField]
    private int _level2BattleValue = 0;
    [SerializeField]
    private int _level3BattleValue = 0;

    [SerializeField]
    private int _battleValueAdded = 0;

    // Use this for initialization
    void Start () {
	    _level1BattleValue = Random.Range(0, 6);
        _level2BattleValue = Random.Range(1, 10);
        _level3BattleValue = Random.Range(3, 15);
        _battleValueAdded = Random.Range(-1, 5);
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
