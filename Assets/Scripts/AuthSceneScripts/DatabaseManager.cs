using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class DatabaseManager : MonoBehaviour
{
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
        _databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void GetUser()
    {
        _firebaseUser = FirebaseAuth.DefaultInstance.CurrentUser;
    }

}
