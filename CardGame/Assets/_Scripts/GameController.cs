using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

    private string _xmlPath = null;
    private XmlDocument _xmlDoc = null;



    private bool _canDrawFromPlayerDeckForFree = false;

    // Use this for initialization
    void Start ()
    {
        bdm = _donjonDeckObject.GetComponent<DonjonDeckManager>();
        ccm = _aventurierDeckObject.GetComponent<AventurierCard>();
        pcm = _playedCardObject.GetComponent<PlayedCard>();

        _xmlPath = Application.dataPath + @"/CardsList.xml";
        _xmlDoc = new XmlDocument();
        if (File.Exists(_xmlPath))
        {
            Debug.Log("J'ai trouvé le ficher");
            _xmlDoc.Load(_xmlPath);
        }

        LoadDonjonDeck();

        _notorietePointText.text = "" + _notorietePoints;
        _aventurierLevelText.text = "" + _hazardLevel;



    }

    void LoadDonjonDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            Debug.Log(node.Attributes["name"].Value);
            if(node.Attributes["name"].Value == "Base")
            {
                XmlNodeList cards = node.ChildNodes;
                foreach(XmlNode card in cards)
                {

                    int number = int.Parse(card.Attributes["nombre"].Value);
                    Debug.Log(number);
                    for (int i = 0; i< number; i++)
                    {
                        GameObject cardPrefab = Instantiate(bdm._cardModel);
                        PlayableCard datas = cardPrefab.GetComponent<PlayableCard>();
                        datas._name = card.Attributes["name"].Value;
                        datas._battleValue = int.Parse(card.Attributes["resistance"].Value);
                        datas._discardPrice = int.Parse(card.Attributes["defausse"].Value);
                        datas._effet = int.Parse(card.Attributes["effet"].Value);
                        bdm._cardDeck.Add(cardPrefab);
                    }

                }
            }
        }
        bdm.ShuffleDeck(bdm._cardDeck);

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
