using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject currentDragged;
    Vector3 startPosition;
    //Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentDragged = gameObject;
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentDragged = null;
        transform.position = startPosition;
    }
}
