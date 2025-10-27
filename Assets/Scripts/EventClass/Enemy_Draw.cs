using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Draw : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.Draw);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        //UIManager.Instance.GetUI<FightUI>("FightUI").ShowHP();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }

    public void talk()
    {
        texts.Add("你是否知晓《九章算术》？");
        imgs.Add(false);

        texts.Add("是“算经十书”中的一本，主要阐明当时的盖天说和四分历法。");
        imgs.Add(true);

        texts.Add("是我们华夏历史了解自然的重要的一步，是数学与自然结合的精华。");
        imgs.Add(false);

        texts.Add("那么与之对应的最厉害的精华就是《太平御览》了吧！");
        imgs.Add(true);

        texts.Add("真是有意思的见解，异国少女。");
        imgs.Add(false);
    }
}
