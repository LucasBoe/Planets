using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rocket>())
            GravityVisualizer.Instance.EnterGravity(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rocket>())
            GravityVisualizer.Instance.LeaveGravity(this);
    }

    private void OnDrawGizmos()
    {
        foreach (CircleCollider2D coll in GetComponents<CircleCollider2D>())
        {
            if (coll.usedByEffector)
                Gizmos.DrawWireSphere(transform.position, coll.radius * transform.localScale.x);
        }
    }
}
