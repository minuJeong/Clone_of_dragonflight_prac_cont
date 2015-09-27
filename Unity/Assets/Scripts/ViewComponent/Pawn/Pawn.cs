using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
    #region MonoBehaviour Messages
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }
    #endregion

    #region Pawn Common
    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
    #endregion
}

public class PawnDataModel
{
    float HP, MaxHP, MP, MaxMP;
    MovePath path;
}