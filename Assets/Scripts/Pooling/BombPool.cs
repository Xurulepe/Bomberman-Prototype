using UnityEngine;

public class BombPool : ObjectPooling
{
    public static BombPool Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }
}
