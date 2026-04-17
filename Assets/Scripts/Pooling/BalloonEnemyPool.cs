using UnityEngine;

public class BalloonEnemyPool : ObjectPooling
{
    public static BalloonEnemyPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
