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
            int bombPositionX = Mathf.RoundToInt(transform.position.x);
            int bombPositionY = Mathf.RoundToInt(transform.position.y);

            bomb.transform.position = new Vector3(bombPositionX, bombPositionY, transform.position.z);
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
