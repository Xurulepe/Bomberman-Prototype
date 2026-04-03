using System;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;

    private void OnDisable()
    {
        DropPowerUp();
    }

    public void DropPowerUp()
    {
        switch (powerUpType)
        {
            case PowerUpType.BombRangeIncrease:
                DropBombExplosionBuffer();
                break;
            case PowerUpType.ExtraBomb:
                DropExtraBomb();
                break;
        }
    }

    private void DropExtraBomb()
    {
        GameObject powerUp = ExtraBombPool.Instance.GetPooledObject();

        if (powerUp != null)
        {
            powerUp.transform.position = transform.position;
            powerUp.transform.rotation = transform.rotation;
            powerUp.SetActive(true);
        }
    }

    private void DropBombExplosionBuffer()
    {
        GameObject powerUp = BombExplosionBufferPool.Instance.GetPooledObject();

        if (powerUp != null)
        {
            powerUp.transform.position = transform.position;
            powerUp.transform.rotation = transform.rotation;
            powerUp.SetActive(true);
        }
    }
}
