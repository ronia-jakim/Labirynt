using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    void Update()
    {
        PortalCameraController();
    }

    private void PortalCameraController () {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalsRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotDiff = Quaternion.AngleAxis(angularDifferenceBetweenPortalsRotation, Vector3.up);
        Vector3 newCamPos = portalRotDiff * playerCamera.forward;
        newCamPos = new Vector3 (newCamPos.x * -1, newCamPos.y, newCamPos.z * -1); 

        transform.rotation = Quaternion.LookRotation(newCamPos, Vector3.up);
    }

}
