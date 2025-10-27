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

    //unity会自动传递onStarGameBtn所需要的参数
    private void OnButton1(GameObject obj, PointerEventData pDate)
    {
        //重新开始
        //UIManager.Instance.CloseAllUI();
        //Destroy(EventManager.Instance.Player);
        UIManager.Instance.CloseAllUI();
        EnemyManager.Instance.DeleteEnemy();
        FightManager.Instance.ChangeType(FightType.Init);
        //UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        ////播放bgm

    }

    private void OnButton2(GameObject obj, PointerEventData pDate)
    {
        //需要打包成软件才能正常使用
        Application.Quit();
    }
}
