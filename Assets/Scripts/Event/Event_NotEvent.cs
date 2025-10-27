using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NotEvent : EventUnit
{
    //非事件状态
    public override void Init()
    {
        //UIManager.Instance.HideUI("TalkUI");//隐藏UI
        
        UIManager.Instance.GetUI<EventUI>("EventUI").AddALLEvent();//补充事件至三个
        //UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();//卡牌向下移动
        UIManager.Instance.GetUI<EventUI>("EventUI").LeftMoveAnimation();//事件栏左移出现
        UIManager.Instance.GetUI<EventUI>("EventUI").AllowEventBtn();//启用事件按钮
    }
}
