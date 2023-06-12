using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] CharacterControllerBase m_Target;
    float m_RangeAttack = 15;
    private void Start()
    {
        m_Target = GetComponentInParent<CharacterControllerBase>();
    }

    void LateUpdate()
    {
        Interpolate();
    }

    void Interpolate()
    {
        if (m_Target == null)
        {
            return;
        }
        m_RangeAttack = m_Target.GetAttackRange();
        transform.localPosition = new Vector3(0, m_RangeAttack * 1.5f, m_RangeAttack * -1);
    }
}
