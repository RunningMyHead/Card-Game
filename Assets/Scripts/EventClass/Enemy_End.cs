using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_End : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.NPC);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }
    public void talk()
    {
        texts.Add("啊！莫非您就是……古中国最伟大的数学家之一——刘徽吗？！");
        imgs.Add(true);

        texts.Add("不敢当，不敢当……只是个腐朽的老头罢了。");
        imgs.Add(false);

        texts.Add("您的著作我曾拜读多次！");
        imgs.Add(true);

        texts.Add("您对于数学那犀利的见解，“中国数学史上的牛顿”名副其实。");
        imgs.Add(true);

        texts.Add("只是借助了前人的智慧罢了。");
        imgs.Add(false);

        texts.Add("莫要谦虚啊！您现在也是前人，不不不，是数学巨人啊！");
        imgs.Add(true);

        texts.Add("真是讨人欢喜的少女，那么你要借助我的智慧吗？");
        imgs.Add(false);

        texts.Add("当然，我来到这片巨龙盘旋之地，就是为了寻求那数学的奥秘。");
        imgs.Add(true);

        texts.Add("那么来吧，少女，我承认你对数学的执着。");
        imgs.Add(false);
    }
}
