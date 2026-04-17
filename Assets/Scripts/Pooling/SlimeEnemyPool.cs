using UnityEngine;

public class SlimeEnemyPool : ObjectPooling
{
    public static SlimeEnemyPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
