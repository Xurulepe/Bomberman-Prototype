using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombAttack : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private GameObject bombPrefab;

    [Header("Player Attributes")]
    [SerializeField] private PlayerAttributes playerAttributes;

    private int remainingBombs;

    private void OnEnable()
    {
        playerAttributes.OnPowerUpCollected += UpdateAttibutes;
    }

    private void OnDisable()
    {
        playerAttributes.OnPowerUpCollected -= UpdateAttibutes;
    }

    private void Start()
    {
        UpdateAttibutes();
    }

    public void DropBomb()
    {
        GameObject bomb = BombPool.Instance.GetPooledObject();

        if (bomb != null)
        {
            int bombPositionX = Mathf.RoundToInt(transform.position.x);
            int bombPositionY = Mathf.RoundToInt(transform.position.y);

            bomb.transform.position = new Vector3(bombPositionX, bombPositionY, transform.position.z);
            bomb.transform.rotation = transform.rotation;

            Bomb bombScript = bomb.GetComponent<Bomb>();
            bombScript.SetupBomb(playerAttributes);
            bombScript.OnBombExploded += RecoverOneBomb;

            bomb.SetActive(true);

            remainingBombs--;
        }
    }

    public void SetBombAttack(InputAction.CallbackContext context)
    {
        if (context.performed && remainingBombs > 0)
        {
            DropBomb();
        }
    }

    private void RecoverOneBomb()
    {
        if (remainingBombs < playerAttributes.MaxBombs)
        {
            remainingBombs++;
        }
    }

    private void UpdateAttibutes()
    {
        remainingBombs = playerAttributes.MaxBombs;
    }
}
