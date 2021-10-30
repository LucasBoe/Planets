using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonFeedbackBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] SpriteRenderer spriteRenderer;

    Button button;
    bool IsUiButton => button;

    bool isHovered = false;
    bool pointerIsDown = false;

    float scale = 1f;

    private void PointerDown()
    {
        pointerIsDown = true;
    }

    private void PointerUp()
    {
        pointerIsDown = false;
    }
    private void PointerEnter()
    {
        SoundHandler.Play(BaseSounds.UIClick);
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

        (spriteRenderer ? spriteRenderer.transform : transform).localScale = Vector3.one * scale;
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
    }
    public void OnPointerDown(PointerEventData eventData) { if (IsUiButton) PointerDown(); }
    public void OnPointerUp(PointerEventData eventData) { if (IsUiButton) PointerUp(); }
    public void OnPointerEnter(PointerEventData eventData) { if (IsUiButton) PointerEnter(); }
    public void OnPointerExit(PointerEventData eventData) { if (IsUiButton) PointerExit(); }
}
