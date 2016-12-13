﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DonjonDeckManager : MonoBehaviour {


    public GameObject _cardModel = null;
    public List<GameObject> _donjonDeck = null;
    public List<GameObject> _donjonDefausseDeck = null;
    [SerializeField]
    private GameObject _donjonCardSpot = null;
    private PlayedCard _dcm = null;

	//On initialise les arrays
    //Puis melange le deck de base
	void Start () {
        _donjonDefausseDeck = new List<GameObject>();
        _donjonDeck = new List<GameObject>();
        _dcm = _donjonCardSpot.GetComponent<PlayedCard>();
	}
	
	// Update is called once per frame
	void Update () {

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
        if(_donjonDeck.Count > 0)
        {
             _dcm._playedCardlist.Add(_donjonDeck[_donjonDeck.Count - 1]);
            _donjonDeck.Remove(_donjonDeck[_donjonDeck.Count - 1]);
            _dcm._playedCardlist[_dcm._playedCardlist.Count - 1].transform.SetParent(_donjonCardSpot.transform);
            _dcm._playedCardlist[_dcm._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_donjonDeck.Count == 0)
        {
            //TODO
            //Melanger défausse, ajouter carte usure puis piocher
        }
    }



}
