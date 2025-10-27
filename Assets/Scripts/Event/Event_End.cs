using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_End : EventUnit
{
    public override void Init()
    {
        if(EventManager.Instance.EventDictionary.Count == 0)
        {
            UIManager.Instance.ShowUI<EndUI>("EndUI");
            UIManager.Instance.GetUI<EndUI>("EndUI").LeftMoveAnimation();
        }
        else
        {
            UIManager.Instance.ShowUI<WinUI>("WinUI").LeftMoveAnimation();
            UIManager.Instance.GetUI<WinUI>("WinUI").ShowAllButton();
        }

    }
}
