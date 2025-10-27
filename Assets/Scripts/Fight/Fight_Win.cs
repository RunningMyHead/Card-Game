using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Win : FightUnit
{
    //private List<string> texts;
    //private List<bool> imgs;
    public override void Init()
    {
        //texts = new List<string>();
        //imgs = new List<bool>();
        //talk();
        EventManager.Instance.DefenseCount = 0;
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        AudioManager.Instance.PlayBGM("Win");//播放win音乐

        UIManager.Instance.GetUI<FightUI>("FightUI").HieAllFightBtn();//隐藏Fight标志
        //UIManager.Instance.ShowUI<WinUI>("WinUI").Show();//显示胜利界面
        //UIManager.Instance.GetUI<WinUI>("WinUI").ShowAllButton();//显示胜利界面所有按钮
        //UIManager.Instance.GetUI<WinUI>("WinUI").integralText.text = EventManager.Instance.integralint.ToString();
        //UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemDownPos();
        //UIManager.Instance.ShowUI<TalkUI>("TalkUI");
    
    }

    //public void talk()
    //{
    //    switch (cardtype.Sword)
    //    {
    //        case cardtype.Sword:
    //            Swordtalk();
    //            break;
    //        case cardtype.Shield:
    //            Shieldtalk();
    //            break;
    //        case cardtype.Return:
    //            Returntalk();
    //            break;
    //        case cardtype.Draw:
    //            Drawtalk();
    //            break;
    //    }
    //}

    //public void Swordtalk()
    //{
    //    texts.Add("Sword1");
    //    imgs.Add(true);

    //    texts.Add("Sword2");
    //    imgs.Add(true);

    //    texts.Add("Sword3");
    //    imgs.Add(false);

    //    texts.Add("Sword4");
    //    imgs.Add(true);

    //    texts.Add("Sword5");
    //    imgs.Add(false);
    //}

    //public void Shieldtalk()
    //{
    //    texts.Add("Shield1");
    //    imgs.Add(true);

    //    texts.Add("Shield2");
    //    imgs.Add(true);

    //    texts.Add("Shield3");
    //    imgs.Add(false);

    //    texts.Add("Shield4");
    //    imgs.Add(true);

    //    texts.Add("Shield5");
    //    imgs.Add(false);
    //}

    //public void Returntalk()
    //{
    //    texts.Add("Return1");
    //    imgs.Add(true);

    //    texts.Add("Return2");
    //    imgs.Add(true);

    //    texts.Add("Return3");
    //    imgs.Add(false);

    //    texts.Add("Return4");
    //    imgs.Add(true);

    //    texts.Add("Return5");
    //    imgs.Add(false);
    //}

    //public void Drawtalk()
    //{
    //    texts.Add("Draw1");
    //    imgs.Add(true);

    //    texts.Add("Draw2");
    //    imgs.Add(true);

    //    texts.Add("Draw3");
    //    imgs.Add(false);

    //    texts.Add("Draw4");
    //    imgs.Add(true);

    //    texts.Add("Draw5");
    //    imgs.Add(false);
    //}
}
