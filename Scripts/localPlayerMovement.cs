using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class localPlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public class Objects
    {
        public Transform lp, lp_head, lp_left_hand, lp_right_hand;
    }
    public Objects objects;
    
    [System.Serializable]
    public class Values
    {
        public float smallBoost, bigBoost, breakBoost, turnSpeed;
        public int boostAmount;
    }
    public Values values;

    [System.Serializable]
    public class Inputs
    {
        public InputActionReference boostLeft, boostRight, breaking, bigBoost, yaw, pitch, roll;
    }
    public Inputs inputs;
    
    [System.Serializable]
    public class Other
    {
        public Rigidbody lp_RB;
        public bool canUseMovement;
        public bool lp_locked;
    }
    public Other other;

    void FixedUpdate()
    {
        if(inputs.boostLeft.action.IsPressed())
        {
            if (other.lp_RB.velocity.magnitude <= 4)
            {
                other.lp_RB.AddForce(objects.lp_left_hand.forward * values.smallBoost);
            }
            else
            {
                // Change direction but counteract speed
                Vector3 currentVelocity = other.lp_RB.velocity;
                Vector3 targetDirection = objects.lp_left_hand.forward.normalized;

                // Calculate the force to negate current velocity and redirect to the target direction
                Vector3 counteractForce = -currentVelocity * other.lp_RB.mass; // Negate current momentum
                Vector3 redirectForce = targetDirection * currentVelocity.magnitude * other.lp_RB.mass;

                other.lp_RB.AddForce(counteractForce + redirectForce);
            }
                
        }
        if(inputs.boostRight.action.IsPressed())
        {
            if (other.lp_RB.velocity.magnitude <= 4)
            {
                other.lp_RB.AddForce(objects.lp_right_hand.forward * values.smallBoost);
            }
            else
            {
                // Change direction but counteract speed
                Vector3 currentVelocity = other.lp_RB.velocity;
                Vector3 targetDirection = objects.lp_right_hand.forward.normalized;

                // Calculate the force to negate current velocity and redirect to the target direction
                Vector3 counteractForce = -currentVelocity * other.lp_RB.mass; // Negate current momentum
                Vector3 redirectForce = targetDirection * currentVelocity.magnitude * other.lp_RB.mass;

                other.lp_RB.AddForce(counteractForce + redirectForce);
            }
        }
        if(inputs.breaking.action.IsPressed())
        {
            other.lp_RB.velocity *= values.breakBoost;
        }
        // turn
        if(inputs.yaw.action.ReadValue<Vector2>().x >= 0.5 || inputs.yaw.action.ReadValue<Vector2>().x <= -0.5)
        {
            other.lp_RB.AddTorque(transform.up * values.turnSpeed * inputs.yaw.action.ReadValue<Vector2>().x, ForceMode.VelocityChange);
        }
    }
}