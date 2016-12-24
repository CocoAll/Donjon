using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//Permet de Stocker les cartes usures tant que le joueur ne les as pas.
public class UsureDeckManager : MonoBehaviour {

    public static List<GameObject> _usureDeck = null;
    public GameObject _cardModel = null;

	void Start () {
        _usureDeck = new List<GameObject>();
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
    }

}
