using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NotEvent : EventUnit
{
    //���¼�״̬
    public override void Init()
    {
        //UIManager.Instance.HideUI("TalkUI");//����UI
        
        UIManager.Instance.GetUI<EventUI>("EventUI").AddALLEvent();//�����¼�������
        //UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();//���������ƶ�
        UIManager.Instance.GetUI<EventUI>("EventUI").LeftMoveAnimation();//�¼������Ƴ���
        UIManager.Instance.GetUI<EventUI>("EventUI").AllowEventBtn();//�����¼���ť
    }
}
