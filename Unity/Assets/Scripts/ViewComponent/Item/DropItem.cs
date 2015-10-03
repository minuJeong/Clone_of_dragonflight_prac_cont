using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour
{
    public Vector2 POP_POWER = new Vector2(0.1F, 0.1F);
    public float m_Gravity = -0.005F;
    public float m_HitRadius = 0.25F;
    public Vector3 m_Velocity;
    private Rect m_ValidRect;

    void Start()
    {
        float w = Game.Instance.GameCamera.orthographicSize * Game.Instance.GameCamera.aspect * 2;
        float h = Game.Instance.GameCamera.orthographicSize * 2;

        m_ValidRect = new Rect(-w / 2, -h / 2, w, h);
    }

    void Update()
    {
        Vector3 tempPos = transform.position;
        tempPos += m_Velocity;
        m_Velocity += new Vector3(0, m_Gravity, 0);

        if (tempPos.x < m_ValidRect.xMin)
        {
            tempPos.x = m_ValidRect.xMin;
            m_Velocity.x *= -.5F;
        } else if (tempPos.x > m_ValidRect.xMax)
        {
            tempPos.x = m_ValidRect.xMax;
            m_Velocity.x *= -.5F;
        }

        transform.position = tempPos;

        if (transform.position.y < m_ValidRect.yMin)
        {
            Die();
        }
    }

    void OnEnable()
    {
        float x = Random.Range(-POP_POWER.x, POP_POWER.x);
        float y = Random.Range(0.0F, POP_POWER.y);
        m_Velocity = new Vector3(x, y);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        DropItemSpawnControl.Instance.DropItemPool.push(gameObject);
    }

    public void OnGather()
    {
        Game.Instance.CoinGain += 10;
    }
}
