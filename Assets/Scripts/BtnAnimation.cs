using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BtnAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite[] sprites; //[�Ϲݹ�ư��������Ʈ,������ư��������Ʈ]
    public TextMeshProUGUI textMeshPro; //�� ��ư�� �ؽ�Ʈ�޽�����
    

    public void OnPointerDown (PointerEventData downData) //���콺Ŭ���� �����Ҷ� ��������Ʈ �����ϰ� ���ڵ� �Ʒ��� ��������
    {
        gameObject.GetComponent<Image>().sprite = sprites[1]; 
        textMeshPro.verticalAlignment = VerticalAlignmentOptions.Bottom;
    }

    public void OnPointerUp (PointerEventData upData) //���콺Ŭ���� ������ ��������Ʈ �ٽ� �����ϰ� ���� ����ġ
    {
        gameObject.GetComponent<Image>().sprite = sprites[0];
        textMeshPro.verticalAlignment = VerticalAlignmentOptions.Middle;
    }
}
