﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DonjonDeckManager : MonoBehaviour {


    public GameObject _cardModel = null;
    public static List<GameObject> _donjonDeck = null;
    public static List<GameObject> _donjonDefausseDeck = null;
    public static List<GameObject> _donjonExileDeck = null;
    [SerializeField]
    private GameObject _donjonCardSpot = null;
    [SerializeField]
    private Text _nbCardText = null;
    [SerializeField]
    private GameController _gcm = null;

	//On initialise les arrays
    //Puis melange le deck de base
	void Start () {
        _donjonDefausseDeck = new List<GameObject>();
        _donjonDeck = new List<GameObject>();
        _donjonExileDeck = new List<GameObject>();
        UpdateText();
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
        UpdateText();
    }

    //Permet de piocher une carte du 
    //deck donjon
   public void DrawCard()
    {
        if (_gcm.CanDrawDonjonCard() == true)
        {
            _gcm._drawnDonjonCard++;
            if (_donjonDeck.Count > 0)
            {
                PlayedCard._playedCardlist.Add(_donjonDeck[_donjonDeck.Count - 1]);
                _donjonDeck.Remove(_donjonDeck[_donjonDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_donjonCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_donjonDeck.Count == 0)
            {
                _donjonDefausseDeck.Add(UsureDeckManager._usureDeck[UsureDeckManager._usureDeck.Count - 1]);
                UsureDeckManager._usureDeck.Remove(UsureDeckManager._usureDeck[UsureDeckManager._usureDeck.Count - 1]);
                ResetDeck();
                PlayedCard._playedCardlist.Add(_donjonDeck[_donjonDeck.Count - 1]);
                _donjonDeck.Remove(_donjonDeck[_donjonDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_donjonCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            }

            _gcm.HasDraw();
            UpdateText();
        }
    }

    //Fonction mettant a jour les textes
    //pour les valeurs de combats
    private void UpdateText()
    {
        _nbCardText.text = "" + _donjonDeck.Count;
    }

    //Fonction qui permet de remettre le deck defausse
    //En deck jouable
    public void ResetDeck()
    {
        _donjonDeck = _donjonDefausseDeck;
        _donjonDefausseDeck = new List<GameObject>();
        ShuffleDeck(_donjonDeck);
        foreach (GameObject gO in _donjonDeck)
        {
            gO.GetComponent<PlayableCard>()._hasUseEffet = false;
        }
    }

    //Extra Draw qui coute 1 point de notoriété
    public void ExtraDrawCard()
    {
        if(GameTurnManager._actualGameState == GameState.PreparerDonjon)
        {
            _gcm._notorietePoints--;
            _gcm._notorietePointText.text = "" + _gcm._notorietePoints;
            if (_donjonDeck.Count > 0)
            {
                PlayedCard._playedCardlist.Add(_donjonDeck[_donjonDeck.Count - 1]);
                _donjonDeck.Remove(_donjonDeck[_donjonDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_donjonCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_donjonDeck.Count == 0)
            {
                _donjonDefausseDeck.Add(UsureDeckManager._usureDeck[UsureDeckManager._usureDeck.Count - 1]);
                UsureDeckManager._usureDeck.Remove(UsureDeckManager._usureDeck[UsureDeckManager._usureDeck.Count - 1]);
                ResetDeck();
                PlayedCard._playedCardlist.Add(_donjonDeck[_donjonDeck.Count - 1]);
                _donjonDeck.Remove(_donjonDeck[_donjonDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_donjonCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            }
            _gcm.HasDraw();
            UpdateText();
        }
    }
}
