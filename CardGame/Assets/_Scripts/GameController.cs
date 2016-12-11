using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    private int _hazardLevel = 1;
    private int _hazardMaxLevel = 3;
    private int _lifePoints = 20;


    [SerializeField]
    private Text _lifePointText = null;

    [SerializeField]
    private Text _hazardLevelText = null;

    [SerializeField]
    private GameObject _playerDeckObject = null;
    private BaseDeckManager bdm = null;

    [SerializeField]
    private GameObject _hazardDeckObject = null;
    private ConfrontationCard ccm = null;

    [SerializeField]
    private GameObject _playedCardObject = null;
    private PlayedCard pcm = null;



    private bool _canDrawFromPlayerDeckForFree = false;

    // Use this for initialization
    void Start () {

        _lifePointText.text = "" + _lifePoints;
        _hazardLevelText.text = "" + _hazardLevel;

        bdm = _playerDeckObject.GetComponent<BaseDeckManager>();
        ccm = _hazardDeckObject.GetComponent<ConfrontationCard>();
        pcm = _hazardDeckObject.GetComponent<PlayedCard>();

    }
	
	// Update is called once per frame
	void Update () {
	

	}


    public void DrawACard()
    {
        pcm._playedCardlist.Add(bdm._cardDeck[bdm._cardDeck.Count - 1]);
        bdm._cardDeck.Remove(bdm._cardDeck[bdm._cardDeck.Count - 1]);
    }


}
