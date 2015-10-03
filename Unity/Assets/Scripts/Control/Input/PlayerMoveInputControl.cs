using UnityEngine;
using System.Collections;

/*
 * Input controls for player pawn movement
 */
public class PlayerMoveInputControl : InputControl
{
    private const float CONTROL_SPEED_FACTOR = 0.0005F;
    public PlayerPawn m_PlayerPawn;
    private Vector3 m_PreviousPressPosition;
    private bool m_IsDragging;

    // Initialize Here
    void Start()
    {
        m_IsDragging = false;
    }

    protected override void OnPress(Vector3 pos)
    {
        base.OnPress(pos);

        m_PreviousPressPosition = pos;
        m_IsDragging = true;
    }

    protected override void OnUp(Vector3 pos)
    {
        base.OnUp(pos);

        m_IsDragging = false;
    }

    protected override void OnPressing(Vector3 pos)
    {
        base.OnPressing(pos);

        if (m_IsDragging)
        {
            Vector3 DeltaDrag = (pos - m_PreviousPressPosition) / Time.deltaTime;

            float x = DeltaDrag.x * CONTROL_SPEED_FACTOR;

            m_PlayerPawn.MoveBy(new Vector3(x, 0, 0));

            m_PreviousPressPosition = pos;
        }


    }
}
