using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class LogInButton : MonoBehaviour
{
    [SerializeField] private PlayerProvideData _playerRegistrationData;
    [SerializeField] private WarningPanel _warningPanel;
    private string _login;
    private string _password;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ProvideLogIn);
    }

    private void ProvideLogIn()
    {
        FirebaseAuth auth = RegistrationManager.Instance.Auth;
        _login = _playerRegistrationData.Login;
        _password = _playerRegistrationData.Password;

        auth.SignInWithEmailAndPasswordAsync(_login, _password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });

      

    }
}
