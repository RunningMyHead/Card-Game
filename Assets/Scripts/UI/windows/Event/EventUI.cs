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
        EventEnd = false;//没到最终章
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
    //事件栏向右移动
    //public void RightMoveAnimation()
    //{
    //    // 使用DOTween创建移动动画，将物体向右移动指定距离
    //    transform.DOMoveX(2700, 1)
    //        .SetEase(Ease.OutQuad); // 使用缓动效果，先慢后快
    //}
    ////事件栏向左移动
    //public void LeftMoveAnimation()
    //{
    //    // 使用DOTween创建移动动画，将物体向左移动指定距离
    //    transform.DOMoveX(1250, 1)
    //        .SetEase(Ease.OutQuad); // 使用缓动效果，先慢后快
    //}
    //禁止所有事件按钮触发
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
    //允许所有事件按钮触发
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
