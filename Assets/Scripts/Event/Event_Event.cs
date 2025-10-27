using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Event : EventUnit
{
    public override void Init()
    {
        UIManager.Instance.GetUI<EventUI>("EventUI").BanEventBtn();
        UIManager.Instance.GetUI<EventUI>("EventUI").RightMoveAnimation();
    }
}
