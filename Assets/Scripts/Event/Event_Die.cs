using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Die : EventUnit
{
    public override void Init()
    {
        UIManager.Instance.ShowUI<LossUI>("LossUI");
        UIManager.Instance.GetUI<LossUI>("LossUI").LeftMoveAnimation();//integralText.text = EventManager.Instance.integralint.ToString();
    }
}
