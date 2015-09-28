using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        transform.localScale = new Vector3(w, h, 1.0F);
    }
}
