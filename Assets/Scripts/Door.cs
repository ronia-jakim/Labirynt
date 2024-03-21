using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    public Transform doorObject;
    public Transform doorOpen;
    public Transform doorClose;

    public KeyColor myColor;
    public Material [] lista_materialow;

    public GameObject keyObject;

    public void Open() {
        isOpen = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        doorObject.position = doorClose.position;

        int nr_w_liscie = 0;
        switch(myColor) {
            case KeyColor.Red:
            nr_w_liscie = 0;
            break;
            case KeyColor.Green:
            nr_w_liscie = 1;
            break;
            case KeyColor.Gold:
            nr_w_liscie = 2;
            break;
            default:
            nr_w_liscie = 0;
            break;
        }

        keyObject.GetComponent<Renderer>().material = lista_materialow[nr_w_liscie];
    }

    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (isOpen && Vector3.Distance(doorObject.position, doorOpen.position) > 0.001f) {
            doorObject.position = Vector3.MoveTowards(doorObject.position, doorOpen.position, speed * Time.deltaTime);
        }
    }
}
