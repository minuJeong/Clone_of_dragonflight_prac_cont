using UnityEngine;
using System.Collections;

public class ResultSceneControl : MonoBehaviour
{
    void Awake()
    {
        if (null == App.Instance)
        {
            Application.LoadLevel (App.HOME_LEVEL);
            return;
        }
    }

    public void GoScene(string SceneName)
    {
        Application.LoadLevel(SceneName);
    }
}