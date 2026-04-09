using UnityEngine;

namespace Game.Enemy.AI
{
    public class EnemyCornerDetection : MonoBehaviour
    {
        [SerializeField] private PatrolBehaviour patrolBehaviour;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("MapCorner"))
            {
                patrolBehaviour.ChangeDirection();
            }
        }
    }
}
