using UnityEngine;

public class BombExplosionBufferPool : ObjectPooling
{
    public static BombExplosionBufferPool Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }
}
