//-------------------------------------------------------------------------------------------------
// @file	UI_EventHandler.cs
//
// @brief	UI_Event 사용을 위한 Handler 
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
    /// <summary> Click Action 바인드용 </summary>
    public Action<PointerEventData> OnClickHandler = null;
    /// <summary> Drag Action 바인드용 </summary>
    public Action<PointerEventData> OnDragHandler = null;
    /// <summary> Mouse Down Action 바인드용 </summary>
    public Action<PointerEventData> OnDownHandler = null;
    /// <summary> Mouse Down Action 바인드용 </summary>
    public Action<PointerEventData> OnUpHandler = null;

    /// <summary>
    /// Drag 입력 받을 시 바인드 되어있는 Action 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        OnDragHandler?.Invoke(eventData);
    }

    /// <summary>
    /// Click 입력 받을 시 바인드 되어있는 Action 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData);
    }

    /// <summary>
    /// Mouse Down 입력 받을 시 바인드 되어있는 Action 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDownHandler?.Invoke(eventData);
    }

    /// <summary>
    /// Mouse Up 입력 받을 시 바인드 되어있는 Action 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        OnUpHandler?.Invoke(eventData);
    }
}
