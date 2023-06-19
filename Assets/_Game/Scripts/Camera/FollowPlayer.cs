using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] CharacterControllerBase m_Target;
    float m_RangeAttack = 10;
    bool m_IsInMenu = true;
    Vector3 m_Distance = new Vector3(0,27,-24);
    private void Start()
    {
    }
    void Update()
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
        transform.localPosition = new Vector3(0, 12, -63f);
        transform.localRotation = Quaternion.Euler(35, 0, 0);
    }
    void Interpolate()
    {
        m_RangeAttack = m_Target.GetAttackRange();
        transform.localPosition = new Vector3(0, m_RangeAttack * 27/10f, m_RangeAttack * (-74f/10f));
        transform.localRotation = Quaternion.Euler(50, 0, 0);
        Vector3 diff = m_Target.transform.position - transform.position;
        transform.position += diff + m_Distance;
    }
}
