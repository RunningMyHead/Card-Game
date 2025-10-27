using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.Shield);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }

    public void talk()
    {
        texts.Add("异国少女，纵观我们华夏千年，不乏对于知识的毁灭。");
        imgs.Add(false);

        texts.Add("很多真迹，甚至是副本都已失传。");
        imgs.Add(false);

        texts.Add("那些学者穷尽一生的结晶，却被如此轻松的毁灭，遗憾吗？我也如此。");
        imgs.Add(true);

        texts.Add("不，他们一生的追求被如此践踏，我感到愤怒。");
        imgs.Add(false);

        texts.Add("既然如此，那么我们又该如何是好？");
        imgs.Add(true);

        texts.Add("毁灭无法被消灭，但也无法毁灭一切。");
        imgs.Add(false);

        texts.Add("知识的传播，传播到毁灭无法波及的地方，传播到每个人的内心。");
        imgs.Add(false);
    }
}
