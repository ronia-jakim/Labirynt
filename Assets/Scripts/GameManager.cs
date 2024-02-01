using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private int timeToEnd;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null) gameManager = this;

        InvokeRepeating("Stopper", 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Stopper () {
        timeToEnd--;
        Debug.Log("Time to end: " + timeToEnd + " s");
    }
}
