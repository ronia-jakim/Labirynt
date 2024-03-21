using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isClosed = true;

    public Transform barrierObject;
    public Transform startPos;
    public Transform endPos; 
    public float speed = 5.0f;

    void Start() {
        isClosed = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" || other.tag == "Ball") {
            isClosed = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" || other.tag == "Ball") {
            isClosed = true;
        }
    }

    void Update () {
        if (isClosed) {
            barrierObject.position = Vector3.MoveTowards(barrierObject.position, startPos.position, speed * Time.deltaTime);
        }
        else {
            barrierObject.position = Vector3.MoveTowards(barrierObject.position, endPos.position, speed * Time.deltaTime);
        }
    }

}
