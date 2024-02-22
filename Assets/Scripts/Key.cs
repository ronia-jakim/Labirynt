public enum KeyColor
{
    Red,
    Green, 
    Gold,
}

public class Key : PickUp
{
    public KeyColor color;

    public override void Picked()
    {
        base.Picked();
        GameManager.gameManager.AddKey(color);
    }
}
