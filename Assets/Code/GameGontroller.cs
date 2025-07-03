using System.Collections;
using UnityEngine;

public class GameGontroller : MonoBehaviour
{
    [SerializeField] private FallingBlock block;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CreateBlock());

    }

    IEnumerator CreateBlock()
    {
        yield return new WaitForSeconds(Random.Range(0.8f,3f));
        Instantiate(block,new Vector3(Random.Range(-6f,6f),6,0), default);
        StartCoroutine(CreateBlock());
    }
}
