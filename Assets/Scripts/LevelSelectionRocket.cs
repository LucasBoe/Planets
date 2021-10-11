using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionRocket : MonoBehaviour
{
    [SerializeField] LevelUI selected;
    float rotation = 0;
    Coroutine current;

    // Update is called once per frame
    void Update()
    {
        if (current != null || selected == null)
            return;

        if (rotation >= 180)
        {
            if (Vector3.Distance(selected.transform.position, transform.position) > 0.1f && selected.transform.position.x > transform.position.x)
            {
                current = StartCoroutine(MoveRoutine(selected.transform.position));
            }
            else
            {
                rotation = -180;
                current = StartCoroutine(RotationRoutine(0));
            }
        }
        else
        {
            if (Vector3.Distance(selected.transform.position, transform.position) > 0.1f  && selected.transform.position.x < transform.position.x)
            {
                Debug.Log(selected.transform.position + " - " + transform.position);

                current = StartCoroutine(MoveRoutine(selected.transform.position));
            }
            else
            {
                current = StartCoroutine(RotationRoutine(180));
            }
        }
    }

    IEnumerator MoveRoutine(Vector3 position)
    {
        Vector3 positionBefore = transform.position;
        float t = 1;

        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.position = Vector3.Lerp(position, positionBefore, t);
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
