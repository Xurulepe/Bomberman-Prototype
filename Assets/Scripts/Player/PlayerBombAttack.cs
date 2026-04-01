using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombAttack : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private GameObject bombPrefab;

    public void DropBomb()
    {
        GameObject bomb = BombPool.Instance.GetPooledObject();

        if (bomb != null)
        {
            bomb.transform.position = transform.position;
            bomb.transform.rotation = transform.rotation;
            bomb.SetActive(true);
        }
    }

    public void SetBombAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DropBomb();
        }
    }
}
