using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�������
public class UIBase: MonoBehaviour
{
    //ע���¼�
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }
    //��ʾ
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    //����
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    //�رգ����٣�
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }

    public void RightMoveAnimation()
    {
        // ʹ��DOTween�����ƶ������������������ƶ�ָ������
        transform.DOMoveX(2700, 1)
            .SetEase(Ease.OutQuad); // ʹ�û���Ч�����������
    }
    //�¼��������ƶ�
    public void LeftMoveAnimation()
    {
        // ʹ��DOTween�����ƶ������������������ƶ�ָ������
        transform.DOMoveX(1250, 1)
            .SetEase(Ease.OutQuad); // ʹ�û���Ч�����������
    }
}
