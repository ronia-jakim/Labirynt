using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool xRotation = false;
    public bool yRotation = false;
    public bool zRotation = true;

    public AudioClip pickClip;

    private float xRot, yRot, zRot;

    public float rotSpeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        if (xRotation) xRot = 1.0f;
        else xRot = 0.0f;

        yRot = yRotation ? 1.0f : 0.0f;
        zRot = zRotation ? 1.0f : 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    public virtual void Picked() {
        Debug.Log("Podniosłem");
        Destroy(this.gameObject);
        GameManager.gameManager.PlayClip(pickClip);
    }

    public void Rotation () {
        Vector3 rot = new Vector3(xRot, yRot, zRot);
        rot = rot * rotSpeed;
        transform.Rotate(rot);
    }
}
