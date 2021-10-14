using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    float time = 0;
    [SerializeField] AnimationCurve widthAndHeightOverTime;
    [SerializeField] RectTransform cirlce;

    private void Start()
    {
        Target target = FindObjectOfType<Target>();

        if (target != null)
            cirlce.position = Camera.main.WorldToScreenPoint(target.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float value = widthAndHeightOverTime.Evaluate(time);
        cirlce.sizeDelta = new Vector2(value, value);
    }
}
