using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AventurierDeckManager : MonoBehaviour {

    public GameObject _cardModel = null;
    public List<GameObject> _aventurierDeck = null;
    public List<GameObject> _aventurierDefausseDeck = null;

    public GameObject _selectedAventurierCardSpot = null;
    public GameObject _aventurierCardSpot = null;
    private ChoosenAventurier _acsm = null;
    private PlayedCard _pcm = null;
    [SerializeField]
    private Text _nbCardText = null;

    // Use this for initialization
    void Start () {
        _aventurierDeck = new List<GameObject>();
        _aventurierDefausseDeck = new List<GameObject>();
        _pcm = _aventurierCardSpot.GetComponent<PlayedCard>();
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


    //Fonction pour piocher les cartes 
    public void DrawCard()
    {
        if (GameTurnManager._actualGameState == GameState.ChoisirAventurier)
        {
            if (_aventurierDeck.Count > 1 && _pcm._playedCardlist.Count == 0 && _acsm._choosenOne == null)
            {
                _pcm._playedCardlist.Add(_aventurierDeck[_aventurierDeck.Count - 1]);
                _aventurierDeck.Remove(_aventurierDeck[_aventurierDeck.Count - 1]);
                _pcm._playedCardlist[_pcm._playedCardlist.Count - 1].transform.SetParent(_aventurierCardSpot.transform);
                _pcm._playedCardlist[_pcm._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);

                _pcm._playedCardlist.Add(_aventurierDeck[_aventurierDeck.Count - 1]);
                _aventurierDeck.Remove(_aventurierDeck[_aventurierDeck.Count - 1]);
                _pcm._playedCardlist[_pcm._playedCardlist.Count - 1].transform.SetParent(_aventurierCardSpot.transform);
                _pcm._playedCardlist[_pcm._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_aventurierDeck.Count == 1 && _pcm._playedCardlist.Count == 0 && _acsm._choosenOne == null)
            {
                _pcm._playedCardlist.Add(_aventurierDeck[_aventurierDeck.Count - 1]);
                _aventurierDeck.Remove(_aventurierDeck[_aventurierDeck.Count - 1]);
                _pcm._playedCardlist[_pcm._playedCardlist.Count - 1].transform.SetParent(_aventurierCardSpot.transform);
                _pcm._playedCardlist[_pcm._playedCardlist.Count - 1].transform.localScale = new Vector3(1, 1, 1);

                //TODO
                //Re-melanger Defausse, incrémenter/vérifier état de la partie : Fin ? ou juste plus dur après.
                //Question de règle : niveau augmenterce tour ci ou le suivant ??????????????????????????????????????????????????

            }
            else if (_aventurierDeck.Count == 0 && _pcm._playedCardlist.Count == 0 && _acsm._choosenOne == null)
            {
                //TODO
                //Re-melanger Defausse, incrémenter/vérifier état de la partie : Fin ? ou juste plus dur après.
            }
            UpdateText();
        }
    }

    private void UpdateText()
    {
        _nbCardText.text = "" + _aventurierDeck.Count;
    }
}
