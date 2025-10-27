using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Init : EventUnit
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()//初始化
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();

        FightCardManager.Instance.Init();//初始化卡牌
        EventManager.Instance.Init();//初始化玩家数值，初始化事件

        UIManager.Instance.ShowUI<FightUI>("FightUI");//调用战斗UI
        UIManager.Instance.ShowUI<WinUI>("WinUI");//调用胜利UI
        UIManager.Instance.ShowUI<CardCloseUI>("CardCloseUI");//调用删卡UI
        UIManager.Instance.ShowUI<EventUI>("EventUI");//调用事件栏

 
        //UIManager.Instance.GetUI<FightUI>("FightUI").Stooptime(1f);
        UIManager.Instance.ShowUI<TalkUI>("TalkUI");
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
        
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(8);//生成初始卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").updateCardItemPos();//移动卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();
    }

    public void talk()
    {
        texts.Add("我早听闻东方的巨龙盘旋与此，数学的高楼在此建立。");
        imgs.Add(true);

        texts.Add("我想穷尽数学之奥秘，因此前来拜学。");
        imgs.Add(true);

        texts.Add("奇装异服的少女哟，穷尽数学奥秘，你可还真是心高气傲。");
        imgs.Add(false);

        texts.Add("你，你是谁？什么时候？");
        imgs.Add(true);

        texts.Add("不必在意我，异国少女，你若真想穷尽数学奥秘，那绝非轻松。");
        imgs.Add(false);

        texts.Add("你在质疑我的坚韧与毅力吗？真是不爽。");
        imgs.Add(true);

        texts.Add("那么就证明给我看吧，用你的“坚韧”、“毅力”……");
        imgs.Add(false);

        texts.Add("还有“数学”。");
        imgs.Add(false);
    }

}
