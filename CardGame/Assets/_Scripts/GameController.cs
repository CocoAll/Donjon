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
    private AventurierDeckManager ccm = null;

    [SerializeField]
    private GameObject _playedCardObject = null;
    private PlayedCard pcm = null;

    private string _xmlPath = null;
    private XmlDocument _xmlDoc = null;

    public Canvas _screen = null;

    void Start ()
    {
        bdm = _donjonDeckObject.GetComponent<DonjonDeckManager>();
        ccm = _aventurierDeckObject.GetComponent<AventurierDeckManager>();
        pcm = _playedCardObject.GetComponent<PlayedCard>();

        _xmlPath = Application.dataPath + @"/CardsList.xml";
        _xmlDoc = new XmlDocument();
        if (File.Exists(_xmlPath))
        {
            Debug.Log("J'ai trouvé le ficher");
            _xmlDoc.Load(_xmlPath);
        }

        LoadDonjonDeck();
        LoadAventurierDeck();

        _notorietePointText.text = "" + _notorietePoints;
        _aventurierLevelText.text = "" + _hazardLevel;



    }


    //Fonction pour lire le fichier Xml chargé
    //et récupérer le deck de base du donjon
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
                        bdm._donjonDeck.Add(cardPrefab);
                    }

                }
            }
        }
        bdm.ShuffleDeck(bdm._donjonDeck);

    }

    void LoadAventurierDeck()
    {
        XmlNodeList list = _xmlDoc.DocumentElement.SelectNodes("/decks/deck");
        foreach (XmlNode node in list)
        {
            Debug.Log(node.Attributes["name"].Value);
            if (node.Attributes["name"].Value == "Confrontation")
            {
                XmlNodeList cards = node.ChildNodes;
                foreach (XmlNode card in cards)
                {
                    GameObject cardPrefab = Instantiate(ccm._cardModel);
                    PlayableCard datas1 = cardPrefab.GetComponent<PlayableCard>(); ;
                    AventurierCard datas2 = cardPrefab.GetComponent<AventurierCard>();
                    datas1._name = card.Attributes["newCardName"].Value;
                    datas1._battleValue = int.Parse(card.Attributes["resistance"].Value);
                    datas1._discardPrice = int.Parse(card.Attributes["defausse"].Value);
                    datas1._effet = int.Parse(card.Attributes["effet"].Value);
                    datas2._aventurierName = card.Attributes["name"].Value;
                    datas2._level1BattleValue = int.Parse(card.Attributes["nv1"].Value);
                    datas2._level2BattleValue = int.Parse(card.Attributes["nv2"].Value);
                    datas2._level3BattleValue = int.Parse(card.Attributes["nv3"].Value);
                    datas2._aventurierCardSpot = ccm._aventurierCardSpot;
                    datas2._selectedAventurierCardSpot = ccm._selectedAventurierCardSpot;
                    ccm._aventurierDeck.Add(cardPrefab);
                    datas2._ddm = this.ccm;
                }

            }
        }
        ccm.ShuffleDeck(ccm._aventurierDeck);
    }

    void Update () {
	
	}


}
