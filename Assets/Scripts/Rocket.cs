using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : InitialForce
{
    [SerializeField] float motorForce;
    [SerializeField] float startForce;
    [SerializeField] RocketSettings settings;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform rocketDestroyPrefab;

    public bool On = false;
    public int Astronauts = 0;

    protected override void Start()
    {
        transform.rotation = settings.StartRotation;
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
                rigidbody2D.AddForce(transform.up * settings.StartForce);
                lineRenderer.enabled = false;
            }
        }
        else
        {
            transform.up = rigidbody2D.velocity.normalized;
            rigidbody2D.AddForce(Vector2.up * motorForce * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(rocketDestroyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
