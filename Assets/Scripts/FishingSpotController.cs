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
    private float ConversionRatio;

    // Start is called before the first frame update
    void Start()
    {
        //Going to assume that the box size is equal in all directions
        ConversionRatio = (BoxSize.x) / FishSpawnMask.width;

        while (CommonFishingSpots.Count < 10)
        {
            SpawnFishingSpot();
        }
    }

    void SpawnFishingSpot()
    {
        bool CanSpawn = false;
        while (CanSpawn == false)
        {
            Vector2 TexturePoint;
            TexturePoint.x = Random.Range(0, FishSpawnMask.width);
            TexturePoint.y = Random.Range(0, FishSpawnMask.height);

            int x = Mathf.FloorToInt(TexturePoint.x);
            int y = Mathf.FloorToInt(TexturePoint.y);
            CanSpawn = FishSpawnMask.GetPixel(x , y).r > 0? true: false;

            Vector3 vSpawnPoint;
            vSpawnPoint.x = (TexturePoint.x * ConversionRatio) - BoxSize.x/2.0f;
            vSpawnPoint.y = 20.0f;
            vSpawnPoint.z = (TexturePoint.y * ConversionRatio) - BoxSize.x/2.0f;

            bool InRange = false;

            for(int i = 0; i < CommonFishingSpots.Count; i++)
            {
                if (Vector3.Distance(CommonFishingSpots[i].transform.position, vSpawnPoint) < 50.0f)
                {
                    InRange = true;
                    CanSpawn = false;
                    break;
                }
            }

            if (CanSpawn == true && InRange == false) 
            {
                GameObject TempFishingSpot = Instantiate(FishingSpot, vSpawnPoint, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
                TempFishingSpot.GetComponent<FishingSpot>().DestructionEvent.AddListener(DestroyFishingSpot);
                CommonFishingSpots.Add(TempFishingSpot);
            }
        }
        
    }

    void DestroyFishingSpot(GameObject _FishingSpotRef)
    {

        if(CommonFishingSpots.Remove(_FishingSpotRef))
        {
            Destroy(_FishingSpotRef);
        }
        SpawnFishingSpot();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(transform.position, BoxSize);
    }

   
}
