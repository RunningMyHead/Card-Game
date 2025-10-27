using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Return : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.Return);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        //UIManager.Instance.GetUI<FightUI>("FightUI").ShowHP();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }

    public void talk()
    {
        texts.Add("“左准绳”“右规矩”相传这是大禹当时治水的武器。");
        imgs.Add(false);

        texts.Add("你知道这说明了什么吗，异国少女？");
        imgs.Add(false);

        texts.Add("准、绳、规、矩是华夏最早使用的数学工具？");
        imgs.Add(true);

        texts.Add("确实如此，但更说明了数学的力量可以战胜一切。");
        imgs.Add(false);

        texts.Add("但如果没有刻苦钻研的精神，实事求是的理念，数学的力量微乎其微。");
        imgs.Add(true);

        texts.Add("“终归数学只是工具，还得看使用的人”吗？很有意思见解。");
        imgs.Add(false);
    }
}
