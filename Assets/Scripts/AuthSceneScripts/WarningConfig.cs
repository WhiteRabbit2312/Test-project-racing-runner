using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI", fileName = "Warning")]
public class WarningConfig : ScriptableObject
{
    public string WrongPassword;
    public string EnterLogin;
    public string EnterPassword;
    public string ConfirmPassword;
    public string EnterName;
    public string NameExist;
    public string PasswordMoreSymbols;
}
