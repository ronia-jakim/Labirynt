using UnityEngine;

public class Clock : PickUp
{
    [SerializeField] private bool addTime;
    [SerializeField] private int time = 5;

    public override void Picked()
    {
        base.Picked();

        int sign = -1;
        if (addTime)
            sign = 1;

        GameManager.gameManager.AddTime(sign * time);
    }
}
