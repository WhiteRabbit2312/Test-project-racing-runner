using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestAuth : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;

    void Start()
    {
        _loginInput.text = "qq@fire.com";
        _passwordInput.text = "123123";
    }
}
