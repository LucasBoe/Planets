using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Wormhole target;

    public bool isReceiving;

    void Start()
    {
        Vector3[] circlePoints = Util.CreatePointsInCircle(2, 24).ToArray();
        lineRenderer.positionCount = circlePoints.Length;
        lineRenderer.SetPositions(circlePoints);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReceiving && collision.CompareTag("Player") && target != null)
        {
            Debug.Log("Entered Wormhole");
            StopAllCoroutines();
            StartCoroutine(TeleportationRoutine(collision));
        }
    }

    private IEnumerator TeleportationRoutine(Collider2D player)
    {
        Vector3 startPos = player.transform.position;
        target.isReceiving = true;

        float t = 0.5f;

        PlayAnimation();
        target.PlayAnimation();

        while (t > 0)
        {
            float lerp = t / 0.5f;
            t -= Time.deltaTime * 4;
            player.transform.position = Vector3.Lerp(transform.position, startPos, lerp);
            player.transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, lerp);
            yield return null;
        }

        player.GetComponent<Rocket>().Teleport(target.transform.position);

        while (t < 0.5f)
        {
            float lerp = t / 0.5f;
            t += Time.deltaTime * 4;
            player.transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, Mathf.Sqrt(Mathf.Clamp(lerp,0.01f, 1f)));
            yield return null;
        }

        target.isReceiving = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (target != null)
            Gizmos.DrawLine(target.transform.position, transform.position);
    }

    private void PlayAnimation()
    {
        GetComponent<Animator>().Play("Wormhole_Anim");
    }
}
