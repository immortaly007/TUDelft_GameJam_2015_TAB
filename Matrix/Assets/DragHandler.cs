using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject currentDragged;
    public static bool dragDisable;
    Vector3 startPosition;
    bool cancelDrag;
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
