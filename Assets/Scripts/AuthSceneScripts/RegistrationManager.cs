using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class RegistrationManager : MonoBehaviour
{
    public FirebaseAuth _auth;

    public FirebaseAuth Auth
    {
        get { return _auth; }
    }

    private void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
    }
}
