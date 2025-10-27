using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class EnemyManager
{
    public static EnemyManager Instance  = new EnemyManager();

    public  List<Enemy> enemyList = new List<Enemy>();//�洢���еĵ��ˣ�ֻ������
    //�˼Ҳ���null���ǳ���Ϊ0���б�
    //ʹ��Clear()֮��Ҳֻ�ǽ��б�Ϊ���б���null

    //private Enemy Enemy;//�洢ս���еĵ���
    //    Sword,//���� 0
    //    Shield,//���� 1
    //    Return,//ѭ�� 2 
    //    Draw,//�鿨 3
    //    temp,//�� 4
    private cardtype enemyBoss;
    //���ڴ���һ���µ�boss���
    //public void LoadRes1()
    //{
    //    enemyBoss = Setenemytype();
    //    if(enemyList.Count != 0)
    //    {
    //        //enemylist����������boss
    //        while (enemyList[0].data == enemyBoss)
    //        {
    //            enemyBoss = Setenemytype();
    //        }
    //        return;
    //    }
    //}

    ////���ڸ���boss�������boosʵ�壬�����enemyList
    //public void LoadRes2()
    //{
    //    //��������
    //    GameObject Boss = Object.Instantiate(Resources.Load("playeandboss/" + enemyBoss) as GameObject);
    //    //GameObject Boss = Object.Instantiate(Resources.Load("playeandboss/" + cardtype.Sword) as GameObject);
    //    Enemy enemy = Boss.AddComponent<Enemy>();//���Ԥ�Ƶ��˽ű�
    //    AudioManager.Instance.PlayBGM(enemyBoss.ToString());//�������Ӧboss��bgm

    //    enemy.Init(enemyBoss);
    //    //�洢������
    //    enemyList.Add(enemy);
    //}
    public void LoadRes(cardtype boss)
    {
        GameObject Boss = Object.Instantiate(Resources.Load("playeandboss/" + boss) as GameObject);
        Enemy enemy = Boss.AddComponent<Enemy>();//���Ԥ�Ƶ��˽ű�
        AudioManager.Instance.PlayBGM(enemyBoss.ToString());//�������Ӧboss��bgm
        enemy.Init(boss);
        //�洢������
        enemyList.Add(enemy);
    }

    //����boss����
    //public cardtype Setenemytype()
    //{
    //    int ran = Random.Range(0, 4);
    //    return (cardtype)ran;
    //}

    //����boss��������ͬʱ��������ͼ��
    public void Setenemyattack()
    {
        enemyList[0].UpdateAttcak();
    }

    //�رչ�����ͼ��
    public void Setfalsenemyattack()
    {
        enemyList[0].falseAttcak();
    }

    //�Ƴ���ǰ����
    public void DeleteEnemy()
    {
        enemyList[0].BossDie();
        if(UIManager.Instance.GetUI<WinUI>("WinUI") != null)
        {
            UIManager.Instance.GetUI<WinUI>("WinUI").bosstype = enemyList[0].data;
        }

        enemyList.Remove(enemyList[0]);
        return;
    }
    
    //boss����
    public void EnemyHit( int cardValue)
    {
        enemyList[0].EnemyHit(cardValue);
    }

    //boss����
    public void EnemyAttack()
    {
        enemyList[0].BossAttatcAnima();
        EventManager.Instance.GetPlayerHit(enemyList[0].EnemyAttcak);
    }

    //���boss������ֵ
    public int getEnemyHP()
    {
       return enemyList[0].CurEnemyHP;
    }

    public cardtype GetBOSSdate()
    {
        return enemyList[0].data;
    }

    public void EnemyAttackAnimo()
    {
        Vector2 vector2 = enemyList[0].transform.position;
        enemyList[0].transform.DOMove(new Vector2(0, vector2.y), 0.3f)
            .SetEase(Ease.OutQuad) // �����ƶ�����Ϊ����
            .OnComplete(() =>
            {
                // �ƶ���ɺ�����λ�����û�ԭʼλ��
                enemyList[0].transform.DOMove(vector2, 0.1f)
            .SetEase(Ease.InBack);
            });
    }

    public IEnumerator DoAttack()
    {
        EnemyAttackAnimo();
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        if (EventManager.Instance.CurHP == 0)
        {
            FightManager.Instance.ChangeType(FightType.Loss);
        }
        else
        {
            FightManager.Instance.ChangeType(FightType.Player);
            //Fight_Fight.Instance.ChangeType(Fightenum.palyer);
        }


    }
}
