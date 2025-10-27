using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class TalkUI : UIBase
{
    //public string[] texts;//对话内容顺序
    //public bool[] imgs;//对话立绘顺序

    private List<string> texts;
    private List<bool> imgs;

    public Text text;//当前对话内容
    public string player;
    public string NPC;
    public Text man;//当前对话名字
    public Image img;
    public Sprite sprite1;
    public Sprite sprite2;

    public int Maxint;//最大对话量
    public int Curint;//当前对话量

    public void Awake()
    {
        player = "异国少女";
        NPC = "陌生老者";
        Register("Btn").onClick = Btn;
        Register("skip").onClick = skip;
        Register("Back").onClick = Back;
    }
    public void Init ( List<string> str1 , List<bool> Is)
    {
        texts = new List<string>();
        imgs = new List<bool>();
        texts = str1;
        imgs = Is;
        Maxint = texts.Count;
        Curint = 0;
    }

    public void StartTalk()//开始对话
    {
        if(Curint == Maxint)
        {
            finishTalk();
        }
        else
        {
            text.text = texts[Curint];
            if (imgs[Curint])
            {
                img.overrideSprite = sprite1;
                man.text = player;
            }
            else
            {
                img.overrideSprite = sprite2;
                man.text = NPC;
            }
        }
    }
    public void finishTalk()//结束对话
    {
        if(FightManager.Instance.fightUnit is Fight_Loss)
        {
            EventManager.Instance.ChangeType(eventtype.Die);
        }
        else if(FightManager.Instance.fightUnit is Fight_Win)
        {
            EventManager.Instance.ChangeType(eventtype.End);
        }
        else
        {   
            EventManager.Instance.ChangeType(eventtype.NotEvent);
        }
        Hide();

    }
    private void Btn(GameObject obj, PointerEventData pDate)
    {
        if(Curint != Maxint)
        {
            Curint++;
        }
        StartTalk();
    }

    private void skip(GameObject obj, PointerEventData pDate)
    {
        Curint = Maxint;
        finishTalk();
    }

    private void Back(GameObject obj, PointerEventData pDate)
    {
        if(Curint!=0)
        {
            Curint--;
        }
        StartTalk();
    }

    public void TALK()
    {
        EventManager.Instance.ToPlayetNotFight();//切换到非战斗立绘
        Show();
        StartTalk();
    }
}
