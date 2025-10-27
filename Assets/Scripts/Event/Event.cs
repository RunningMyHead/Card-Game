using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Event : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject GameObject;//��Ϸ���壬���ڿ���֮��Ķ���Ч��
    private Image image;//�洢ͷ����Ե�Χ��
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

    //����
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.black;
        button.gameObject.SetActive(true);
        text.gameObject.SetActive(false);
    }
    //�뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
    }

    //�����ť
    void OnButtonPressed()
    {
        //�����¼�����ֹ��ť������ͬʱ����������
        EventManager.Instance.ChangeType(eventtype.Event);
        //����ID������Ӧ�¼�������¼���ʼ��
        EventManager.Instance.EventDictionary[EventID].Init();//�����¼���ʼ��
        EventManager.Instance.EventDictionary.Remove(EventID);//���ֵ����Ƴ����¼�

        CloseEvnetObj();
    }

    //�ݻ��¼�ͼ��
    public void CloseEvnetObj()
    {
        // ������С��ʧ����
        EventAnimation();  
    }

    //�ݻ��¼�ͼ�궯��
    public void EventAnimation()
    {
        // �������������ǰ���ǳ���ʱ�䣬������ǿ��
        transform.DOShakePosition(1,100)
            .SetLoops(-1, LoopType.Restart) // ѭ�����Ŷ�������
            .SetEase(Ease.InOutQuad); // ʹ�û���Ч��

        // ʹ��DOTween������С��������������СΪ0
        transform.DOScale(Vector3.zero, 1)
            .SetEase(Ease.OutQuart) // ʹ�û���Ч��
            .OnComplete(() =>
            {
                // ������ɺ���������
                UIManager.Instance.GetUI<EventUI>("EventUI").CloseEventID(EventID);
                Destroy(gameObject);

            });
    }
}
