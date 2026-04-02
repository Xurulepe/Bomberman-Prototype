using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
