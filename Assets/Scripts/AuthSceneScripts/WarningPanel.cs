using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningPanel : MonoBehaviour//rename
{
    [SerializeField] private WarningConfig _warningConfig;
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseWarningPanel);
    }

    public void ShowWarning(WarningTypes type)
    {
        _warningPanel.SetActive(true);
        _warningText.text = _warningConfig.WarningText[(int)type];
    }

    private void CloseWarningPanel()
    {
        _warningPanel.SetActive(false);
    }
}
