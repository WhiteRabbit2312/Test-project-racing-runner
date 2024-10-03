using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TemplateData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerPlaceText;
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private Image _playerImg;

    public void SetData(int place, string name, int score)
    {
        _playerPlaceText.text = place.ToString();
        _playerNameText.text = name;
        _playerScoreText.text = score.ToString();
    }

    public void SetColor(Color color)
    {
        _playerImg.color = color;
    }
}
