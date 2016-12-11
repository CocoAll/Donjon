using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    private int _hazardLevel = 1;
    private int _hazardMaxLevel = 3;
    private int _notorietePoints = 20;


    [SerializeField]
    private Text _notorietePointText = null;

    [SerializeField]
    private Text _aventurierLevelText = null;

    [SerializeField]
    private GameObject _donjonDeckObject = null;
    private DonjonDeckManager bdm = null;

    [SerializeField]
    private GameObject _aventurierDeckObject = null;
    private AventurierCard ccm = null;

    [SerializeField]
    private GameObject _playedCardObject = null;
    private PlayedCard pcm = null;



    private bool _canDrawFromPlayerDeckForFree = false;

    // Use this for initialization
    void Start () {

        _notorietePointText.text = "" + _notorietePoints;
        _aventurierLevelText.text = "" + _hazardLevel;

        bdm = _aventurierLevelText.GetComponent<DonjonDeckManager>();
        ccm = _aventurierDeckObject.GetComponent<AventurierCard>();
        pcm = _aventurierDeckObject.GetComponent<PlayedCard>();

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
