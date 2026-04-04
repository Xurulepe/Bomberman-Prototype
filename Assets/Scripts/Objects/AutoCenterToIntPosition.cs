using UnityEngine;

public class AutoCenterToIntPosition : MonoBehaviour
{
    private void OnEnable()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
}
