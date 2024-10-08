using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProvideData : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    private string _login;
    private string _password;

    private void Awake()
    {
        /*
        if (PlayerPrefs.GetInt(Constants.SilentAuthKey) == 1)
        {
            _login = PlayerPrefs.GetString(Constants.LogInKey);
            _password = PlayerPrefs.GetString(Constants.PasswordKey);
        }

        else
        {*/
            _loginInputField.onValueChanged.AddListener(WritePlayerLogin);
            _passwordInputField.onValueChanged.AddListener(WritePlayerPassword);
        //}
    }

    public string Login
    {
        get { return _login; }
    }

    public string Password
    {
        get { return _password; }
    }

    private void WritePlayerLogin(string login)
    {
        _login = login;
    }

    private void WritePlayerPassword(string password)
    {
        _password = password;
    }
}
