using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartGameText;
    [SerializeField] private Image livesImg;
    [SerializeField] private Sprite[] livesSprites;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateScore(int updateScore)
    {
        scoreText.text = updateScore.ToString();
    }

    public void UpdateLives(int currentLive)
    {
        livesImg.sprite = livesSprites[currentLive];
        if (currentLive==0)
        {
            gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
            _gameManager.GameOver();
        }
    }


    private IEnumerator GameOverFlicker()
    {
        var i = 0;
        while (i < 3)
        {
            gameOverText.text = null;
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            i++;
        }
        restartGameText.gameObject.SetActive(true);
    }
}