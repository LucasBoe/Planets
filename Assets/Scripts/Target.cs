using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    LevelHandler levelHandler;

    [SerializeField] AudioSource reachedTargetSource;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * 10);
    }

    private void OnEnable()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
        Vector3[] points = Util.CreatePointsInCircle(4, 36).ToArray();
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelHandler != null && collision.CompareTag("Player"))
        {
            reachedTargetSource.Play();
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
