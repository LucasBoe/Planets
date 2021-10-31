using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueRocket : MonoBehaviour
{
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0), Time.deltaTime);
    }
}
