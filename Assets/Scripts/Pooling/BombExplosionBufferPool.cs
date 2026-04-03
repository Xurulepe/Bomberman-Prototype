using UnityEngine;

public class BombExplosionBufferPool : ObjectPooling
{
    public static BombExplosionBufferPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
