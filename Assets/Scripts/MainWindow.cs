using UnityEngine;
using TMPro;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;

    public void ShowScore(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void ShowHealth(int health)
    {
        _healthText.text = "Health: " + health.ToString();
    }
}
