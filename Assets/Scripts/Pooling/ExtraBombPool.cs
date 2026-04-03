using UnityEngine;

public class ExtraBombPool : ObjectPooling
{
    public static ExtraBombPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
