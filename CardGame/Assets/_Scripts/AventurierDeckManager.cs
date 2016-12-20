using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AventurierDeckManager : MonoBehaviour {

    public GameObject _cardModel = null;
    public static List<GameObject> _aventurierDeck = null;
    public static List<GameObject> _aventurierDefausseDeck = null;

    public GameObject _selectedAventurierCardSpot = null;
    public GameObject _aventurierCardSpot = null;
    private ChoosenAventurier _acsm = null;
    [SerializeField]
    private Text _nbCardText = null;

    // Use this for initialization
    void Start () {
        _aventurierDeck = new List<GameObject>();
        _aventurierDefausseDeck = new List<GameObject>();
        _acsm = _selectedAventurierCardSpot.GetComponent<ChoosenAventurier>();
        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {

    }

    //Fonction pour melanger de façon aléatoire un deck
    public void ShuffleDeck(List<GameObject> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            GameObject tmp = array[i];
            array[i] = array[r];
            array[r] = tmp;
        }
        UpdateText();
    }

    //Permet de reprendre la défause, de la melanger et de rejouer avec.
    public void ResetDeck()
    {
        _aventurierDeck = _aventurierDefausseDeck;
        _aventurierDefausseDeck = new List<GameObject>();
        ShuffleDeck(_aventurierDeck);
    }

    //Fonction pour piocher les cartes 
    public void DrawCard()
    {
        if (GameTurnManager._actualGameState == GameState.ChoisirAventurier)
        {
            if (_aventurierDeck.Count > 1 && PlayedCard._playedCardlist.Count == 0 && _acsm._choosenOne == null)
            {
                PlayedCard._playedCardlist.Add(_aventurierDeck[_aventurierDeck.Count - 1]);
                _aventurierDeck.Remove(_aventurierDeck[_aventurierDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_aventurierCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);

                PlayedCard._playedCardlist.Add(_aventurierDeck[_aventurierDeck.Count - 1]);
                _aventurierDeck.Remove(_aventurierDeck[_aventurierDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_aventurierCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_aventurierDeck.Count == 1 && PlayedCard._playedCardlist.Count == 0 && _acsm._choosenOne == null)
            {
                PlayedCard._playedCardlist.Add(_aventurierDeck[_aventurierDeck.Count - 1]);
                _aventurierDeck.Remove(_aventurierDeck[_aventurierDeck.Count - 1]);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.SetParent(_aventurierCardSpot.transform);
                PlayedCard._playedCardlist[PlayedCard._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);

                //TODO
                //Re-melanger Defausse, incrémenter/vérifier état de la partie : Fin ? ou juste plus dur après.
                //Question de règle : niveau augmenterce tour ci ou le suivant ??????????????????????????????????????????????????

            }
            else if (_aventurierDeck.Count == 0 && PlayedCard._playedCardlist.Count == 0 && _acsm._choosenOne == null)
            {
                //TODO
                //Re-melanger Defausse, incrémenter/vérifier état de la partie : Fin ? ou juste plus dur après.
                //Done dans gamecontroller ( a check quad meme)
            }
            UpdateText();
        }
    }

    private void UpdateText()
    {
        _nbCardText.text = "" + _aventurierDeck.Count;
    }
}
