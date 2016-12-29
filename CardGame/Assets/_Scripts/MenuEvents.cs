using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour {

    //Fonction pour charger le niveau ou jouer
    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }


    //Fonction pour quitter le jeu
    public void ExitGame()
    {
        Application.Quit();
    }
}
