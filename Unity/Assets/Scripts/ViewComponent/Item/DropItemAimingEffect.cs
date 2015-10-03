using UnityEngine;
using System.Collections;

public class DropItemAimingEffect : MonoBehaviour
{   
    private float FADING_TIME_SCALE = 0.5F;
    private SpriteRenderer ren;

    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float rad = Mathf.Deg2Rad * Time.realtimeSinceStartup * FADING_TIME_SCALE;
        Color tempClr = ren.color;
        tempClr.a = (Mathf.Sin(rad) + 1.0F) * .5F;
        ren.color = tempClr;
    }
}
