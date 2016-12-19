using UnityEngine;
using System.Collections.Generic;

public class PlayedCard : MonoBehaviour {

    public List<GameObject> _playedCardlist = null;

	// Use this for initialization
	void Start () {
        _playedCardlist = new List<GameObject>();
	}

}
