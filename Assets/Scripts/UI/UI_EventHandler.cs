//-------------------------------------------------------------------------------------------------
// @file	UI_EventHandler.cs
//
// @brief	UI_Event ����� ���� Handler 
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary> Click Action ���ε�� </summary>
    public Action<PointerEventData> OnClickHandler = null;
    /// <summary> Drag Action ���ε�� </summary>
    public Action<PointerEventData> OnDragHandler = null;
    /// <summary> Mouse Down Action ���ε�� </summary>
    public Action<PointerEventData> OnDownHandler = null;
    /// <summary> Mouse Down Action ���ε�� </summary>
    public Action<PointerEventData> OnUpHandler = null;

    /// <summary>
    /// Drag �Է� ���� �� ���ε� �Ǿ��ִ� Action ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        OnDragHandler?.Invoke(eventData);
    }

    /// <summary>
    /// Click �Է� ���� �� ���ε� �Ǿ��ִ� Action ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData);
    }

    /// <summary>
    /// Mouse Down �Է� ���� �� ���ε� �Ǿ��ִ� Action ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDownHandler?.Invoke(eventData);
    }

    /// <summary>
    /// Mouse Up �Է� ���� �� ���ε� �Ǿ��ִ� Action ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        OnUpHandler?.Invoke(eventData);
    }
}
