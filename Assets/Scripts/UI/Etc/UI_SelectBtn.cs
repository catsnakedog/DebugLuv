using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SelectBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite _on;
    private Sprite _off;
    private Image _image;

    private void Start()
    {
        _on = Resources.Load<Sprite>("Sprites/2_selectbox_enabled");
        _off = Resources.Load<Sprite>("Sprites/2_selectbox_disabled");
        _image = transform.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _on;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _off;
    }
}