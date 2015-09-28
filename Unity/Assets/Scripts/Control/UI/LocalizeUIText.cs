using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class LocalizeUIText : MonoBehaviour
{
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
            text = GetComponent<Text>();
            text.text = StringTable.Data [LocalizeKey].ToString();
        }
    }
}
