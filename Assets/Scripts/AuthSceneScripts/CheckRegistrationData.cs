using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;
using System.Text.RegularExpressions;
using TMPro;

public class CheckRegistrationData : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginInputField;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;
    [SerializeField] private TMP_InputField _confirmPasswordInputField;

    [SerializeField] private Button _registrationButton;

    [SerializeField] private WarningPanel _warning;

    [SerializeField] private DatabaseInfo _databaseInfo;

    private bool _isNameFound = false;

    public void ConfirmPlayerData()
    {
        _databaseInfo.SetData(Constants.DatabaseNameKey, _nameInputField.text);
        _databaseInfo.SetData(Constants.DatabaseAvatarKey, 0);
        _databaseInfo.SetData(Constants.DatabaseScoreKey, 0);

    }

    public bool Check()
    {
        Debug.Log("1");
        DoesNameExist((name) =>
        {
            _isNameFound = name;

            if (_isNameFound && IsPasswordConfirmed()
            && IsLoginValid(_loginInputField.text)
            && IsEnoughSymbols())
            {
                Debug.Log("true");
                return true;
            }

            else
            {
                Debug.Log("false");

                return false;
            }
        });


        return false;
        
    }

    private bool IsPasswordConfirmed()
    {
        Debug.Log("2");

        if (_passwordInputField.text == _confirmPasswordInputField.text)
        {
            return true;
        }

        else
        {
            _warning.ShowWarning(WarningTypes.ConfirmPassword);
            return false;
        }
    }

    private bool IsLoginValid(string login)
    {
        Debug.Log("3");

        if (!string.IsNullOrEmpty(login) && Regex.IsMatch(login, Constants.LoginPattern))
        {
            return true;
        }
        else
        {
            _warning.ShowWarning(WarningTypes.IncorrectLogin);
            return false;

        }
    }


    private bool IsEnoughSymbols()
    {
        Debug.Log("3");

        if (_passwordInputField.text.Length >= Constants.MinPasswordSymbols)
        {
            return true;
        }

        else
        {
            _warning.ShowWarning(WarningTypes.PasswordMoreSymbols);

            return false;
        }
    }

    public void DoesNameExist(Func<bool, bool> onComplete)
    {
        Debug.Log("4");

        StartCoroutine(DoesNameExistCoroutine(_nameInputField.text, onComplete));
        Debug.Log("4.1");

    }

    private IEnumerator DoesNameExistCoroutine(string name, Func<bool, bool> OnComplete)
    {
        Debug.Log("p");

        var task = DatabaseManager.Instance.DatabaseRef.Child(Constants.DatabaseUserKey).Child(Constants.DatabaseNameKey).GetValueAsync();

        Debug.Log("q");

        yield return new WaitUntil(() => task.IsCompleted);

        Debug.Log("w");


        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting data");
        }

        else
        {
            DataSnapshot snapshot = task.Result;
            foreach (var item in snapshot.Children)
            {
                if (!item.HasChild(Constants.DatabaseNameKey))
                {
                    Debug.Log("e");

                    continue;
                }
                
                if (item.Child(Constants.DatabaseNameKey).Value.ToString() == name)
                {
                    Debug.Log("r");

                    _warning.ShowWarning(WarningTypes.NameExist);
                    OnComplete?.Invoke(true);
                    yield break;
                }
                
            }
            Debug.Log("t");

            OnComplete?.Invoke(false);
        }
    }
}
