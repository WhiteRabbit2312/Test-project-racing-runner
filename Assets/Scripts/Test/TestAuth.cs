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
        _loginInput.text = "login2@gmail.com";
        _passwordInput.text = "121212";
    }
}
