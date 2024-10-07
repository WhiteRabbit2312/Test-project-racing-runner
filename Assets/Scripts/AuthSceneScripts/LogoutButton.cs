using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour
{
    [Inject] private RegistrationManager _registrationManager;
    
    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(LogOutButton);
    }

    public void LogOutButton()
    {
        _registrationManager.Auth.SignOut();
        PlayerPrefs.SetString(Constants.LogInKey, "");
        PlayerPrefs.SetString(Constants.PasswordKey, "");
        PlayerPrefs.SetInt(Constants.SilentAuthKey, Constants.UserLogOutIdx);
        SceneManager.LoadScene(Constants.AuthSceneIdx);
    }
}
