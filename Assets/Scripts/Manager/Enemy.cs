using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using static Unity.Burst.Intrinsics.X86;
//using UnityEngine.UIElements;
//using static System.Net.Mime.MediaTypeNames;
public class Enemy : MonoBehaviour
{
    public cardtype data;//种类

    //UI物体
    public GameObject hpItemObj;//boss血量
    public GameObject actionObj;//攻击图标
    public Text Enemytexing;//boss特性

    //UI相关
    public Text HitText;//攻击力文本
    public Text HPText;//血量文本就
    public Image HPImg;

    //数值相关
    public int CurEnemyHP;//当前生命值
    public int MaxEnemyHP;//最大生命值
    public int EnemyAttcak;//攻击力
    public void Init(cardtype data)
    {
        this.data = data;
    }

    private void Start()
    {
        hpItemObj = UIManager.Instance.CreateHpItem();//获得HP血条物体
        actionObj = UIManager.Instance.CreateActionIcon();//获得攻击力物体

        HitText = actionObj.transform.Find("Hitint"). GetComponent<Text>();
        HPText = hpItemObj.transform.Find("HPPlane1/HP/HPText").GetComponent<Text>();
        HPImg = hpItemObj.transform.Find("HPPlane1/HP").GetComponent <Image>();
        Enemytexing = hpItemObj.transform.Find("EnemyTeXing").GetComponent<Text>();
        

        //初始化数值
        CurEnemyHP = 40;
        if (data == cardtype.NPC)
        {
            CurEnemyHP = 60;
        }
        MaxEnemyHP = CurEnemyHP;
        EnemyAttcak = new int();
        UpdateHP();
        InitText();
    }

    //更新血量
    public void UpdateHP()
    {
        HPText.text = CurEnemyHP + "/" + MaxEnemyHP;
        HPImg.fillAmount = (float)CurEnemyHP/MaxEnemyHP;
   
    }
    //只会在防御回合显示，更新攻击
    public void UpdateAttcak()
    {
        EnemyAttcak = Random.Range(10, 16);
        if (data == cardtype.NPC)
        {
            EnemyAttcak = Random.Range(13, 19);
        }
        HitText.text = EnemyAttcak.ToString();
        actionObj.gameObject.SetActive(true);
    }

    //隐藏怪物攻击图标
    public void falseAttcak()
    {
        actionObj.gameObject.SetActive(false);
    }

    //BOSS受伤
    public void EnemyHit( int val)
    {
        CurEnemyHP -= val;
        if (CurEnemyHP <= 0)
        {
            if( CurEnemyHP == 0)
            {
                EventManager.Instance.integralint += 12;//完美击杀获得12分
                UIManager.Instance.GetUI<WinUI>("WinUI").ShowOnButton7();
            }
            else
            {
                EventManager.Instance.integralint += 7;//击杀获得7分
                UIManager.Instance.GetUI<WinUI>("WinUI").HideOnbutton7();
            }
            CurEnemyHP = 0;
            //播放死亡动画
            BossDieAnima();
        }
        else
        {
            //播放受伤动画
            BossHitAnima(val);
        }

        //刷新血量UI
        UpdateHP();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateintegralCount();
    }

    //删除boss，血条，攻击三个游戏物体
    public void BossDie()
    {
        Destroy(gameObject.gameObject);
        Destroy(hpItemObj.gameObject);
        Destroy(actionObj.gameObject);
    }

    //死亡动画，同时转到Win&UI
    public void BossDieAnima()
    {
        gameObject.transform.DOMove(new Vector2(6, -5), 1);
        gameObject.transform.DORotate(new Vector3(0, 0, -60), 0.75f).OnComplete(() => {
            BossDie();
            EnemyManager.Instance.DeleteEnemy();
            FightManager.Instance.ChangeType(FightType.Win);
        });
    }

    public void BossAttatcAnima()
    {
        float x = gameObject.transform.position.x;
            gameObject.transform.DOMoveX( 0 , 0.3f)
        .SetEase(Ease.OutQuad) // 设置移动曲线为线性
        .OnComplete(() =>
                {
            // 移动完成后将物体位置设置回原始位置
            gameObject.transform.DOMoveX( x , 0.1f)
         .SetEase(Ease.InBack);
            });
    }

    public void BossHitAnima(int avl)
    {
        gameObject.transform.DOShakePosition(2f, new Vector2(0.5f, 0), avl, 1).OnComplete(() => {
            //FightManager.Instance.ChangeType(FightType.Enemy);
            FightManager.Instance.ChangeType(FightType.Enemy);
        }); ;
    }


    private void OnMouseEnter()//给出玩家提示
    {
        // 在这里编写鼠标进入事件的处理逻辑
        StartCoroutine(ShowText());
    }

    public void InitText()
    {
        switch (data)
        {
            case cardtype.Sword:
                Enemytexing.text = "双剑效果失效";
                break;
            case cardtype.Shield:
                Enemytexing.text = "盾牌效果失效";
                break;
            case cardtype.Return:
                Enemytexing.text = "循环效果失效";
                break;
            case cardtype.Draw:
                Enemytexing.text = "抽卡效果失效";
                break;
            case cardtype.NPC:
                Enemytexing.text = "老先生莫非是……";
                break;
        }
    }

    public IEnumerator ShowText()
    {
        Enemytexing.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Enemytexing.gameObject.SetActive(false);
    }
}
