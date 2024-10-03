using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;
using System.Text.RegularExpressions;
using TMPro;
using System.Threading.Tasks;

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
        DatabaseManager.Instance.CreateUser();
        _databaseInfo.SetData(Constants.DatabaseNameKey, _nameInputField.text);
        _databaseInfo.SetData(Constants.DatabaseAvatarKey, 0);
        _databaseInfo.SetData(Constants.DatabaseScoreKey, 0);

    }

    public async Task<bool> Check()
    {
        bool nameExists = await DoesNameExist();
        _isNameFound = nameExists;

        if (IsLoginValid(_loginInputField.text)
            && IsNameNotEmpty(_nameInputField.text)
            && IsPasswordConfirmed()
            && IsEnoughSymbols()
             && _isNameFound)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private bool IsPasswordConfirmed()
    {
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

    private bool IsNameNotEmpty(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return true;
        }
        else
        {
            _warning.ShowWarning(WarningTypes.EnterName);
            return false;
        }
    }

    private bool IsEnoughSymbols()
    {
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

    public Task<bool> DoesNameExist()
    {
        var tcs = new TaskCompletionSource<bool>();
        StartCoroutine(DoesNameExistCoroutine(_nameInputField.text, (result) =>
        {
            tcs.SetResult(result);
        }));
        return tcs.Task;
    }


    private IEnumerator DoesNameExistCoroutine(string name, Action<bool> OnComplete)
    {
        var task = DatabaseManager.Instance.DatabaseRef.Child(Constants.DatabaseUserKey).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting data");
            OnComplete?.Invoke(false);
        }
        else
        {
            DataSnapshot snapshot = task.Result;
            bool nameNotFound = true;

            Debug.Log("snapshot.Children: " + snapshot.ChildrenCount);

            foreach (var item in snapshot.Children)
            {
                Debug.Log("item.Child(Constants.DatabaseNameKey).Value: " + item.Child(Constants.DatabaseNameKey).Value);
                Debug.Log("name: " + name);
                if (item.HasChild(Constants.DatabaseNameKey) && item.Child(Constants.DatabaseNameKey).Value.ToString() == name)
                {
                    _warning.ShowWarning(WarningTypes.NameExist);
                    nameNotFound = false;
                    break;
                }
            }
            Debug.Log(nameNotFound ? "Name found" : "Name not found");
            OnComplete?.Invoke(nameNotFound);
        }
    }

}
