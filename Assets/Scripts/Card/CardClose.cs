using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClose : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public card data;
    public bool Cardselect;
    Vector2 initpos;
    public void Init(card data)
    {
        this.data = data;
        Cardselect = false;
    }

    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.3f, 0.2f);
    }
 
    //����뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.2f); 
    }
    //�����
    public void OnPointerClick(PointerEventData eventData)
    {
        initpos = transform.GetComponent<RectTransform>().anchoredPosition;
        if (Cardselect)
        {
            transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(initpos.x, initpos.y - 50), 0.2f);
            UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").cardClose.Remove(data);
            Cardselect = false;
        }
        else
        {
            if(UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").cardClose.Count == 1)
            {
                //���������
                UIManager.Instance.ShowTip("����ѡ��", Color.red, delegate () { });
            }
            else
            {
                transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(initpos.x, initpos.y + 50), 0.2f);
                UIManager.Instance.GetUI<CardCloseUI>("CardCloseUI").cardClose.Add(data);
                Cardselect = true;
            }
 
        }
    }


}
