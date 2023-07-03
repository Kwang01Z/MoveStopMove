using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICanvas : MonoBehaviour
{
    //public bool IsAvoidBackKey = false;
    
    [Header("Main UI Canvas Elements")]
    protected RectTransform m_RectTransform;
    protected Animator m_Animator;
    protected bool m_IsDestroyOnClose = false;
    protected bool m_IsInit = false;
    protected float m_OffsetY = 0;
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();
        Init();
    }

    public virtual void Init()
    {
        float ratio = (float)Screen.height / (float)Screen.width;
        // xu ly tai tho
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = m_RectTransform.offsetMin;
            Vector2 rightTop = m_RectTransform.offsetMax;
            rightTop.y = -100f;
            leftBottom.y = 0f;
            m_RectTransform.offsetMax = rightTop;
            m_RectTransform.offsetMin = leftBottom;
            m_OffsetY = 100f;
        }
        m_IsInit = true;
    }

    public virtual void Setup()
    {
        UIManager.Instance.AddBackUI(this);
        UIManager.Instance.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        //anim
    }

    public virtual void Close()
    {
        UIManager.Instance.RemoveBackUI(this);
        //anim
        gameObject.SetActive(false);
        if (m_IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
    }
}
