using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndUI : UIBase
{
    private Text integral;
    private Text time;
    public void Awake()
    {
        integral = transform.Find("EndUIPlane/integral/Text").GetComponent<Text>();
        time = transform.Find("EndUIPlane/time/Text").GetComponent<Text>();
        Register("EndUIPlane/Btn").onClick = OnBtn;
        UIManager.Instance.GetUI<FightUI>("FightUI").StopTimer();
    }

    private void Start()
    {
        //if (!Application.isPlaying)
        integral.text = UIManager.Instance.GetUI<FightUI>("FightUI").integral.text;
        time.text = UIManager.Instance.GetUI<FightUI>("FightUI").time.text;
    }

    private void OnBtn(GameObject obj, PointerEventData pDate)
    {
        UIManager.Instance.CloseAllUI();
        Destroy(EventManager.Instance.Player);
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        //²¥·Åbgm
        AudioManager.Instance.PlayBGM("startBGM");
    }
}
