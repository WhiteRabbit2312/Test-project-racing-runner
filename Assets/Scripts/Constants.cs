using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const string DatabaseUserKey = "User";
    public const string DatabaseNameKey = "Name";
    public const string DatabaseLoginKey = "Login";
    public const string DatabasePasswordKey = "Password";
    public const string DatabaseScoreKey = "Score";
    public const string DatabaseAvatarKey = "Avatar";
    public const string NetworkRunnerName = "NetworkRunner";
    public const string SessionName = "GameSession";
    public const string LogInKey = "LogIn";
    public const string PasswordKey = "Password";

    public const int MinPasswordSymbols = 6;
    public const int AuthSceneIdx = 0;
    public const int MainMenuSceneIdx = 1;
    public const int PreGameplaySceneIdx = 2;
    public const int GameplaySceneIdx = 3;
    public const int PlayersInLeaderboardCount = 5;
    public const int DefaultValue = -1;
    public const int PlayersInSessionCount = 2;

    public const int UserLogOutIdx = 0;
    public const int UserLogInIdx = 1;

    public const string SilentAuthKey = "Silent";

    public const string LoginPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
  + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
  + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
  + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
}
