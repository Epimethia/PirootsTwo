using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FishingSpot : MonoBehaviour
{
    private MeshRenderer rend;
    public float MinSpawnTime, MaxSpawnTime;
    private float SpawnTime;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        SpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnFish()
    {
        yield return new WaitForSecondsRealtime(SpawnTime);
        Debug.Log("SpawnedFish");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.SetFloat("Vector1_E4BA77E7", 1.0f);
            StartCoroutine(SpawnFish());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.SetFloat("Vector1_E4BA77E7", 0.0f);
        }
    }
}
