using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CatchHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject currentDragged = DragHandler.currentDragged;
        MatrixPlatformManager.instance.Catch(gameObject, currentDragged);
    }
}
