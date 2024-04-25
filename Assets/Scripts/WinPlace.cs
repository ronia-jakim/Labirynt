using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlace : MonoBehaviour
{
    float alpha = 0;

    public float Resizer() {
        float value = Mathf.Sin(alpha);
        alpha += (1.5f * Time.deltaTime);
        return value + 2f;
    }

    void FixedUpdate() {
        float scale = Resizer();
        transform.localScale = new Vector3(scale, 10f, scale);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.gameManager.WinGame();
        }
    }
}
