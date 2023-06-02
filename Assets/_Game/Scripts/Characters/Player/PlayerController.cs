using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] UltimateJoystick m_Joystick;
    [SerializeField] CharacterAnimator m_CharacterAnimator;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] float m_MoveSpeed;
    Vector3 m_MoveDirection = Vector3.zero;
    private void Reset()
    {
        m_CharacterAnimator = GetComponentInChildren<CharacterAnimator>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        m_MoveDirection = new Vector3(m_Joystick.GetHorizontalAxis(), m_MoveDirection.y, m_Joystick.GetVerticalAxis()).normalized;
        m_Rigidbody.velocity = m_MoveDirection * m_MoveSpeed;
        RotateObject(m_MoveDirection);
    }
    private void LateUpdate()
    {
        UpdateAnim();
    }
    void UpdateAnim()
    {
        if (m_Rigidbody.velocity.magnitude > 0.3f)
        {
            m_CharacterAnimator.ChangeState(CharacterState.Run);
        }
        else
        {
            m_CharacterAnimator.ChangeState(CharacterState.Idle);
        }
    }
    private void RotateObject(Vector3 rot)
    {
        if (rot.magnitude == 0) return;
        Quaternion rotation = Quaternion.LookRotation(rot, Vector3.up);
        transform.rotation = rotation;
    }
}
