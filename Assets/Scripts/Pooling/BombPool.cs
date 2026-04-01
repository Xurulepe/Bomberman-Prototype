using UnityEngine;

public class BombPool : ObjectPooling
{
    public static BombPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
