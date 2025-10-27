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
        texts.Add("������������ѧ�ɾ����������ǰ��財�");
        imgs.Add(true);

        texts.Add("��ѧ�����������������У�����һ�еĻ��أ�������һ�еĶ���");
        imgs.Add(false);

        texts.Add("��Ƭ��شӹ�����Ӳ�ȱ�������ѧ���ˣ������޴����˽⣬�޴���̽����");
        imgs.Add(false);

        texts.Add("������ȷ���Ǵ��󣬶��ǶԺ��˱���ľ���");
        imgs.Add(false);

        texts.Add("��Ƭ�������ס���ǵ��ģ���Ƭ����ڲ�ס���ǵ��ۣ�");
        imgs.Add(true);

        texts.Add("�������������л�������");
        imgs.Add(false);
    }
}
