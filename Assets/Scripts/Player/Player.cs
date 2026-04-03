using UnityEngine;

public class Player : MonoBehaviour, IDestroyable
{
    public void Destroy()
    {
        
        Debug.Log("Player destroyed!");

        gameObject.SetActive(false);
    }
}
