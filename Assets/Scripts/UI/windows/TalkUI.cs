using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class TalkUI : UIBase
{
    //public string[] texts;//�Ի�����˳��
    //public bool[] imgs;//�Ի�����˳��

    private List<string> texts;
    private List<bool> imgs;

    public Text text;//��ǰ�Ի�����
    public string player;
    public string NPC;
    public Text man;//��ǰ�Ի�����
    public Image img;
    public Sprite sprite1;
    public Sprite sprite2;

    public int Maxint;//���Ի���
    public int Curint;//��ǰ�Ի���

    public void Awake()
    {
        player = "�����Ů";
        NPC = "İ������";
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

    public void StartTalk()//��ʼ�Ի�
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
    public void finishTalk()//�����Ի�
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
        EventManager.Instance.ToPlayetNotFight();//�л�����ս������
        Show();
        StartTalk();
    }
}
