using UnityEngine;

/**
 * Movement Path for missile and pawn
 */
public class MovePath
{
    public Vector3[] Points;
    
    private float m_MaxLifeTime;
    public float MaxLifeTime
    {
        get
        {
            return m_MaxLifeTime;
        }
        set
        {
            m_MaxLifeTime = value;
            LifeTime = m_MaxLifeTime;
        }
    }
    
    public float LifeTime;
    
    public virtual Vector3 GetPosition(float progress)
    {
        progress = Mathf.Clamp01(progress);
        
        float SumDistance = 0.0F;
        int len = Points.Length;
        for (int i = 0; i < len - 1; i++)
        {
            SumDistance += Vector3.Distance (Points [i], Points [i + 1]);
        }
         
        float progressDistance = SumDistance * progress;
        for (int i = 0; i < len - 1; i++)
        {
            float distance = Vector3.Distance (Points [i], Points [i + 1]);
            progressDistance -= distance;
            
            if (progressDistance <= 0)
            {
                return Vector3.Lerp(Points [i + 1], Points [i], (-progressDistance / distance));
            }
        }
        
        Debug.LogError("MissilePath: Unknown error.");
        return Vector3.zero;
    }
    
    /*
     * Calling this method will consume LifeTime, and returns next position of the missile path.
     */
    public virtual Vector3 GetPositionGradually(float deltaTime)
    {
        LifeTime -= deltaTime;
        return GetPosition(1.0F - LifeTime / MaxLifeTime);
    }
}
