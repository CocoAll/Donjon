using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DonjonDeckManager : MonoBehaviour {


    public GameObject _cardModel = null;
    public List<GameObject> _cardDeck = null;
    public List<GameObject> _defausseDeck = null;
    [SerializeField]
    private GameObject _donjonCardSpot = null;
    private PlayedCard _dcm = null;

	//On initialise les arrays
    //Puis melange le deck de base
	void Start () {
        _defausseDeck = new List<GameObject>();
        _cardDeck = new List<GameObject>();
        _dcm = _donjonCardSpot.GetComponent<PlayedCard>();
       /* for(int i = 0; i < 18; i++)
        {
            GameObject card = Instantiate(_cardModel);
            card.transform.SetParent(this.transform.parent);
            _cardDeck.Add(card);
        }*/
	
	}
	
	// Update is called once per frame
	void Update () {

        //Permet de remettre le deck lorsque celui ci est vide
	    if(_cardDeck.Count == 0)
        {
            _cardDeck = _defausseDeck;
            _defausseDeck = new List<GameObject>();
        }
	}


    //Fonction pour melanger de façon aléatoire un deck
    public void ShuffleDeck(List<GameObject> array)
    {
        for(int i = array.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0 , i+1);
            GameObject tmp = array[i];
            array[i] = array[r];
            array[r] = tmp;
        }
    }

   public void DrawCard()
    {
        if(_cardDeck.Count > 0)
        {
        _dcm._playedCardlist.Add(_cardDeck[_cardDeck.Count - 1]);
        _cardDeck.Remove(_cardDeck[_cardDeck.Count - 1]);
        _dcm._playedCardlist[_dcm._playedCardlist.Count - 1].transform.SetParent(_donjonCardSpot.transform);
        }
    }



}
