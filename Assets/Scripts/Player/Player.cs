using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour, IDestroyable
    {
        public void Destroy()
        {

            Debug.Log("Player destroyed!");

            gameObject.SetActive(false);
        }
    }
}
