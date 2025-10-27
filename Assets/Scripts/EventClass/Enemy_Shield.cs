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
        texts.Add("�����Ů���ݹ����ǻ���ǧ�꣬��������֪ʶ�Ļ���");
        imgs.Add(false);

        texts.Add("�ܶ��漣�������Ǹ�������ʧ����");
        imgs.Add(false);

        texts.Add("��Щѧ���һ���Ľᾧ��ȴ��������ɵĻ����ź�����Ҳ��ˡ�");
        imgs.Add(true);

        texts.Add("��������һ����׷����˼�̤���Ҹе���ŭ��");
        imgs.Add(false);

        texts.Add("��Ȼ��ˣ���ô�����ָ�����Ǻã�");
        imgs.Add(true);

        texts.Add("�����޷������𣬵�Ҳ�޷�����һ�С�");
        imgs.Add(false);

        texts.Add("֪ʶ�Ĵ����������������޷������ĵط���������ÿ���˵����ġ�");
        imgs.Add(false);
    }
}
