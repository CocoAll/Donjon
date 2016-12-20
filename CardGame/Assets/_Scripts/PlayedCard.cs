using UnityEngine;
using System.Collections.Generic;

public class PlayedCard : MonoBehaviour {

    public static List<GameObject> _playedCardlist = null;

	// Use this for initialization
	void Start () {
        _playedCardlist = new List<GameObject>();
	}

}
