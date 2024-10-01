using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class RegistrationManager : MonoBehaviour
{
    public static RegistrationManager Instance;
    public FirebaseAuth Auth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Auth = FirebaseAuth.DefaultInstance;
    }
}
