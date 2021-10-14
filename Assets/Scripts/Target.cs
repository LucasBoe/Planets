using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    LevelHandler levelHandler;

    private void OnEnable()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelHandler != null && collision.CompareTag("Player"))
        {
            levelHandler.ReachedTarget();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rigidbody2D = collision.attachedRigidbody;
            Vector2 targetVector = ((Vector2)transform.position - rigidbody2D.position).normalized;
            float targetVelocity = Vector2.Distance(rigidbody2D.position, (Vector2)transform.position);
            rigidbody2D.velocity = Vector3.MoveTowards(rigidbody2D.velocity, targetVector * targetVelocity, Time.deltaTime * 50f);

            collision.transform.localScale /= (1f + Time.deltaTime);
        }
    }
}
