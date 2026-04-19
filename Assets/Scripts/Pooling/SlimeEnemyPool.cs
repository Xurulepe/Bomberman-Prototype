using UnityEngine;

public class SlimeEnemyPool : ObjectPooling
{
    public static SlimeEnemyPool Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }
}
