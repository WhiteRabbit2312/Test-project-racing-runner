using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class SignUpButton : MonoBehaviour
{
    [SerializeField] private PlayerProvideData _playerRegistrationData;
    [SerializeField] private CheckRegistrationData _checkRegistrationData;
    private string _login;
    private string _password;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ProvideSignUp);
    }

    private void ProvideSignUp()
    {
        StartCoroutine(ProvideSignUpCoroutine());
    }

    private IEnumerator ProvideSignUpCoroutine()
    {
        FirebaseAuth auth = RegistrationManager.Instance.Auth;
        _login = _playerRegistrationData.Login;
        _password = _playerRegistrationData.Password;

        Debug.Log("login: " + _login);
        Debug.Log("password: " + _password);
        Debug.Log("auth: " + auth);

        bool confirmed = true;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_login, _password).ContinueWith(task =>
        {
            if (!_checkRegistrationData.Check())
            {
                confirmed = false;

                Debug.LogError("Did not confirm");
                return;
            }

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                confirmed = false;

                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                confirmed = false;
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

        });

        yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

        if (confirmed)
        {
            Debug.Log("Confirmed");

            _checkRegistrationData.ConfirmPlayerData();
        }


    }
}
