using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    bool iCanOpen = false;

    public KeyColor myColor;
    bool locked = false;
    Animator key;

    void Start () {
        key = GetComponent<Animator>();
    }

    void Update () {
        if (iCanOpen && !locked && Input.GetKeyDown(KeyCode.E)) {
            key.SetBool("UseKey", CheckTheKey());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            iCanOpen = true;
            Debug.Log("Możesz użyć zamka");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            iCanOpen = false;
            Debug.Log("Nie możesz już używać zamka");
        }
    }

    public bool CheckTheKey () {
        if (GameManager.gameManager.redKeys > 0 && myColor == KeyColor.Red) {
            GameManager.gameManager.redKeys--;
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.goldKeys > 0 && myColor == KeyColor.Gold) {
            GameManager.gameManager.goldKeys--;
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.greenKeys > 0 && myColor == KeyColor.Green) {
            GameManager.gameManager.greenKeys--;
            locked = true;
            return true;
        }
        else {
            Debug.Log("Nie masz klucza");
            return false;
            // łapka w górę kiedy skończycie pisać
        }
    }

    public Door [] doors;

    public void UseKey () {
        foreach(Door d in doors) {
            d.Open();
        }
    }
}
