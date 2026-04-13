using UnityEngine;
using Game.Player;

public class ExtraBomb : MonoBehaviour
{
    [SerializeField] private int bombIncreaseAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerAttributes playerAttributes))
        {
            playerAttributes.IncreaseMaxBombs(bombIncreaseAmount);
            
            gameObject.SetActive(false);
        }
    }
}
