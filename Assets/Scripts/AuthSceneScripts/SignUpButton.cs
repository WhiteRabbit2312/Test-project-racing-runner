using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(Button))]
public class SignUpButton : MonoBehaviour
{
    [Inject] private RegistrationManager _registrationManager;
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
        FirebaseAuth auth = _registrationManager.Auth;
        _login = _playerRegistrationData.Login;
        _password = _playerRegistrationData.Password;

        bool confirmed = true;

        var checkTask = _checkRegistrationData.Check(); 

        yield return new WaitUntil(() => checkTask.IsCompleted);

        if (!checkTask.Result)
        {
            confirmed = false;
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_login, _password).ContinueWith(task =>
            {
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


                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);

            });

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (confirmed)
            {
                Debug.Log("Confirmed");
                _checkRegistrationData.ConfirmPlayerData();
                SceneManager.LoadScene(Constants.MainMenuSceneIdx);

            }
            else
            {
                Debug.LogError("Did not confirm");

            }
        }
    }
}
