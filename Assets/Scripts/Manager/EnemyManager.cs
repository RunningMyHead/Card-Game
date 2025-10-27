using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class EnemyManager
{
    public static EnemyManager Instance  = new EnemyManager();

    public  List<Enemy> enemyList = new List<Enemy>();//存储序列的敌人，只存两个
    //人家不是null，是长度为0的列表，
    //使用Clear()之后也只是将列表换为空列表不是null

    //private Enemy Enemy;//存储战斗中的敌人
    //    Sword,//攻击 0
    //    Shield,//防御 1
    //    Return,//循环 2 
    //    Draw,//抽卡 3
    //    temp,//空 4
    private cardtype enemyBoss;
    //用于创建一个新的boss类别
    //public void LoadRes1()
    //{
    //    enemyBoss = Setenemytype();
    //    if(enemyList.Count != 0)
    //    {
    //        //enemylist里面有其他boss
    //        while (enemyList[0].data == enemyBoss)
    //        {
    //            enemyBoss = Setenemytype();
    //        }
    //        return;
    //    }
    //}

    ////用于根据boss类别生成boos实体，添加至enemyList
    //public void LoadRes2()
    //{
    //    //设置立绘
    //    GameObject Boss = Object.Instantiate(Resources.Load("playeandboss/" + enemyBoss) as GameObject);
    //    //GameObject Boss = Object.Instantiate(Resources.Load("playeandboss/" + cardtype.Sword) as GameObject);
    //    Enemy enemy = Boss.AddComponent<Enemy>();//添加预制敌人脚本
    //    AudioManager.Instance.PlayBGM(enemyBoss.ToString());//播放与对应boss的bgm

    //    enemy.Init(enemyBoss);
    //    //存储到集合
    //    enemyList.Add(enemy);
    //}
    public void LoadRes(cardtype boss)
    {
        GameObject Boss = Object.Instantiate(Resources.Load("playeandboss/" + boss) as GameObject);
        Enemy enemy = Boss.AddComponent<Enemy>();//添加预制敌人脚本
        AudioManager.Instance.PlayBGM(enemyBoss.ToString());//播放与对应boss的bgm
        enemy.Init(boss);
        //存储到集合
        enemyList.Add(enemy);
    }

    //设置boss种类
    //public cardtype Setenemytype()
    //{
    //    int ran = Random.Range(0, 4);
    //    return (cardtype)ran;
    //}

    //设置boss攻击力，同时开启攻击图标
    public void Setenemyattack()
    {
        enemyList[0].UpdateAttcak();
    }

    //关闭攻击力图标
    public void Setfalsenemyattack()
    {
        enemyList[0].falseAttcak();
    }

    //移除当前敌人
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
    
    //boss受伤
    public void EnemyHit( int cardValue)
    {
        enemyList[0].EnemyHit(cardValue);
    }

    //boss攻击
    public void EnemyAttack()
    {
        enemyList[0].BossAttatcAnima();
        EventManager.Instance.GetPlayerHit(enemyList[0].EnemyAttcak);
    }

    //获得boss的生命值
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
            .SetEase(Ease.OutQuad) // 设置移动曲线为线性
            .OnComplete(() =>
            {
                // 移动完成后将物体位置设置回原始位置
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
