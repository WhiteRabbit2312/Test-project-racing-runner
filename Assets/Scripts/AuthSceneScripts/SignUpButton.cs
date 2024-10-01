using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class SignUpButton : MonoBehaviour
{
    [SerializeField] private PlayerRegistrationData _playerRegistrationData;
    private string _login;
    private string _password;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ProvideSignUp);
    }


    private void ProvideSignUp()
    {
        FirebaseAuth auth = RegistrationManager.Instance.Auth;
        _login = _playerRegistrationData.Login;
        _password = _playerRegistrationData.Password;

        Debug.Log("login: " + _login);
        Debug.Log("password: " + _password);
        Debug.Log("auth: " + auth);

        auth.CreateUserWithEmailAndPasswordAsync(_login, _password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }
}
