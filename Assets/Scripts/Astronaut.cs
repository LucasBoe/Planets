using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    LevelHandler levelHandler;
    float rotation;

    private void OnEnable()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rotation = UnityEngine.Random.Range(-1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelHandler.Astronauts += 1;
            StartCoroutine(MoveToRocketRoutine(collision.transform));
            Destroy(GetComponent<Collider2D>());
        }
    }

    private IEnumerator MoveToRocketRoutine(Transform rocket)
    {
        float t = 1f;
        while (t > 0)
        {
            transform.localScale = Vector3.one * (t / 1f);
            transform.position = Vector3.Lerp(transform.position, rocket.position, 1f-t);
            t -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
