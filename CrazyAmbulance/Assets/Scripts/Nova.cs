using UnityEngine.SceneManagement;
using UnityEngine;

public class Nova : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
