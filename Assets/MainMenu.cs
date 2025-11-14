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
        if(Cursor.visible == false && SceneManager.GetActiveScene().name == "New Scene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("New Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
