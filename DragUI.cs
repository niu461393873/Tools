using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // 按下时UI的坐标(以下都是在本地坐标进行)
    private Vector3 uiPosition;
    // 按下点的坐标
    private Vector2 pointPosition;

    private RectTransform parentRectTransform;

    void Awake()
    {
        parentRectTransform = transform.parent as RectTransform;
    }

    public void OnDrag(PointerEventData data)
    {
        if (Input.mousePosition.x >= 10 && Input.mousePosition.x <= 1910 && Input.mousePosition.y >= 10 & Input.mousePosition.y <= 1070)
        {
            if (this.transform.position.x >= 0)
                if (parentRectTransform == null)
                    return;
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, data.position, data.pressEventCamera, out localPointerPosition))
            {
                //移动的距离
                Vector3 offsetToOriginal = localPointerPosition - pointPosition;

                transform.localPosition = uiPosition + offsetToOriginal;
            }
        }
       
    }

    public void OnPointerDown(PointerEventData data)
    {
        uiPosition = transform.localPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, data.position, data.pressEventCamera, out pointPosition);

    }
}