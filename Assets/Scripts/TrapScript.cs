using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{

    // easyMode - pulapka odejmuje ptk
    // hard mode - pulapka konczy gre (?)
    public bool easyMode = true;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (easyMode) GameManager.gameManager.AddPoints(-3);
            else GameManager.gameManager.EndGame();
        }
    }
}
