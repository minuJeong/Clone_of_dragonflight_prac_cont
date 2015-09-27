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
        
        Vector3 sum = Vector3.zero;
        int len = Points.Length;
        for (int i = 0; i < len - 1; i++)
        {
            sum += Points [i] - Points [i + 1];
        }
        
        float progressDistance = sum.magnitude * progress;
        for (int i = 0; i < len - 1; i++)
        {
            Vector3 delta = Points [i] - Points [i + 1];
            progressDistance -= delta.magnitude;
            
            if (progressDistance < 0)
            {
                return Vector3.Lerp(Points [i + 1], Points [i], (-progressDistance / delta.magnitude));
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
        return GetPosition(LifeTime / MaxLifeTime);
    }
}
