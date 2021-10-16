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
    [SerializeField] FollowTransform rocketTail;

    public bool On = false;
    public int Astronauts = 0;

    protected Rigidbody2D rigidbody2D;

    private void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        transform.rotation = settings.StartRotation;
        Instantiate(rocketTail).ToFollow = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!On)
        {
            transform.Rotate(Vector3.forward, -Input.GetAxis("Horizontal"));
            settings.StartRotation = transform.rotation;
            settings.StartForce += Input.GetAxis("Vertical");
            lineRenderer.SetPosition(1, new Vector3(0, settings.StartForce / 50, 1));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                On = true;
                foreach (SimulationBehaviour behaviour in FindObjectsOfType<SimulationBehaviour>())
                {
                    behaviour.StartSimulation();
                }
            }
        }
        else
        {
            transform.up = rigidbody2D.velocity.normalized;
            rigidbody2D.AddForce(Vector2.up * motorForce * Time.deltaTime);
        }
    }

    public override void StartSimulation()
    {
        rigidbody2D.AddForce(transform.up * settings.StartForce);
        lineRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(rocketDestroyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
