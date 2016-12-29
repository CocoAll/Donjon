using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public GameState _previousState = 0;
	
	// Update is called once per frame
	void Update () {
        this.transform.SetAsLastSibling();
	}

    public void Continuer()
    {
        GameTurnManager._actualGameState = _previousState;
        this.gameObject.SetActive(false);
    }

    public void Quitter()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
