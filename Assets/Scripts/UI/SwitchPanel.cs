using UnityEngine;
using UnityEngine.UI;

public class SwitchPanel : MonoBehaviour
{
    [SerializeField] private Button _switchButton;
    [SerializeField] private GameObject _switchToThisPanel;
    [SerializeField] private GameObject _switchFromThisPanel;

    private void Awake()
    {
        _switchButton.onClick.AddListener(Switch);
    }

    private void Switch()
    {
        _switchToThisPanel.SetActive(true);
        _switchFromThisPanel.SetActive(false);


    }
}
