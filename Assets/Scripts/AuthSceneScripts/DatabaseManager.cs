using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class DatabaseManager : MonoBehaviour
{
    
    private static DatabaseManager _instance;
    public static DatabaseManager Instance
    {
        get { return  _instance;  }
    }
    
    private DatabaseReference _databaseRef;
    public DatabaseReference DatabaseRef
    {
        get { return _databaseRef; }
    }

    private FirebaseUser _firebaseUser;
    public FirebaseUser FirebaseUser
    {
        get { return _firebaseUser; }
    }

    private void Awake()
    {
        
        if (_instance == null)
        {
            _instance = this;
        }
        _databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void CreateUser()
    {

        Debug.Log("Create user");
        _firebaseUser = FirebaseAuth.DefaultInstance.CurrentUser;
    }

}
