using UnityEngine;

public class DestroyableWall : DestroyableObject
{
    [SerializeField] private PowerUpType powerUpType;

    private void OnEnable()
    {
        int randomChance = Random.Range(0, 100);

        if (randomChance < 50)
        {
            powerUpType = PowerUpType.None;
        }
        else if (randomChance < 75)
        {
            powerUpType = PowerUpType.BombRangeIncrease;
        }
        else
        {
            powerUpType = PowerUpType.ExtraBomb;
        }
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

    public override void Destroy()
    {
        DropPowerUp();

        gameObject.SetActive(false);
    }
}
