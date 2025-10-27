using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_End : EventClass
{
    private List<string> texts;
    private List<bool> imgs;
    public override void Init()
    {
        texts = new List<string>();
        imgs = new List<bool>();
        talk();
        EventManager.Instance.ToPlayerFight();
        EnemyManager.Instance.LoadRes(cardtype.NPC);
        UIManager.Instance.GetUI<FightUI>("FightUI").CardItemUpPos();
        UIManager.Instance.GetUI<FightUI>("FightUI").ShowAttack();
        FightManager.Instance.ChangeType(FightType.Player);
        UIManager.Instance.GetUI<TalkUI>("TalkUI").Init(texts, imgs);
    }
    public void talk()
    {
        texts.Add("����Ī�������ǡ������й���ΰ�����ѧ��֮һ���������𣿣�");
        imgs.Add(true);

        texts.Add("���ҵ������ҵ�����ֻ�Ǹ��������ͷ���ˡ�");
        imgs.Add(false);

        texts.Add("�������������ݶ���Σ�");
        imgs.Add(true);

        texts.Add("��������ѧ��Ϭ���ļ��⣬���й���ѧʷ�ϵ�ţ�١�������ʵ��");
        imgs.Add(true);

        texts.Add("ֻ�ǽ�����ǰ�˵��ǻ۰��ˡ�");
        imgs.Add(false);

        texts.Add("ĪҪǫ�鰡��������Ҳ��ǰ�ˣ�������������ѧ���˰���");
        imgs.Add(true);

        texts.Add("�������˻�ϲ����Ů����ô��Ҫ�����ҵ��ǻ���");
        imgs.Add(false);

        texts.Add("��Ȼ����������Ƭ��������֮�أ�����Ϊ��Ѱ������ѧ�İ��ء�");
        imgs.Add(true);

        texts.Add("��ô���ɣ���Ů���ҳ��������ѧ��ִ�š�");
        imgs.Add(false);
    }
}
