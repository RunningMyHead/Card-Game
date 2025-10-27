using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit 
{
    public override void Init()
    {
        
        UIManager.Instance.ShowTip("防御", Color.black, delegate ()
        {
            UIManager.Instance.GetUI<FightUI>("FightUI").ShowDefense();//显示防御键UI
            EnemyManager.Instance.Setenemyattack();//显示怪物攻击意图
            FightCardManager.Instance.ClosehandCardList();//清空已经选择的手牌

        });
    }
}
