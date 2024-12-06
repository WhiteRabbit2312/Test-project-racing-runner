using UnityEngine;
using TMPro;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;

    public void ShowScore(int score)
    {
        Debug.LogError($"Score: {score}");
        _scoreText.text = "Score: " + score.ToString();
    }

    public void ShowHealth(int health)
    {
        _healthText.text = "Health: " + health.ToString();
    }
}
