using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Wormhole target;

    public AudioSource startTeleportSource, endTeleportSource;

    public bool isReceiving;

    Coroutine teleportationRoutine = null;

    void Start()
    {
        Vector3[] circlePoints = Util.CreatePointsInCircle(2, 24).ToArray();
        lineRenderer.positionCount = circlePoints.Length;
        lineRenderer.SetPositions(circlePoints);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportationRoutine == null && !isReceiving && collision.CompareTag("Player") && target != null)
        {
            Debug.Log("Entered Wormhole");
            StopAllCoroutines();
            teleportationRoutine = StartCoroutine(TeleportationRoutine(collision));
        }
    }

    private IEnumerator TeleportationRoutine(Collider2D player)
    {
        Vector3 startPos = player.transform.position;
        target.isReceiving = true;
        startTeleportSource.Play();

        float t = 0.5f;

        PlayAnimation();
        target.PlayAnimation();

        while (t > 0)
        {
            float lerp = t / 0.5f;
            t -= Time.deltaTime * 2;
            player.transform.position = Vector3.Lerp(transform.position, startPos, lerp);
            player.transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one, lerp);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Rocket>().Teleport(target.transform.position);
        target.endTeleportSource.Play();

        while (t < 0.5f)
        {
            float lerp = t / 0.5f;
            t += Time.deltaTime * 2;
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
