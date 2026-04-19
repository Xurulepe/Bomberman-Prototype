using UnityEngine;

public class BalloonEnemyPool : ObjectPooling
{
    public static BalloonEnemyPool Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }
}
