using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Event : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject GameObject;//游戏物体，用于控制之后的动画效果
    private Image image;//存储头像框旁的围栏
    public  Button button;
    public  string EventID;
    private Text text;

    private void Awake()
    {
        GameObject = gameObject;
        image = transform.Find("Image").GetComponent<Image>();
        button = transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
        text = transform.Find("Text").GetComponent<Text>();
    }

    //进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.black;
        button.gameObject.SetActive(true);
        text.gameObject.SetActive(false);
    }
    //离开
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
    }

    //点击按钮
    void OnButtonPressed()
    {
        //进入事件，禁止按钮触发，同时任务栏右移
        EventManager.Instance.ChangeType(eventtype.Event);
        //根究ID搜索对应事件类完成事件初始化
        EventManager.Instance.EventDictionary[EventID].Init();//调用事件初始化
        EventManager.Instance.EventDictionary.Remove(EventID);//在字典内移除该事件

        CloseEvnetObj();
    }

    //摧毁事件图标
    public void CloseEvnetObj()
    {
        // 播放缩小消失动画
        EventAnimation();  
    }

    //摧毁事件图标动画
    public void EventAnimation()
    {
        // 随机抖动动画，前面是持续时间，后面是强度
        transform.DOShakePosition(1,100)
            .SetLoops(-1, LoopType.Restart) // 循环播放抖动动画
            .SetEase(Ease.InOutQuad); // 使用缓动效果

        // 使用DOTween创建缩小动画，将物体缩小为0
        transform.DOScale(Vector3.zero, 1)
            .SetEase(Ease.OutQuart) // 使用缓动效果
            .OnComplete(() =>
            {
                // 动画完成后销毁物体
                UIManager.Instance.GetUI<EventUI>("EventUI").CloseEventID(EventID);
                Destroy(gameObject);

            });
    }
}
