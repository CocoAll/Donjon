using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameController : MonoBehaviour {

    void Update()
    {
        this.transform.SetAsLastSibling();
    }

    public void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Quitter()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
