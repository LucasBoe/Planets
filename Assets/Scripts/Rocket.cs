using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : SimulationBehaviour
{
    [SerializeField] float motorForce;
    [SerializeField] float startForce;
    [SerializeField] RocketSettings settings;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform rocketDestroyPrefab;
    [SerializeField] FollowTransform rocketTailPrefab;
    [SerializeField] RocketRotationHandle rocketRotationHandle;

    public bool On = false;
    public int Astronauts = 0;

    protected Rigidbody2D rigidbody2D;

    private FollowTransform trail;
    List<Vector3> currentPath;

    public System.Action OnRocketLaunch;
    public System.Action OnRocketCrash;

    private void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        Debug.LogWarning("Rocket Disabled");
    }

    // Update is called once per frame
    void Update()
    {
        if (!On)
        {
            Vector2 lookAt = rocketRotationHandle.transform.localPosition;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f);
            settings.StartForce += Input.GetAxis("Vertical");
            lineRenderer.SetPosition(1, new Vector3(0, settings.StartForce / 50, 1));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
            }
        }
        else
        {
            transform.up = rigidbody2D.velocity.normalized;
            rigidbody2D.AddForce(Vector2.up * motorForce * Time.deltaTime);

            if (currentPath != null && currentPath.Count > 0)
            {
                Vector3 prev = currentPath[currentPath.Count - 1];

                if (Vector2.Distance(prev, transform.position) > 1f)
                    currentPath.Add(transform.position);
            }
            else
            {
                currentPath = new List<Vector3>();
                currentPath.Add(transform.position);
            }
        }
    }

    private void OnDestroy()
    {
        if (currentPath != null)
        {
            Path current = new Path();
            current.Points = currentPath.ToArray();
            settings.previousPaths.Add(current);
        }

        while (settings.previousPaths.Count > 3)
            settings.previousPaths.RemoveAt(0);
    }

    public void Launch()
    {
        if (On)
            return;

        OnRocketLaunch?.Invoke();

        On = true;
        foreach (SimulationBehaviour behaviour in FindObjectsOfType<SimulationBehaviour>())
        {
            behaviour.StartSimulation();
        }
    }

    public void Teleport(Vector3 position)
    {
        Destroy(trail);
        transform.position = position;
        trail = CreateRocketTrail();
    }

    public override void StartSimulation()
    {
        Time.timeScale = 1.5f;
        transform.parent = null;
        rigidbody2D.AddForce(transform.up * settings.StartForce);
        lineRenderer.enabled = false;
        trail = CreateRocketTrail();
    }

    private FollowTransform CreateRocketTrail()
    {
        var trail = Instantiate(rocketTailPrefab, transform.position, Quaternion.identity);
        trail.ToFollow = transform;
        return trail;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(rocketDestroyPrefab, transform.position, transform.rotation);
        OnRocketCrash?.Invoke();
        Destroy(gameObject);
    }
}
