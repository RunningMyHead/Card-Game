using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sword : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.Sword);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }
    public void talk()
    {
        texts.Add("华夏文明的数学成就宛若天上星般璀璨。");
        imgs.Add(true);

        texts.Add("数学包罗万象又特立独行，它是一切的基地，又是了一切的顶点");
        imgs.Add(false);

        texts.Add("这片大地从古至今从不缺乏想穷尽数学的人，他们无处不了解，无处不探索。");
        imgs.Add(false);

        texts.Add("或是正确或是错误，都是对后人宝贵的经验");
        imgs.Add(false);

        texts.Add("这片大地困不住我们的心，这片天空遮不住我们的眼！");
        imgs.Add(true);

        texts.Add("哈哈哈，真是有活力啊。");
        imgs.Add(false);
    }
}
