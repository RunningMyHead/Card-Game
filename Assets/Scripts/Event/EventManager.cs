using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eventtype
{
    None,
    lnit,//��ʼ��
    NotEvent,
    Event,
    Die,
    End,
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Dictionary<string, EventClass> EventDictionary;//ǰ�����¼�ID���������¼�

    public GameObject Player;
    public GameObject PlayerNotFight;
    public GameObject PlayerFight;
    public int MaxHP;
    public int CurHP;
    public int DefenseCount;

    public int MaxCardSize;//�������
    public int integralint;//��ҷ���
    public int money;//���
    public int Maxpopotion;//���ҩˮƿ


    public EventUnit eventUnit;//�¼���Ԫ
    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        EventDictionary = new Dictionary<string, EventClass>();
        MaxHP = 20;
        CurHP = 20;
        DefenseCount = 0;
        MaxCardSize = 15;//���15�ţ�ʵ��ֻ��14��
        Player = Instantiate(Resources.Load("playeandboss/Player") as GameObject);
        PlayerNotFight = Player.transform.Find("notFight").gameObject;
        PlayerFight = Player.transform.Find("Fight").gameObject;
        integralint = 0;
        money = 0;
        Maxpopotion = 0;

        //��ʼ���¼�
        EventDictionary.Add("Enemy_Sword", new Enemy_Sword());
        EventDictionary.Add("Enemy_Return", new Enemy_Return());
        EventDictionary.Add("Enemy_Draw", new Enemy_Draw());
        EventDictionary.Add("Enemy_Shield", new Enemy_Shield());
    }

    public void ChangeType(eventtype eventtype)
    {
        switch(eventtype)
        {
            case eventtype.None:
                break;
            case eventtype.lnit:
                eventUnit = new Event_Init();
                break;
            case eventtype.NotEvent:
                eventUnit = new Event_NotEvent();
                break;
            case eventtype.Event:
                eventUnit = new Event_Event();
                break;
            case eventtype.Die:
                eventUnit = new Event_Die();
                break;
            case eventtype.End:
                eventUnit = new Event_End();
                break;   
        }
        eventUnit.Init();
        
    }

    
    public void ToPlayerFight()//���������ս����̬
    {
        PlayerNotFight.gameObject.SetActive(false);
        PlayerFight.gameObject.SetActive(true);
    }
   
    public void ToPlayetNotFight() //��������ɷ�ս����̬
    {
        PlayerNotFight.gameObject.SetActive(true);
        PlayerFight.gameObject.SetActive(false);
    }
    
    public void PlayerAttactAnimo()//��ҹ���������ʱ0.4��
    {
        Player.transform.DOMove(new Vector2(-7, -3.5f), 0.3f)
        .SetEase(Ease.OutQuad) // �����ƶ�����Ϊ����
        .OnComplete(() =>
        {
            // �ƶ���ɺ�����λ�����û�ԭʼλ��
            Player.transform.DOMove(new Vector2(-5, -3.5f), 0.1f)
            .SetEase(Ease.InBack);
        });
    }
    
    public void PlayerDieAnimo()//����������� ��ʱ2.5��
    {
        Player.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
        {
            ToPlayetNotFight();
            Player.transform.DORotate(new Vector3(0, 0, 60), 2f, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear);
            Player.transform.DOMove(Player.transform.position + new Vector3(-0.5f, -1, 0), 2f).SetRelative().SetEase(Ease.Linear);
        });
    }
    
    public void PlyaerHitAnimo()//����ܻ����� ��ʱ0.9��
    {
        ToPlayetNotFight();
        // ʹ�� DOTween ��������˸Ч��
        Sequence rotationSequence = DOTween.Sequence();
        rotationSequence.Append(Player.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f))
            .SetEase(Ease.Linear).SetLoops(8, LoopType.Restart);
        Sequence blinkSequence = DOTween.Sequence();
        blinkSequence.Append(PlayerNotFight.GetComponent<Renderer>().material.DOColor(Color.red, 0.1f)); // �л���ɫΪ��˸��ɫ
        blinkSequence.Append(PlayerNotFight.GetComponent<Renderer>().material.DOColor(Color.white, 0.1f)); // �ָ�ԭʼ��ɫ
        blinkSequence.SetLoops(4);
        blinkSequence.OnComplete(() =>
        {
            // ��˸��ɺ�����Ƕ����û���ȷ�ĳ���
            ToPlayerFight();
            Player.transform.DORotate(new Vector3(0f, 0f, 0f), 0.1f).SetEase(Ease.OutSine);
        });
        // ������������
        //PlayertonotFight();
        rotationSequence.Play();
        blinkSequence.Play();
    }
    public void GetPlayerHit(int Hit)//����
    {
        //�����Ļ���
        if (DefenseCount >= Hit)
        {
            DefenseCount -= Hit;
            if (DefenseCount == 0)
            {
                integralint += 5;//������5��
            }
            else
            {
                integralint += DefenseCount;
            }
        }
        else
        {
            Hit = Hit - DefenseCount;

            DefenseCount = 0;
            CurHP -= Hit;

            if (CurHP <= 0)
            {
                CurHP = 0;
                PlayerDieAnimo();
            }
            else
            {
                PlyaerHitAnimo();
                Camera.main.DOShakePosition(1f, 0.2f, 10, 45);
            }

        }
        //���½���
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateintegralCount();
    }
}
