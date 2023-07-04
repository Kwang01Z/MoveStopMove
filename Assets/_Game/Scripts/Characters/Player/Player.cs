using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] UltimateJoystick m_Joystick;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] IndicatorEnemy m_Indicator;
    AudioClip deathSound;
    Vector3 m_MoveDirection = Vector3.zero;
    public bool JustDeath;
    private void Reset()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MoveSpeed = 10;
    }
    private void Start()
    {
        UpdateSetFull();
        UpdateWeapon();
        UpdateHeadSkin();
        UpdatePantSkin();
        deathSound = Resources.Load<AudioClip>("Sounds/stickman_dead2");
    }
    protected override void UpdateAnim()
    {
        MoveUpdate();
        IndicateEnemy();
        StateUpdate();
    }
    void StateUpdate()
    {
        if (m_MoveVelocity.magnitude > 0.3f)
        {
            m_Animator.ChangeState(CharacterState.Run);
            RotateObject(m_MoveDirection);
        }
        else
        {
            if (CanAttack())
            {
                m_Animator.ChangeState(CharacterState.Attack);
                Character target = LoadTaget();
                Vector3 dir = target.transform.position - transform.position;
                RotateObject(dir.normalized);
            }
            else
            {
                m_Animator.ChangeState(CharacterState.Idle);
            }
        }
    }    
    void MoveUpdate()
    {
        m_MoveDirection = new Vector3(m_Joystick.GetHorizontalAxis(), m_MoveDirection.y, m_Joystick.GetVerticalAxis()).normalized;
        m_MoveVelocity = m_MoveDirection * m_MoveSpeed;
        m_Rigidbody.velocity = m_MoveVelocity;
    }
    void IndicateEnemy()
    {
        Character target = LoadTaget();
        if (target != null)
        {
            m_Indicator.SetTarget(target.transform.position);
        }
        else
        {
            m_Indicator.OffIndicator();
        }
        m_Indicator.SetFollow(m_AttackRange, true);
    }
    protected override void OnDead()
    {
        base.OnDead();
        m_Rigidbody.velocity = Vector3.zero;
        m_Indicator.OffIndicator();
        SoundManager.Instance.OnPlayAudioClip(deathSound);
        if (!JustDeath)
        {
            UIManager.Instance.OpenUI<UIRevive>();
        }
        else
        {
            UIManager.Instance.OpenUI<UITotal>();
        }
    }
    public void UpdateWeapon()
    {
        m_CurrentWeapon = SaveManager.Instance.CharacterData.EquippedWeapon;
        if (m_MainWeapon != null)
        {
            Destroy(m_MainWeapon.gameObject);
        }
        m_MainWeapon = WeaponManager.Instance.GetWeapon(m_CurrentWeapon, m_CharWeaponHolder);
    }
    public void UpdateHeadSkin()
    {
        string skin = SaveManager.Instance.CharacterData.EquippedHeadSkin;
        if (!skin.Equals("") && skin != null)
        {
            m_CharacterSkin.SetHeadSkin(skin);
        }
    }
    public void UpdatePantSkin()
    {
        string skin = SaveManager.Instance.CharacterData.EquippedPantSkin;
        if (!skin.Equals("") && skin != null)
        {
            Material mat = SkinManager.Instance.GetPantSkin(skin).Material;
            m_CharacterSkin.SetPant(mat);
        }       
    }
    public void UpdateSetFull()
    {
        string skin = SaveManager.Instance.CharacterData.EquippedSetFull;
        if (!skin.Equals("") && skin != null)
        {
            SetFullInfo set = SkinManager.Instance.GetSetFull(skin);
            m_CharacterSkin.SetFull(set);
        }
    }
    public void Reborn()
    {
        SetDead(false);
        UpdateWeapon();
    }
}
