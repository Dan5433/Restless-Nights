using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] Transform spawn;

    void Start()
    {
        transform.position = spawn.position;
    }
}
