using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteCollection : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();

    /**
     * [!] Can be null
     */
    public Sprite GetSpriteByName(string spriteName)
    {
        int len = Sprites.Count;
        for (int i = 0; i < len; i++)
        {
            if (Sprites [i].name == spriteName)
            {
                return Sprites [i];
            }
        }

        for (int i = 0; i < len; i++)
        {
            Debug.Log(Sprites [i].name);
        }
        return null;
    }
}