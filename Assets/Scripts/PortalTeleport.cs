using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform receiver;
    
    public bool playerIsOverlapping = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerIsOverlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerIsOverlapping = false;
    }

    private void FixedUpdate()
    {
        Teleportation();
    }

    private void Teleportation()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0, rotationDiff, 0) * portalToPlayer;
                player.position = positionOffset + receiver.position;

                playerIsOverlapping = false;
            }
        }
    }
}
