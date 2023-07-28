using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Image livesImg;
    [SerializeField] private Sprite[] livesSprites;

    public void UpdateScore(int updateScore)
    {
        scoreText.text = updateScore.ToString();
    }

    public void UpdateLives(int currentLive)
    {
        livesImg.sprite = livesSprites[currentLive];
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    private IEnumerator GameOverFlicker()
    {
        var i = 0;
        while (i < 4)
        {
            gameOverText.text = null;
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }
}