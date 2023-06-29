using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] CharacterControllerBase m_Target;
    public float m_RangeAttack = 10;
    bool m_IsInMenu = true;
    Vector3 m_Distance = new Vector3(0,35,-20);
    Vector3 m_OldTargetPos;
    private void Start()
    {
        m_OldTargetPos = m_Target.transform.position;
        LoadDistance();
    }
    void LateUpdate()
    {
        if (m_Target == null)
        {
            return;
        }
        if (m_IsInMenu)
        {
            DisplayMenu();
        }
        else
        {
            Interpolate();
        }
    }
    public void TurnMenu(bool a_turn)
    {
        m_IsInMenu = a_turn;
    }
    void DisplayMenu()
    {
        transform.position = new Vector3(0, 12, -63f);
        transform.LookAt(m_Target.transform);
    }
    void Interpolate()
    {
        LoadDistance();
        transform.position = m_Target.transform.position + m_Distance;
        transform.LookAt(m_Target.transform);
    }
    void LoadDistance()
    {
        m_RangeAttack = m_Target.GetAttackRange();
        m_Distance = PlayGamePosBase() - m_OldTargetPos;
    }
    Vector3 PlayGamePosBase()
    {
        return new Vector3(0, 35 / 10f * m_RangeAttack,  -70f) ;
    }
}
