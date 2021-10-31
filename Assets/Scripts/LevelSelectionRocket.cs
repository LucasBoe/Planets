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

    IEnumerator MoveRoutine(Vector3 position)
    {
        Vector3 positionBefore = (transform as RectTransform).anchoredPosition;
        float t = 1;

        while (t > 0)
        {
            t -= Time.deltaTime;
            (transform as RectTransform).anchoredPosition = Vector3.Lerp(position, positionBefore, t);
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
