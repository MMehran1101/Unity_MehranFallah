using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverText;
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
        gameOverText.SetActive(true);
    }
}
