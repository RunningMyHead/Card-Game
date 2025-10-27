using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LossUI : UIBase
{
    private Text integral;
    private Text time;
    private void Awake()
    {
        integral = transform.Find("LossUIPlane/integral/Text").GetComponent<Text>();
        time = transform.Find("LossUIPlane/time/Text").GetComponent<Text>();
        Register("Btn1").onClick = OnButton1;
        Register("Btn2").onClick = OnButton2;
        UIManager.Instance.GetUI<FightUI>("FightUI").StopTimer();
    }

    private void Start()
    {
        integral.text = UIManager.Instance.GetUI<FightUI>("FightUI").integral.text;
        time.text = UIManager.Instance.GetUI<FightUI>("FightUI").time.text;
    }

    //unity���Զ�����onStarGameBtn����Ҫ�Ĳ���
    private void OnButton1(GameObject obj, PointerEventData pDate)
    {
        //���¿�ʼ
        //UIManager.Instance.CloseAllUI();
        //Destroy(EventManager.Instance.Player);
        UIManager.Instance.CloseAllUI();
        EnemyManager.Instance.DeleteEnemy();
        FightManager.Instance.ChangeType(FightType.Init);
        //UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        ////����bgm

    }

    private void OnButton2(GameObject obj, PointerEventData pDate)
    {
        //��Ҫ����������������ʹ��
        Application.Quit();
    }
}
