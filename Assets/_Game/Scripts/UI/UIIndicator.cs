using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIndicator : UICanvas
{
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UIIndicator>(this);
        gameObject.SetActive(false);
        base.Init();
    }
}
