using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
//using System;

public class EventUI : UIBase
{
    public   string[] EventID;
    public  GameObject[] EventObj;
    public GameObject EventPlane;
    private List<string> EventDictionarykeysList;
    public bool EventEnd;

    public void Awake()
    {
        EventID  = new string[3];
        EventObj = new GameObject[3];
        EventDictionarykeysList = EventManager.Instance.EventDictionary.Keys.ToList();
        //events = new Event[3];
        EventEnd = false;//û��������
    }

    public void AddALLEvent()
    {
        int Cout = EventDictionarykeysList.Count;
        if(Cout > 0 && !EventEnd)
        {
            for (int i = 0; i < 3; i++)
            {
                if (EventID[i] == null)
                {
                    int j = Random.Range(0, Cout);
                    EventID[i] = EventDictionarykeysList[j];
                    EventDictionarykeysList.RemoveAt(j);
                    Cout--;
                    EventObj[i] = Instantiate(Resources.Load("Event/" + EventID[i]), EventPlane.transform) as GameObject;
                    EventObj[i].GetComponent<Event>().EventID = EventID[i];
                }
            }
        }
        if(Cout == 0 && !EventEnd )
        {
            EventEnd = true;
            //EventID[0] = "Enemy_End";
            //EventObj[0] = Instantiate(Resources.Load("Event/" + EventID[0]), EventPlane.transform) as GameObject;
            //EventObj[0].GetComponent<Event>().EventID = EventID[0];
            EventManager.Instance.EventDictionary.Add("Enemy_End", new Enemy_End());
        }
        if(EventID[0] == null && EventID[1] == null && EventID[2] == null)
        {
            EventID[0] = "Enemy_End";
            EventObj[0] = Instantiate(Resources.Load("Event/" + EventID[0]), EventPlane.transform) as GameObject;
            EventObj[0].GetComponent<Event>().EventID = EventID[0];
        }
    }
    //�¼��������ƶ�
    //public void RightMoveAnimation()
    //{
    //    // ʹ��DOTween�����ƶ������������������ƶ�ָ������
    //    transform.DOMoveX(2700, 1)
    //        .SetEase(Ease.OutQuad); // ʹ�û���Ч�����������
    //}
    ////�¼��������ƶ�
    //public void LeftMoveAnimation()
    //{
    //    // ʹ��DOTween�����ƶ������������������ƶ�ָ������
    //    transform.DOMoveX(1250, 1)
    //        .SetEase(Ease.OutQuad); // ʹ�û���Ч�����������
    //}
    //��ֹ�����¼���ť����
    public void BanEventBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (EventObj[i] != null)
            {
                EventObj[i].GetComponent<Event>().button.interactable = false;
            }
        }
    }
    //���������¼���ť����
    public void AllowEventBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (EventObj[i] != null)
            {
                EventObj[i].GetComponent<Event>().button.interactable = true;
            }
        }
    }

    public void CloseEventID(string ID)
    {
        for(int i = 0; i < EventID.Length; i++)
        {
            if (EventID[i] == ID)
            {
                EventID[i] = null;
            }
        }
    }

}
