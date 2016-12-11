using UnityEngine;
using System.Collections;

public class PlayableCard : MonoBehaviour {

    [SerializeField]
    private int _battleValue = 0;

    private int _discardPrice = 1;

	
	void Start () {
        _battleValue = Random.Range(-1, 3);
	}
	

	void Update () {
	
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
