using UnityEngine;
using System.Collections;

public class PawnHPBarControl : MonoBehaviour
{
    public Pawn OwnerPawn;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 NextPos = transform.position;
        NextPos = RectTransformUtility.WorldToScreenPoint (Game.Instance.GameCamera, OwnerPawn.transform.position);
        NextPos.z = 0.0F;
        transform.position = NextPos;
    }
}
