using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotController : MonoBehaviour
{
    public Vector3 BoxSize;
    public Texture2D FishSpawnMask;
    private float ConversionRatio;

    public GameObject FishingSpot;
    public List<GameObject> FishingSpotList;
    private float SpawnTime;
    public FishSuccessScript FishingSuccessPopup;
    
    public UIManagerScript UIManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        //Going to assume that the box size is equal in all directions
        ConversionRatio = (BoxSize.x) / FishSpawnMask.width;
        //Spawn 10 fishing spots
        while (FishingSpotList.Count < 10)
        {
            SpawnFishingSpot();
        }
    }

    void SpawnFishingSpot()
    {
        bool CanSpawn = false;
        while (CanSpawn == false)
        {
            //Sample a random pixel in the fishing mask texture
            Vector2 TexturePoint;
            TexturePoint.x = Random.Range(0, FishSpawnMask.width);
            TexturePoint.y = Random.Range(0, FishSpawnMask.height);

            //Check if the pixel is more than zero. If so, we can spawn a fish at that location.
            int x = Mathf.FloorToInt(TexturePoint.x);
            int y = Mathf.FloorToInt(TexturePoint.y);
            CanSpawn = FishSpawnMask.GetPixel(x , y).r > 0? true: false;

            //Convert the pixel location to world location
            Vector3 vSpawnPoint;
            vSpawnPoint.x = (TexturePoint.x * ConversionRatio) - BoxSize.x/2.0f;
            vSpawnPoint.y = 15.0f;
            vSpawnPoint.z = (TexturePoint.y * ConversionRatio) - BoxSize.x/2.0f;

            //Check if the fishing spot is in range of any other fishing spots. If so, retry the spawn again
            bool InRange = false;
            for(int i = 0; i < FishingSpotList.Count; i++)
            {
                if (Vector3.Distance(FishingSpotList[i].transform.position, vSpawnPoint) < 50.0f)
                {
                    InRange = true;
                    CanSpawn = false;
                    break;
                }
            }

            //Otherwise, spawn the fishing spot
            if (CanSpawn == true && InRange == false) 
            {
                GameObject TempFishingSpot = Instantiate(FishingSpot, vSpawnPoint, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                TempFishingSpot.GetComponent<FishingSpot>().DestructionEvent.AddListener(DestroyFishingSpot);
                TempFishingSpot.GetComponent<FishingSpot>().FishingEventSuccessful.AddListener(FishingEvent);
                FishingSpotList.Add(TempFishingSpot);
            }
        }
    }

    void DestroyFishingSpot(GameObject _FishingSpotRef)
    {
        if(FishingSpotList.Remove(_FishingSpotRef))
        {
            Destroy(_FishingSpotRef);
        }
        SpawnFishingSpot();
    }

    void FishingEvent(Fish _Fish, int _Score)
    {
        int CurrentScore = PlayerPrefs.GetInt("CurrentScore");
        CurrentScore += _Score;
        PlayerPrefs.SetInt("CurrentScore", CurrentScore);
        Debug.Log(CurrentScore);

        int AdCount = PlayerPrefs.GetInt("CurrentAdCount");
        if(AdCount > 2)
        {
            UIManagerObj.ShowAd();
            PlayerPrefs.SetInt("CurrentAdCount", 0);
        }  
        else
        {
            PlayerPrefs.SetInt("CurrentAdCount", ++AdCount);
        };

        FishingSuccessPopup.Init(_Fish);
        FishingSuccessPopup.PopIn();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(transform.position, BoxSize);
    }

}
