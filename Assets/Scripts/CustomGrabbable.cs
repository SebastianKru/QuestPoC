using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class CustomGrabbable : OVRGrabbable
{
    private Transform originalParent;

    protected override void Start()
    {
        base.Start();
        originalParent = transform.parent;
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        originalParent = transform.parent;
        base.GrabBegin(hand, grabPoint);
        //transform.SetParent(hand.transform);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        transform.SetParent(originalParent);
    }
}
