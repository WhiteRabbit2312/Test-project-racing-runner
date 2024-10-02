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
    public const int MinPasswordSymbols = 6;

    public const string LoginPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
  + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
  + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
  + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
}
