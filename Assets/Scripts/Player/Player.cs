using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour, IDestroyable
    {
        public void Destroy()
        {

            Debug.Log("Player destroyed!");

            GameManager.Instance.GameOver(GameManager.GameOverType.Lose);

            gameObject.SetActive(false);
        }
    }
}
