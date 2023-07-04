using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    [SerializeField] Transform m_CameraTF;
    [SerializeField] Player m_Target;
    [SerializeField] CameraState m_State = CameraState.None;
    [SerializeField] Vector3 m_Offset;
    public float m_PlayerAttackRange = 10;
    private void Reset()
    {
        m_CameraTF = transform;
    }
    private void Awake()
    {
        ChangeState(CameraState.Menu);
    }
    private void LateUpdate()
    {
        UpdateState();
    }
    void UpdateState()
    {
        switch (m_State)
        {
            case CameraState.Menu:
                m_Offset = new Vector3(0, 3, 8);
                m_CameraTF.position = Vector3.Lerp(m_CameraTF.position, m_Target.transform.position + m_Offset, Time.deltaTime * 5f);
                m_CameraTF.LookAt(m_Target.transform);
                break;
            case CameraState.GamePlay:
                m_PlayerAttackRange = m_Target.AttackRange;
                m_Offset = new Vector3(0, m_PlayerAttackRange * 21/7, -12);
                m_CameraTF.position = m_Target.transform.position + m_Offset;
                m_CameraTF.LookAt(m_Target.transform);
                break;
            case CameraState.Shop:
                m_Offset = new Vector3(0, 1.5f, 8);
                m_CameraTF.position = Vector3.Lerp(m_CameraTF.position, m_Target.transform.position + m_Offset, Time.deltaTime * 5f);
                break;
        }
    }

    public void ChangeState(CameraState a_state)
    {
        if (m_State.Equals(a_state)) return;
        m_State = a_state;
        UpdateState();
    }
}
public enum CameraState
{ 
    None,
    Menu,
    GamePlay,
    Shop
}
