using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Extensions;

public class DatabaseInfo : MonoBehaviour
{
    public void SetData<T>(string key, T data)
    {
        DatabaseManager.Instance.DatabaseRef
            .Child(Constants.DatabaseUserKey)
            .Child(DatabaseManager.Instance.FirebaseUser.UserId)
            .Child(key)
            .SetValueAsync(data);
    }

    public async Task<Dictionary<string, PlayerData>> GetSortedScoresAsync()
    {
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.GetReference(Constants.DatabaseUserKey);

        Dictionary<string, PlayerData> playerDictionary = new Dictionary<string, PlayerData>();

        List<PlayerData> playerList = new List<PlayerData>();

        var task = await databaseReference
            .OrderByChild(Constants.DatabaseScoreKey)
            .GetValueAsync();


        if (task.Exists)
        {
            DataSnapshot snapshot = task;

            foreach (DataSnapshot playerSnapshot in snapshot.Children)
            {
                string playerId = playerSnapshot.Key;

                string playerName = playerSnapshot.Child(Constants.DatabaseNameKey).Value.ToString();
                int score = int.Parse(playerSnapshot.Child(Constants.DatabaseScoreKey).Value.ToString());

                PlayerData player = new PlayerData(playerName, score);

                playerDictionary.Add(playerId, player);
            }
            
            playerDictionary = playerDictionary
                .OrderByDescending(p => p.Value.Score)  
                .ToDictionary(pair => pair.Key, pair => pair.Value); 
        }

        return playerDictionary;

        /*if (task.Exists)
        {
            DataSnapshot snapshot = task;

            foreach (DataSnapshot playerSnapshot in snapshot.Children)
            {
                string playerName = playerSnapshot.Child(Constants.DatabaseNameKey).Value.ToString();
                int score = int.Parse(playerSnapshot.Child(Constants.DatabaseScoreKey).Value.ToString());

                PlayerData player = new PlayerData(playerName, score);
                playerList.Add(player);

                
            }

            playerList.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));
            
        }

        return playerList;*/
    }

    public async Task<int> GetAvatarID()
    {
        var snapshot = await DatabaseManager.Instance.DatabaseRef
            .Child(Constants.DatabaseUserKey)
            .Child(DatabaseManager.Instance.FirebaseUser.UserId)
            .Child(Constants.DatabaseAvatarKey)
            .GetValueAsync();

        if (snapshot.Exists)
        {
            string avatarId = snapshot.Value.ToString();
            return int.Parse(avatarId);
        }
        else
        {
            Debug.LogError("Аватар не найден.");
            return -1;
        }

        /*
        DatabaseManager.Instance.DatabaseRef.Child(Constants.DatabaseUserKey)
            .Child(DatabaseManager.Instance.FirebaseUser.UserId)
            .Child(Constants.DatabaseAvatarKey)
            .GetValueAsync()
            .ContinueWithOnMainThread(task => {

            if (task.IsCompleted && task.Result.Exists)
            {
                string avatarId = task.Result.Value.ToString();
                int selectedAvatarIndex = int.Parse(avatarId);
            }
            else
            {
                Debug.LogError("Ошибка при загрузке аватара или аватар не найден");
            }
        });*/
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


    /*
    private void GetSortedScores(Action<List<PlayerData>> OnComplete)
    {
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.GetReference(Constants.DatabaseUserKey);


        databaseReference
            .OrderByChild(Constants.DatabaseScoreKey) 
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    List<PlayerData> playerList = new List<PlayerData>();

                    foreach (DataSnapshot playerSnapshot in snapshot.Children)
                    {
                        string playerName = playerSnapshot.Child(Constants.DatabaseNameKey).Value.ToString();
                        int score = int.Parse(playerSnapshot.Child(Constants.DatabaseScoreKey).Value.ToString());

                        PlayerData player = new PlayerData(playerName, score);
                        playerList.Add(player);
                    }

                    playerList.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));

                    OnComplete?.Invoke(playerList);

                    foreach (var player in playerList)
                    {
                        Debug.Log("Player: " + player.Name + " - Score: " + player.Score);
                    }
                }
            });
    }*/

    /*
    private IEnumerator GetUserDataCoroutine()
    {

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting data");
        }
        else
        {
            DataSnapshot snapshot = task.Result;

            foreach (var item in snapshot.Children)
            {
                PlayerData playerData = new PlayerData();

                playerData.Name = item.Child(Constants.DatabaseNameKey).Value.ToString();
                playerData.Score = (int)item.Child(Constants.DatabaseScoreKey).Value;
                playerData.AvatarId = (int)item.Child(Constants.DatabaseScoreKey).Value;


            }
        }
    }*/
}
