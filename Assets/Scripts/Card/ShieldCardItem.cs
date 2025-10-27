using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCardItem
{
    public static ShieldCardItem Instance = new ShieldCardItem();
    //护盾卡牌
    public void Cardpower(int cardValue)
    {
        if (UIManager.Instance.GetUI<FightUI>("FightUI").CardPower(cardtype.Shield))
        {
            //护甲效果
            //FightManager.Instance.DefenseCount = cardValue;
            EventManager.Instance.DefenseCount = cardValue;
            //刷新护盾
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        }
    }
}
