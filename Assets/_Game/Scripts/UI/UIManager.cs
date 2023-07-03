using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class UIManager : Singleton<UIManager>
{
    //dict for quick query UI prefab
    Dictionary<Type, UICanvas> m_UICanvasPrefab = new Dictionary<Type, UICanvas>();

    //list from resource
    [SerializeField] UICanvas[] m_UIResources;

    //dict for UI active
    Dictionary<Type, UICanvas> m_UICanvas = new Dictionary<Type, UICanvas>();

    [SerializeField] Transform m_CanvasParentTF;
    [SerializeField] Camera m_ItemCamera;

    #region Canvas

    public T OpenUI<T>() where T : UICanvas
    {
        UICanvas canvas = GetUI<T>();

        canvas.Setup();
        canvas.Open();

        return canvas as T;
    }

    public void CloseUI<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().Close();
        }
    }

    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && m_UICanvas[typeof(T)].gameObject.activeInHierarchy;
    }


    public bool IsLoaded<T>() where T : UICanvas
    {
        Type type = typeof(T);
        return m_UICanvas.ContainsKey(type) && m_UICanvas[type] != null;
    }

    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            UICanvas canvas = Instantiate(GetUIPrefab<T>(), m_CanvasParentTF);
            m_UICanvas[typeof(T)] = canvas;
        }

        return m_UICanvas[typeof(T)] as T;
    }
    public void PushCanvas<T>(T a_uiCanvas) where T:UICanvas
    {
        if (!IsLoaded<T>())
        {
            Type type = typeof(T);
            m_UICanvas[type] = a_uiCanvas;
        }
    }

    private T GetUIPrefab<T>() where T : UICanvas
    {
        if (!m_UICanvasPrefab.ContainsKey(typeof(T)))
        {
            if (m_UIResources == null)
            {
                //uiResources = Resources.LoadAll<UICanvas>("UI/");
                Debug.LogError("Not exit canvas " + typeof(T) + " in Resources");
            }

            for (int i = 0; i < m_UIResources.Length; i++)
            {
                if (m_UIResources[i] is T)
                {
                    m_UICanvasPrefab[typeof(T)] = m_UIResources[i];
                    break;
                }
            }
        }

        return m_UICanvasPrefab[typeof(T)] as T;
    }


    #endregion

    #region Back Button

    Dictionary<UICanvas, UnityAction> m_BackActionEvents = new Dictionary<UICanvas, UnityAction>();
    List<UICanvas> m_BackCanvas = new List<UICanvas>();
    UICanvas BackTopUI
    {
        get
        {
            UICanvas canvas = null;
            if (m_BackCanvas.Count > 0)
            {
                canvas = m_BackCanvas[m_BackCanvas.Count - 1];
            }

            return canvas;
        }
    }


    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape) && BackTopUI != null)
        {
            m_BackActionEvents[BackTopUI]?.Invoke();
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!m_BackActionEvents.ContainsKey(canvas))
        {
            m_BackActionEvents.Add(canvas, action);
        }
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!m_BackCanvas.Contains(canvas))
        {
            m_BackCanvas.Add(canvas);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        m_BackCanvas.Remove(canvas);
    }

    /// <summary>
    /// CLear backey when comeback index UI canvas
    /// </summary>
    public void ClearBackKey()
    {
        m_BackCanvas.Clear();
    }

    #endregion
    public void TurnItemCamera(bool a_active)
    {
        m_ItemCamera.gameObject.SetActive(a_active);
    }
}
