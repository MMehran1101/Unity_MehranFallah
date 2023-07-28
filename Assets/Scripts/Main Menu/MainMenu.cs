using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
        public void ExitGame()
        {
            EditorApplication.ExitPlaymode();
        }

    }
}
