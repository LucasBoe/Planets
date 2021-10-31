using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonFeedbackBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] SpriteRenderer spriteRenderer;


    Slider slider;
    Button button;
    Toggle toggle;

    Graphic targetGraphic;
    bool IsUiElement => button || slider || toggle;

    bool isHovered = false;
    bool pointerIsDown = false;

    float scale = 1f;

    private void PointerDown()
    {
        SoundHandler.Play(BaseSounds.UIClick);
        pointerIsDown = true;
    }

    private void PointerUp()
    {
        pointerIsDown = false;
    }
    private void PointerEnter()
    {
        isHovered = true;
    }

    private void PointerExit()
    {
        isHovered = false;
    }

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float speed = Time.deltaTime / 2;

        if (pointerIsDown)
            scale = Mathf.MoveTowards(scale, 0.9f, speed*4);
        else if (isHovered)
            scale = Mathf.MoveTowards(scale, 1.15f + (0.025f) * Mathf.Sin(Time.time * 6), speed);
        else
            scale = Mathf.MoveTowards(scale, 1, speed*2);

        (targetGraphic ? targetGraphic.transform : (spriteRenderer ? spriteRenderer.transform : transform)).localScale = Vector3.one * scale;
    }

    private void Start()
    {
        InWorldHandle handle = GetComponent<InWorldHandle>();

        if (handle)
        {
            handle.OnPointerDown += PointerDown;
            handle.OnPointerUp += PointerUp;
            handle.OnPointerEnter += PointerEnter;
            handle.OnPointerExit += PointerExit;
        }

        button = GetComponent<Button>();
        slider = GetComponent<Slider>();
        toggle = GetComponent<Toggle>();

        if (button) targetGraphic = button.targetGraphic;
        if (slider) targetGraphic = slider.targetGraphic;
        if (toggle) targetGraphic = toggle.targetGraphic;
    }
    public void OnPointerDown(PointerEventData eventData) { if (IsUiElement) PointerDown(); }
    public void OnPointerUp(PointerEventData eventData) { if (IsUiElement) PointerUp(); }
    public void OnPointerEnter(PointerEventData eventData) { if (IsUiElement) PointerEnter(); }
    public void OnPointerExit(PointerEventData eventData) { if (IsUiElement) PointerExit(); }
}
