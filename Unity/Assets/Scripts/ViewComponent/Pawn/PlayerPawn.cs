using UnityEngine;
using System.Collections;

public class PlayerPawn : Pawn
{
    #region const
    private const float SHOOT_DELAY_TIME = 0.25F;
    private const float LEFT_CAGE = -0.5F;
    private const float RIGHT_CAGE = 0.5F;
    #endregion

    #region private:
    private WaitForSeconds m_WaitTime;
    private bool m_IsShooting;
    #endregion

    #region public:
    public void StartShoot()
    {
        m_IsShooting = true;
    }
    
    public void StopShoot()
    {
        m_IsShooting = false;
    }
    
    public void MoveBy(Vector3 amount)
    {
        Vector3 NextPos = transform.position;
        NextPos += amount;
        NextPos.x = Mathf.Max (LEFT_CAGE, NextPos.x);
        NextPos.x = Mathf.Min (RIGHT_CAGE, NextPos.x);

        transform.position = NextPos;
    }
    #endregion

    #region private:
    protected override void Start()
    {
        // activate shoot by default
        StartShoot();

        StartCoroutine("ShootCycle");

        m_WaitTime = new WaitForSeconds(SHOOT_DELAY_TIME);
    }

    private IEnumerator ShootCycle()
    {
        while (true)
        {
            if (m_IsShooting)
            {
                Shoot();
            }

            yield return m_WaitTime;
        }
    }

    private void Shoot()
    {

    }
    #endregion
}