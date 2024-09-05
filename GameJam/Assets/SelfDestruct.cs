using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;

    void Awake()
    {
        Destroy(gameObject, destroyTime);
    }
}
