using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SwitchMessage : MonoBehaviour
{
    // Start is called before the first frame update
    Image image_1;
    Image image_2;
    GameObject btn_1;
    GameObject btn_2;
    public bool ButtonIsPressed = false;
    GameObject anchor_obj;
    Vector3 anchor_pos;
    Vector2 localPos;
    Vector2 originalPos;
    Vector2 originalSize;


    void Start()
    {
        DOTween.Init();
        image_1 = this.transform.Find("Image_1").gameObject.GetComponent<Image>();
        image_2 = this.transform.Find("Image_2").gameObject.GetComponent<Image>();
        GameObject btnRoot = this.transform.Find("btnRoot").gameObject;
        btn_1 = btnRoot.transform.Find("Button").gameObject;
        btn_2 = btnRoot.transform.Find("Button_Pressed").gameObject;

        anchor_obj = this.transform.Find("AnchorPos").gameObject;
        anchor_pos = anchor_obj.transform.position;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(anchor_obj.transform.position);

        // ����Ļ����ת��Ϊ�� Canvas Ϊ��׼������
        //localPos = anchor_obj.GetComponent<RectTransform>().position;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    image_2.canvas.GetComponent<RectTransform>(),   // Canvas �� RectTransform
        //    screenPos,
        //    image_2.canvas.worldCamera,     // Canvas ʹ�õ������
        //    out localPos                        // ����ľֲ�����
        //);

        originalSize = image_2.rectTransform.rect.size;
       originalPos = image_2.rectTransform.anchoredPosition;
    }

    // �����ﶨ�������ڰ�������ʱִ�еĺ���
    void OnKeyDown()
    {
        Debug.Log("Key is pressed down.");
        btn_1.SetActive(false);
        //age_2.rectTransform.DOAnchorPos(new Vector2(302, 589), 5);
        image_2.rectTransform.pivot = new Vector2(1, 0);
        // ��С�� 0
        image_2.rectTransform.DOScale(Vector3.zero, 5);
        // ���� anchoredPosition �Ա������½ǵ�λ�ò���
        image_2.rectTransform.anchoredPosition = originalPos + new Vector2(originalSize.x, -originalSize.y)/2.0f;

        // ���ŵ� 0
        image_2.rectTransform.DOScale(Vector3.zero, 5);

        // ��ѡ��ͬʱ����͸���ȵ� 0
        image_2.DOFade(0, 5);

    }

    // �����ﶨ�������ڰ����ͷ�ʱִ�еĺ���
    void OnKeyUp()
    {
        Debug.Log("Key is released.");
        btn_1.SetActive(true);
        // �������������ִ�еĴ���
    }

    // Update is called once per frame
    void Update()
    {
        // ����κΰ����İ����¼�
        if (Input.anyKeyDown)
        {
            OnKeyDown();
            ButtonIsPressed = true;
        }

        // ����κΰ������ͷ��¼�
        if (Input.anyKey == false && ButtonIsPressed)
        {
            OnKeyUp();
            ButtonIsPressed = false;
        }
    }

}
