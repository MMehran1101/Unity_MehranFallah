using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScore(int updateScore)
    {
        scoreText.text = updateScore.ToString();
    }
}
