using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_MainBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite _on;
    private Sprite _off;
    private Image _image;

    private void Start()
    {
        _on = Resources.Load<Sprite>("Sprites/1_BTN_enabled");
        _off = Resources.Load<Sprite>("Sprites/1_BTN_disabled");
        _image = transform.GetComponent<Image>();
        _image.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _on;
        Value value = new();
        value.Value1 = "600";
        value.Value2 = $"{563}, {transform.localPosition.y}";
        EffectManager.Instance.PlayEffect(typeof(MoveBySpeedLocal), gameObject, value);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _off;
        Value value = new();
        value.Value1 = "600";
        value.Value2 = $"{691.6f}, {transform.localPosition.y}";
        EffectManager.Instance.PlayEffect(typeof(MoveBySpeedLocal), gameObject, value);
    }
}
