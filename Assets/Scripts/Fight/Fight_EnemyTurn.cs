using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit 
{
    public override void Init()
    {
        
        UIManager.Instance.ShowTip("����", Color.black, delegate ()
        {
            UIManager.Instance.GetUI<FightUI>("FightUI").ShowDefense();//��ʾ������UI
            EnemyManager.Instance.Setenemyattack();//��ʾ���﹥����ͼ
            FightCardManager.Instance.ClosehandCardList();//����Ѿ�ѡ�������

        });
    }
}
