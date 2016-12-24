using UnityEngine;
using System.Collections;

public class DonjonDefausseManager : MonoBehaviour {

    public GameObject _defausseSpot = null;
    public GameObject _1to10 = null;
    public GameObject _11to20 = null;
    public GameObject _21to30 = null;

    public void OnClickDonjonDefausse()
    {
        _defausseSpot.SetActive(true);
        _defausseSpot.transform.SetAsLastSibling();
        int i = DonjonDeckManager._donjonDefausseDeck.Count;
        if (i <= 30 && i > 0)
        {
            for (int j = 0; j < i; j++)
            {
                if(j < 10)
                {
                    DonjonDeckManager._donjonDefausseDeck[j].transform.SetParent(_1to10.transform);
                }
                else if (j < 20)
                {
                    DonjonDeckManager._donjonDefausseDeck[j].transform.SetParent(_11to20.transform);
                }
                else
                {
                    DonjonDeckManager._donjonDefausseDeck[j].transform.SetParent(_21to30.transform);
                }
            }
        }
        else if (i > 30)
        {
            for (int j = 0; j < 30; j++)
            {
                if (j < 10)
                {
                    DonjonDeckManager._donjonDefausseDeck[j].transform.SetParent(_1to10.transform);
                }
                else if (j < 20)
                {
                    DonjonDeckManager._donjonDefausseDeck[j].transform.SetParent(_11to20.transform);
                }
                else
                {
                    DonjonDeckManager._donjonDefausseDeck[j].transform.SetParent(_21to30.transform);
                }
            }
        }
    }

    void Update()
    {
        if(_defausseSpot.active == true && this.transform.parent.childCount != this.transform.GetSiblingIndex())
        {
            _defausseSpot.transform.SetAsLastSibling();
        }
    }
}
