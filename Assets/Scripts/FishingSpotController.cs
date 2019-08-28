using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotController : MonoBehaviour
{
    public Vector3 BoxSize;
    public GameObject FishingSpot;
    public List<GameObject> CommonFishingSpots;
    public List<GameObject> UnCommonFishingSpots;
    public List<GameObject> RareFishingSpots;
    private float SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
       
            
        while (CommonFishingSpots.Count < 4)
        {
            Vector3 SpawnPoint;
            SpawnPoint.x = Random.Range(-BoxSize.x, BoxSize.x) / 2.0f;
            SpawnPoint.y = 20.0f;
            SpawnPoint.z = Random.Range(-BoxSize.z, BoxSize.z) / 2.0f;

            GameObject TempFishingSpot = Instantiate(FishingSpot, SpawnPoint, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
            CommonFishingSpots.Add(TempFishingSpot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, BoxSize);
    }
}
