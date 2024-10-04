using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Zenject;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase;

public class LogInButton : MonoBehaviour
{
    [SerializeField] private PlayerProvideData _playerRegistrationData;
    [SerializeField] private WarningPanel _warningPanel;

    [Inject] private RegistrationManager _registrationManager;

    private string _login;
    private string _password;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(async () => await ProvideLogIn());
    }

    private async Task ProvideLogIn()
    {

        FirebaseAuth auth = RegistrationManager.Instance.Auth;
        _login = _playerRegistrationData.Login;
        _password = _playerRegistrationData.Password;

        bool confirm = false;

        try
        {
            Firebase.Auth.AuthResult result = await auth.SignInWithEmailAndPasswordAsync(_login, _password);
            confirm = true;

        }
        catch (FirebaseException ex)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + ex.Message);
            _warningPanel.ShowWarning(WarningTypes.WrongPassword);
        }

        if (confirm)
        {
            SceneManager.LoadScene(Constants.MainMenuSceneIdx);
        }
    }
}
