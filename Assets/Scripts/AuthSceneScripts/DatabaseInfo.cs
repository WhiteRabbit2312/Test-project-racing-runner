using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
using System.Linq;
using Zenject;

public class DatabaseInfo : MonoBehaviour
{
    [Inject] private DatabaseManager _databaseManager;



    public void SetData<T>(string key, T data)
    {
        _databaseManager.DatabaseRef
            .Child(Constants.DatabaseUserKey)
            .Child(_databaseManager.FirebaseUser.UserId)
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
    }

    public async Task<string> GetPlayerData(string key, string userId)
    {
        var snapshot = await _databaseManager.DatabaseRef
            .Child(Constants.DatabaseUserKey)
            .Child(userId)
            .Child(key)
            .GetValueAsync();

        //Debug.LogError("key: " + key + "userId: " + userId);

        if (snapshot.Exists)
        {
            Debug.LogError("snapshot exists");
            string data = snapshot.Value.ToString();
            return data;
        }
        else
        {
            Debug.LogError("NOT EXISTS");
            return null;
        }
    }

}
