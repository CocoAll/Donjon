using UnityEngine;
using System.Collections;

public class ReturnToGame : MonoBehaviour {

    public GameObject _defausse = null;
    public GameObject _1to10 = null;
    public GameObject _11to20 = null;
    public GameObject _21to30 = null;
    public GameObject _toReturnTo = null;


    //Fonction pour enlever la défausse et retourner en jeu.
    public void BackToGame()
    {
        // Je m'assure ici que toutes les cartes sois hors du canvas pour ne pas
        //apparaitre a l'écran
        foreach (GameObject gO in DonjonDeckManager._donjonDefausseDeck)
        {
            if (gO.transform.parent != _toReturnTo.transform.parent)
            {
                gO.transform.SetParent(_toReturnTo.transform.parent);
            }
        }

        foreach (GameObject gO in DonjonDeckManager._donjonExileDeck)
        {
            if (gO.transform.parent != _toReturnTo.transform.parent)
            {
                gO.transform.SetParent(_toReturnTo.transform.parent);
            }
        }

        foreach (GameObject gO in AventurierDeckManager._aventurierDefausseDeck)
        {
            if(gO.transform.parent != _toReturnTo.transform.parent)
            {
                gO.transform.SetParent(_toReturnTo.transform.parent);
            }
        }
        _defausse.transform.SetAsFirstSibling();
        _defausse.SetActive(false);
    }
}
