using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class RegistrationManager : MonoBehaviour
{
    /*
    private static RegistrationManager _instance;

    public static RegistrationManager Instance
    {
        get { return _instance; }
    }
    */
    public FirebaseAuth _auth;

    public FirebaseAuth Auth
    {
        get { return _auth; }
    }

    private void Start()
    {
        /*
        if(_instance == null)
        {
            _instance = this;
        }*/

        _auth = FirebaseAuth.DefaultInstance;
    }
}
