using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public card data;
    public bool Cardselect;
    public void Init(card data)
    {
        this.data = data;
        Cardselect = false;
        //RectTransform rectTransform = transform.GetComponent<RectTransform>();
    }
    Vector2 initpos;
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
        if (UIManager.Instance.GetUI<FightUI>("FightUI").Attackbtn.gameObject.activeSelf || UIManager.Instance.GetUI<FightUI>("FightUI").Defensebtn.gameObject.activeSelf)
        {
            initpos = transform.GetComponent<RectTransform>().anchoredPosition;
            if (Cardselect == true)
            {
                //�Ѿ�ѡ����ôȡ��ѡ��
                transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(initpos.x, -320), 0.2f);
                Cardselect = false;
                FightCardManager.Instance.handCardList.Remove(data);//��ѡ�п����Ƴ��ÿ��ƣ�ֻ���Ƴ���һ������ֵ��ȵĿ�
            }
            else
            {
                //��ûѡ����ô�ж��Ƿ����ѡ��
                if (CardAdd() || FightManager.Instance.fightUnit is Fight_EnemyTurn)
                {

                    transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(initpos.x, -260), 0.2f);
                    //FightCardManager.Instance.UPcardList.Add(data);
                    Cardselect = true;
                    FightCardManager.Instance.handCardList.Add(data);//��ѡ�п�����Ӹÿ���
                }
                else
                {
                    //����ѡ��
                    UIManager.Instance.ShowTip("����ѡ��", Color.red, delegate () { });
                }

            }
        }
    }

    //�жϵ�ǰ�����Ƿ����ѡ��
    public bool CardAdd()
    {
        //����ͬ����Ҳ����ͬ��ֵ������С���ܳ���10��
        if (UIManager.Instance.GetUI<FightUI>("FightUI").Cardsamevalueandtype(data))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
