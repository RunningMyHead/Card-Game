using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��һغ�
public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        UIManager.Instance.ShowTip("����", Color.black, delegate ()
        {
            //���»���ֵ
            EventManager.Instance.DefenseCount = 0;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
            //��ʾ������UI
            UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
            //���ع��﹥����ͼ
            EnemyManager.Instance.Setfalsenemyattack();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateintegralCount();//ˢ�»���
            FightCardManager.Instance.ClosehandCardList();//�Ƴ���ѡ�еĿ����б�
        });
    }
}
