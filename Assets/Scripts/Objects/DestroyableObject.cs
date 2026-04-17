using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public virtual void Destroy()
    {
        gameObject.SetActive(false);
    }
}
