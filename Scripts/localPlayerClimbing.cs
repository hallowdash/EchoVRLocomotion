using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class localPlayerClimbing : MonoBehaviour
{
    [System.Serializable]
    public class Objects
    {
        public Transform lp, lp_left_hand, lp_right_hand;
    }
    public Objects objects;

    [System.Serializable]
    public class Values
    {
        public Vector3 leftGrabPoint;
        public Vector3 rightGrabPoint;
        public float climbInput;
    }
    public Values values;

    [System.Serializable]
    public class Inputs
    {
        public InputActionReference climbLeft, climbRight, veloLeft, veloRight;
    }
    public Inputs inputs;

    [System.Serializable]
    public class Other
    {
        public Rigidbody lp_RB;
        public bool isLeftGrabPointSet;
        public bool isRightGrabPointSet;
        public bool leftLaunch;
        public bool rightLaunch;
    }
    public Other other;

    public localPlayerMovement lpm;
    public colliderCheck lCC;
    public colliderCheck rCC;

    // Update is called once per frame
    void Update()
    {
        if(inputs.climbLeft.action.ReadValue<float>() >= values.climbInput && lCC.inCol)
        {
            if(!other.isLeftGrabPointSet)
            {
                values.leftGrabPoint = objects.lp_left_hand.position;
                lpm.other.canUseMovement = false;
                other.lp_RB.velocity = Vector3.zero;
                other.isLeftGrabPointSet = true;
                other.leftLaunch = false;
                lCC.sphereCollider.radius = 0.15f;
            }

            other.lp_RB.MovePosition(other.lp_RB.position - (objects.lp_left_hand.position - values.leftGrabPoint));
        }
        else
        {
            if(!other.leftLaunch)
            {
                other.lp_RB.velocity = (other.lp_RB.rotation * -inputs.veloLeft.action.ReadValue<Vector3>());
                other.leftLaunch = true;
            }
            values.leftGrabPoint = new Vector3(0, 0, 0);
            other.isLeftGrabPointSet = false;
            lCC.sphereCollider.radius = 0.1f;
        }

        if(inputs.climbRight.action.ReadValue<float>() >= values.climbInput && rCC.inCol)
        {
            if(!other.isRightGrabPointSet)
            {
                values.rightGrabPoint = objects.lp_right_hand.position;
                lpm.other.canUseMovement = false;
                other.lp_RB.velocity = Vector3.zero;
                other.isRightGrabPointSet = true;
                other.rightLaunch = false;
                rCC.sphereCollider.radius = 0.15f;
            }

            other.lp_RB.MovePosition(other.lp_RB.position - (objects.lp_right_hand.position - values.rightGrabPoint));
        }
        else
        {
            if(!other.rightLaunch)
            {
                other.lp_RB.velocity = (other.lp_RB.rotation * -inputs.veloRight.action.ReadValue<Vector3>());
                other.rightLaunch = true;
            }

            values.rightGrabPoint = new Vector3(0, 0, 0);
            other.isRightGrabPointSet = false;
            rCC.sphereCollider.radius = 0.1f;
        }
    }
}
