using UnityEngine;

namespace Game.Player
{
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private int enemyLayerMask;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == enemyLayerMask)
            {
                player.Destroy();
            }
        }
    }
}
