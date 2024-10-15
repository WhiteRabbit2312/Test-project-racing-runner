using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerData : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);

    }

    public string NickName
    {
        get;
        set;
    }

    public int AvatarId
    {
        get;
        set;
    }
}
