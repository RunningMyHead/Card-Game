using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家回合
public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        UIManager.Instance.ShowTip("攻击", Color.black, delegate ()
        {
            //更新护盾值
            EventManager.Instance.DefenseCount = 0;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
            //显示攻击键UI
            UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
            //隐藏怪物攻击意图
            EnemyManager.Instance.Setfalsenemyattack();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateintegralCount();//刷新积分
            FightCardManager.Instance.ClosehandCardList();//移除被选中的卡牌列表
        });
    }
}
