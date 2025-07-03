using System.Collections;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private float fallingSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocityY = -fallingSpeed;
        StartCoroutine(DestroyMe());
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
