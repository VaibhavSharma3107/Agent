using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpike : MonoBehaviour
{
    public float throwForce = 10f;

    private Rigidbody rb;
    private bool isGrabbedLeft = false;
    private bool isGrabbedRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Left hand trigger interaction
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            isGrabbedLeft = true;
            GrabObject(OVRInput.Controller.LTouch);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && isGrabbedLeft)
        {
            ThrowObject(OVRInput.Controller.LTouch);
        }

        // Right hand grip interaction
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            isGrabbedRight = true;
            GrabObject(OVRInput.Controller.RTouch);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) && isGrabbedRight)
        {
            ThrowObject(OVRInput.Controller.RTouch);
        }
    }

    void GrabObject(OVRInput.Controller controller)
    {
        rb.isKinematic = true;
        transform.SetParent(null);
    }

    void ThrowObject(OVRInput.Controller controller)
    {
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;

        Vector3 throwDirection = Camera.main.transform.forward;
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        if (controller == OVRInput.Controller.LTouch)
        {
            isGrabbedLeft = false;
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            isGrabbedRight = false;
        }
    }
}