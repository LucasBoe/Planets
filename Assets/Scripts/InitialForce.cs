using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialForce : MonoBehaviour
{
    protected Rigidbody2D rigidbody2D;
    [SerializeField] Vector3 initialForce;
    [SerializeField] float forceScale = 100;

    void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        rigidbody2D.AddForce(initialForce * forceScale);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + initialForce);
    }
}
