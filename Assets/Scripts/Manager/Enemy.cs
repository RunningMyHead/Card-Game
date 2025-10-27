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
    public cardtype data;//����

    //UI����
    public GameObject hpItemObj;//bossѪ��
    public GameObject actionObj;//����ͼ��
    public Text Enemytexing;//boss����

    //UI���
    public Text HitText;//�������ı�
    public Text HPText;//Ѫ���ı���
    public Image HPImg;

    //��ֵ���
    public int CurEnemyHP;//��ǰ����ֵ
    public int MaxEnemyHP;//�������ֵ
    public int EnemyAttcak;//������
    public void Init(cardtype data)
    {
        this.data = data;
    }

    private void Start()
    {
        hpItemObj = UIManager.Instance.CreateHpItem();//���HPѪ������
        actionObj = UIManager.Instance.CreateActionIcon();//��ù���������

        HitText = actionObj.transform.Find("Hitint"). GetComponent<Text>();
        HPText = hpItemObj.transform.Find("HPPlane1/HP/HPText").GetComponent<Text>();
        HPImg = hpItemObj.transform.Find("HPPlane1/HP").GetComponent <Image>();
        Enemytexing = hpItemObj.transform.Find("EnemyTeXing").GetComponent<Text>();
        

        //��ʼ����ֵ
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

    //����Ѫ��
    public void UpdateHP()
    {
        HPText.text = CurEnemyHP + "/" + MaxEnemyHP;
        HPImg.fillAmount = (float)CurEnemyHP/MaxEnemyHP;
   
    }
    //ֻ���ڷ����غ���ʾ�����¹���
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

    //���ع��﹥��ͼ��
    public void falseAttcak()
    {
        actionObj.gameObject.SetActive(false);
    }

    //BOSS����
    public void EnemyHit( int val)
    {
        CurEnemyHP -= val;
        if (CurEnemyHP <= 0)
        {
            if( CurEnemyHP == 0)
            {
                EventManager.Instance.integralint += 12;//������ɱ���12��
                UIManager.Instance.GetUI<WinUI>("WinUI").ShowOnButton7();
            }
            else
            {
                EventManager.Instance.integralint += 7;//��ɱ���7��
                UIManager.Instance.GetUI<WinUI>("WinUI").HideOnbutton7();
            }
            CurEnemyHP = 0;
            //������������
            BossDieAnima();
        }
        else
        {
            //�������˶���
            BossHitAnima(val);
        }

        //ˢ��Ѫ��UI
        UpdateHP();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateintegralCount();
    }

    //ɾ��boss��Ѫ��������������Ϸ����
    public void BossDie()
    {
        Destroy(gameObject.gameObject);
        Destroy(hpItemObj.gameObject);
        Destroy(actionObj.gameObject);
    }

    //����������ͬʱת��Win&UI
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
        .SetEase(Ease.OutQuad) // �����ƶ�����Ϊ����
        .OnComplete(() =>
                {
            // �ƶ���ɺ�����λ�����û�ԭʼλ��
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


    private void OnMouseEnter()//���������ʾ
    {
        // �������д�������¼��Ĵ����߼�
        StartCoroutine(ShowText());
    }

    public void InitText()
    {
        switch (data)
        {
            case cardtype.Sword:
                Enemytexing.text = "˫��Ч��ʧЧ";
                break;
            case cardtype.Shield:
                Enemytexing.text = "����Ч��ʧЧ";
                break;
            case cardtype.Return:
                Enemytexing.text = "ѭ��Ч��ʧЧ";
                break;
            case cardtype.Draw:
                Enemytexing.text = "�鿨Ч��ʧЧ";
                break;
            case cardtype.NPC:
                Enemytexing.text = "������Ī���ǡ���";
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
