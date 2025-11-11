using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("New Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
