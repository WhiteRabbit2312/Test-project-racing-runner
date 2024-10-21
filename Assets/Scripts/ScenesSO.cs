using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSO", menuName = "SceneSO")]
public class ScenesSO : ScriptableObject
{
    public int AuthSceneIdx;
    public int MainMenuSceneIdx;
    public int PreGameplaySceneIdx;
    public int GameplaySceneIdx;
}
