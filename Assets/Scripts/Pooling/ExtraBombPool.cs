using UnityEngine;

public class ExtraBombPool : ObjectPooling
{
    public static ExtraBombPool Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }
}
