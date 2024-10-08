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
    [Inject] private DatabaseManager _databaseManager;

    private string _login;
    private string _password;

    private void Awake()
    {
        /*
        if (!PlayerPrefs.HasKey(Constants.SilentAuthKey))
        {
            PlayerPrefs.SetInt(Constants.SilentAuthKey, Constants.UserLogOutIdx);
        }*/
    }

    private void Start()
    {
        CheckAuthentification();

        Button button = GetComponent<Button>();

        button.onClick.AddListener(async () => await ProvideLogIn());
    }

    private void CheckAuthentification()
    {
        if (PlayerPrefs.GetInt(Constants.SilentAuthKey) == 1) 
        {
            Task.Run(async () =>
            {
                await ProvideLogIn();
            });
        } 
    }

    private async Task ProvideLogIn()
    {
        _login = _playerRegistrationData.Login;
        _password = _playerRegistrationData.Password;

        FirebaseAuth auth = _registrationManager.Auth;
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
            
            _databaseManager.GetUser();
            Debug.LogWarning("Confirm");
            SceneManager.LoadScene(Constants.MainMenuSceneIdx);
            Debug.LogWarning("Confirm2");
            SilentAuthentification();
        }

    }

    private void SilentAuthentification()
    {
        PlayerPrefs.SetString(Constants.LogInKey, _login);
        PlayerPrefs.SetString(Constants.PasswordKey, _password);
        PlayerPrefs.SetInt(Constants.SilentAuthKey, Constants.UserLogInIdx);
    }
}
