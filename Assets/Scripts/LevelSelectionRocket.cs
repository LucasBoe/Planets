using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionRocket : MonoBehaviour
{
    public int selected;
    float rotation = 0;
    Coroutine current;

    // Update is called once per frame
    void Update()
    {
        if (current != null)
            return;

        if (rotation >= 180)
        {
            if (Vector3.Distance(IndexToPosition(selected), (transform as RectTransform).anchoredPosition) > 0.1f && IndexToPosition(selected).x > (transform as RectTransform).anchoredPosition.x)
            {
                current = StartCoroutine(MoveRoutine(IndexToPosition(selected)));
            }
            else
            {
                rotation = -180;
                current = StartCoroutine(RotationRoutine(0));
            }
        }
        else
        {
            if (Vector3.Distance(IndexToPosition(selected), (transform as RectTransform).anchoredPosition) > 0.1f && IndexToPosition(selected).x < (transform as RectTransform).anchoredPosition.x)
            {
                current = StartCoroutine(MoveRoutine(IndexToPosition(selected)));
            }
            else
            {
                current = StartCoroutine(RotationRoutine(180));
            }
        }
    }

    private Vector3 IndexToPosition(int selected)
    {
        return new Vector3(selected * 200, 0, 0);
    }

    public void Teleport(int indexToTeleportTo)
    {
        (transform as RectTransform).anchoredPosition = IndexToPosition(indexToTeleportTo);
    }

    IEnumerator MoveRoutine(Vector3 target)
    {
        RectTransform rect = (transform as RectTransform);
        Vector3 positionBefore = rect.anchoredPosition;
        float t = 1;

        while (Vector2.Distance(rect.anchoredPosition, target) > 0.001f)
        {
            t -= Time.deltaTime;
            rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, target, Time.deltaTime * 150f);
            yield return null;
        }

        current = null;
    }

    IEnumerator RotationRoutine(float target)
    {
        while (rotation < target)
        {
            rotation += Time.deltaTime * 100f;
            transform.localRotation = Quaternion.Euler(0, 0, -rotation);
            yield return null;
        }

        current = null;
    }
}
