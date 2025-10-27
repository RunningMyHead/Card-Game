using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCardItem
{
    public static ShieldCardItem Instance = new ShieldCardItem();
    //���ܿ���
    public void Cardpower(int cardValue)
    {
        if (UIManager.Instance.GetUI<FightUI>("FightUI").CardPower(cardtype.Shield))
        {
            //����Ч��
            //FightManager.Instance.DefenseCount = cardValue;
            EventManager.Instance.DefenseCount = cardValue;
            //ˢ�»���
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        }
    }
}
