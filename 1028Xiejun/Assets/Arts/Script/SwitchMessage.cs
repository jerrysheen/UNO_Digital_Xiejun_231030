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
    Vector2 anchor_pos;
    Vector2 localPos;
    Vector2 originalPos;
    Vector2 originalSize;
    Vector2 shrinkDiff;

    public float closeTime = .5f;
    void Start()
    {
        DOTween.Init();
        image_1 = this.transform.Find("Image_1").gameObject.GetComponent<Image>();
        image_2 = this.transform.Find("Image_2").gameObject.GetComponent<Image>();
        GameObject btnRoot = this.transform.Find("btnRoot").gameObject;
        btn_1 = btnRoot.transform.Find("Button").gameObject;
        btn_2 = btnRoot.transform.Find("Button_Pressed").gameObject;

        anchor_obj = this.transform.Find("AnchorPos").gameObject;
        var temp = anchor_obj.GetComponent<RectTransform>().transform.localPosition;
        anchor_pos = new Vector2(temp.x, temp.y);
        
        Vector2 screenPos = Camera.main.WorldToScreenPoint(anchor_obj.transform.position);

        // 将屏幕坐标转换为以 Canvas 为基准的坐标
        //localPos = anchor_obj.GetComponent<RectTransform>().position;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    image_2.canvas.GetComponent<RectTransform>(),   // Canvas 的 RectTransform
        //    screenPos,
        //    image_2.canvas.worldCamera,     // Canvas 使用的摄像机
        //    out localPos                        // 输出的局部坐标
        //);

        originalSize = image_2.rectTransform.rect.size;
        shrinkDiff = anchor_pos - image_2.rectTransform.anchoredPosition;
        Debug.Log(anchor_pos);
        shrinkDiff.x = Mathf.Abs(shrinkDiff.x);
        shrinkDiff.y = Mathf.Abs(shrinkDiff.y);
        originalPos = image_2.rectTransform.anchoredPosition;
    }

    // 在这里定义你想在按键按下时执行的函数
    void OnKeyDown()
    {
        switch (image_2.transform.GetSiblingIndex())
        {
            case 0:
                SetOneImageToBackGround(image_1);
                break;
            case 1:
                SetOneImageToBackGround(image_2);
                break;
            default:
                break;
        }
    }

    void SetOneImageToBackGround(Image image_2) 
    {
        Debug.Log("Key is pressed down.");
        btn_1.SetActive(false);
        image_2.rectTransform.pivot = new Vector2(1, 0);
        image_2.rectTransform.anchoredPosition = originalPos + new Vector2(shrinkDiff.x, -shrinkDiff.y);

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(image_2.rectTransform.DOScale(Vector3.zero, closeTime));
        mySequence.Append(image_2.DOFade(0, 0.0f));
        mySequence.AppendCallback(() =>
        {

            int index = image_2.transform.GetSiblingIndex();
            index = (int)Mathf.Max(index, 1);
            image_2.transform.SetSiblingIndex(index - 1);

        });
        image_2.rectTransform.anchoredPosition = originalPos + new Vector2(originalSize.x, -originalSize.y) / 2.0f;
        mySequence.Append(image_2.rectTransform.DOScale(1, closeTime));
        mySequence.Append(image_2.DOFade(1, 0.0f));
        //mySequence.Append(myTransform.DORotate(new Vector3(0, 180, 0), 1));

        // 开始播放 Sequence
        mySequence.Play();
    }

    // 在这里定义你想在按键释放时执行的函数
    void OnKeyUp()
    {
        Debug.Log("Key is released.");
        btn_1.SetActive(true);
        // 在这里添加你想执行的代码
    }

    // Update is called once per frame
    void Update()
    {
        // 检测任何按键的按下事件
        if (Input.anyKeyDown)
        {
            OnKeyDown();
            ButtonIsPressed = true;
        }

        // 检测任何按键的释放事件
        if (Input.anyKey == false && ButtonIsPressed)
        {
            OnKeyUp();
            ButtonIsPressed = false;
        }
    }

}
