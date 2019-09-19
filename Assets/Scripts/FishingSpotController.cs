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
    public Texture2D FishSpawnMask;

    // Start is called before the first frame update
    void Start()
    {
        //Going to assume that the box size is equal in all directions
        float ConversionRatio = (BoxSize.x) / FishSpawnMask.width;

        // for (int i = 0; i < FishSpawnMask.width; i++)
        // {
        //     for (int j = 0; j < FishSpawnMask.height; j++)
        //     {
        //         Vector3 SpawnPoint;
        //         SpawnPoint.x = (i * ConversionRatio) - (BoxSize.x/2.0f);
        //         SpawnPoint.y = 20.0f;
        //         SpawnPoint.z = (j * ConversionRatio) - (BoxSize.x/2.0f);

        //         int x = Mathf.FloorToInt(SpawnPoint.x);
        //         int z = Mathf.FloorToInt(SpawnPoint.z);
        //         bool CanSpawn = FishSpawnMask.GetPixel(i , j).r > 0? true: false;

        //         if (CanSpawn) 
        //         {
        //             GameObject TempFishingSpot = Instantiate(FishingSpot, SpawnPoint, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        //             CommonFishingSpots.Add(TempFishingSpot);
        //         }
        //     } 
        // }   

        while (CommonFishingSpots.Count < 200)
        {
            Vector2 TexturePoint;
            TexturePoint.x = Random.Range(0, FishSpawnMask.width);
            TexturePoint.y = Random.Range(0, FishSpawnMask.height);

            int x = Mathf.FloorToInt(TexturePoint.x);
            int y = Mathf.FloorToInt(TexturePoint.y);
            bool CanSpawn = FishSpawnMask.GetPixel(x , y).r > 0? true: false;
            
            Vector3 vSpawnPoint;
            vSpawnPoint.x = (TexturePoint.x * ConversionRatio) - BoxSize.x/2.0f;
            vSpawnPoint.y = 20.0f;
            vSpawnPoint.z = (TexturePoint.y * ConversionRatio) - BoxSize.x/2.0f;

            if (CanSpawn) 
            {
                GameObject TempFishingSpot = Instantiate(FishingSpot, vSpawnPoint, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
                CommonFishingSpots.Add(TempFishingSpot);
            }
            
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
