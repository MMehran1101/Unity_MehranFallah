using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;

    private void Start()
    {
        _isGameOver = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}