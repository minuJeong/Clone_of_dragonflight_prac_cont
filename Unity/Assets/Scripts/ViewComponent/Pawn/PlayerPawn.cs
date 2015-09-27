using UnityEngine;
using System.Collections;

public class PlayerPawn : Pawn
{
    #region const
    private const float SHOOT_DELAY_TIME = 0.25F;
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
        transform.position += amount;
    }
    #endregion

    #region private:
    private void Start()
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