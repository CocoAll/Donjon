using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BaseDeckManager : MonoBehaviour {


    [SerializeField]
    private GameObject _cardModel = null;
    public List<GameObject> _cardDeck = null;
    public List<GameObject> _defausseDeck = null;

	//On initialise les arrays
    //Puis melange le deck de base
	void Start () {
        _defausseDeck = new List<GameObject>();
        _cardDeck = new List<GameObject>();
        for(int i = 0; i < 18; i++)
        {
            _cardDeck.Add(Instantiate(_cardModel));
        }
        ShuffleDeck(_cardDeck);
	
	}
	
	// Update is called once per frame
	void Update () {

        //Permet de remettre le deck lorsque celui ci est vide
	    if(_cardDeck[0] == null)
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
}
