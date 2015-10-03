using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class LocalizeUIText : MonoBehaviour
{
    private const string LAN_CODE = "KR";
    public string LocalizeKey = "";
    private Text text;

    void Start()
    {
        LocalizeString();
    }
#if UNITY_EDITOR
    void Update ()
    {
        LocalizeString ();
    }
#endif

    private void LocalizeString()
    {
        if ("" != LocalizeKey)
        {
            if (StringTable.Data[LAN_CODE].hasKey (LocalizeKey))
            {
                text = GetComponent<Text>();
                text.text = StringTable.Data [LAN_CODE] [LocalizeKey].ToString();
            }
        }
    }
}
