using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class DatabaseInfo : MonoBehaviour
{
    public void SetData<T>(string key, T data)
    {
        DatabaseManager.Instance.DatabaseRef
            .Child(Constants.DatabaseUserKey)
            .Child(DatabaseManager.Instance.CreateUser().UserId)
            .Child(key)
            .SetValueAsync(data);
    }

    /*
    public IEnumerator GetSnapshot(string key, string userId)
    {
        var task = DatabaseManager.Instance.DatabaseRef.Child(Constants.DatabaseUserKey).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting highscore");
        }
        else
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.HasChild(userId))
            {
                var user = snapshot.Child(userId);
                MySnapshot(user);
                _data = user.Child(key).ToString();
            }

        }


    }*/

    
    public void GetUserHighscore(string key, Action<object> onComplete)
    {
        StartCoroutine(GetUserDataCoroutine(key, onComplete));
    }
    
    private IEnumerator GetUserDataCoroutine(string key, Action<object> onComplete)
    {
        var task = DatabaseManager.Instance.DatabaseRef.Child(Constants.DatabaseUserKey).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting data");
        }
        else
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.HasChild(key))
            {
                DataSnapshot mySnapshot = snapshot.Child(key);

                if (mySnapshot.Value != null)
                {
                    var score = snapshot.Value;
                    onComplete?.Invoke(score);


                }
                else
                {
                    onComplete?.Invoke(null);  // Или значение по умолчанию
                }
            }
        }
    }
}
