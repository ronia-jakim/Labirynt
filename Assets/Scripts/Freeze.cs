using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : PickUp
{
    [SerializeField] private int freezeTime = 10;

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    public override void Picked()
    {
        GameManager.gameManager.FreezeTime(freezeTime);
        base.Picked();
    }
}