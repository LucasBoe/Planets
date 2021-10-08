using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
    public Rigidbody2D Target;
    public Rigidbody2D Self;
    public float pull;
    public float initialForce;
    // Use this for initialization
    void Start()
    {

        Self.AddForce(Vector2.up * initialForce); //add initial orbital velocity
        Self.AddTorque(100f); //add rotation

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector2.Distance(Self.position, Target.position);
        float Gravity = (Self.mass + Target.mass) / (dist * dist);
        Self.AddForce(Target.position - Self.position * (Gravity * pull)); //object to orbit, self, pull of gravity
    }
}
