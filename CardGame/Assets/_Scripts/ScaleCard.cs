using UnityEngine;
using System.Collections;

public class ScaleCard : MonoBehaviour {

    public void ScaleUp()
    {
        this.transform.localScale = new Vector3(2,2,2);
        this.transform.parent.SetAsLastSibling();
    }

    public void ScaleDown()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
}
