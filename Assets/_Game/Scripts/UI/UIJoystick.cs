using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJoystick : UICanvas
{
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UIJoystick>(this);
        gameObject.SetActive(false);
        base.Init();
    }
}
