using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LogoutButton : MonoBehaviour
{
    [SerializeField] private ScenesSO _scenesSO;
    [Inject] private RegistrationManager _registrationManager;
    
    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(LogOutButton);
    }

    public void LogOutButton()
    {
        _registrationManager.Auth.SignOut();
        PlayerPrefs.DeleteKey(Constants.LogInKey);
        PlayerPrefs.DeleteKey(Constants.PasswordKey);
        PlayerPrefs.DeleteKey(Constants.SilentAuthKey);

        SceneManager.LoadScene(_scenesSO.AuthSceneIdx);
    }
}
