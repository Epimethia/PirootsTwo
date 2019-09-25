using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class UIManagerScript : MonoBehaviour
{
    public GameObject PlayerCharacterObj;
    private PlayerCharacter PlayerCharacterScript;

    public Sprite PlayImage;
    public Sprite PauseImage;
    
    public Button PauseButton;

    void Start() {
        PlayerCharacterScript = PlayerCharacterObj.GetComponent<PlayerCharacter>();
    }

    void OnApplicationQuit()
    {
        int PreviousHighScore = PlayerPrefs.GetInt("PreviousHighScore");
        int CurrentScore = PlayerPrefs.GetInt("CurrentScore");

        if(CurrentScore > PreviousHighScore)
        {
            PlayerPrefs.SetInt("PreviousHighScore", CurrentScore);
            PlayerPrefs.SetInt("CurrentScore", 0);
        }
    }

    public void LeftPressed() 
    {
        FishingSpot CurrentFishingSpotScript = PlayerCharacterScript.CurrentFishingSpot.GetComponent<FishingSpot>();
        if(PlayerCharacterScript.CurrentFishingSpot) 
        {
            CurrentFishingSpotScript.SwapDamageType(EDamageType.LEFT);
        }
    }

    public void RightPressed() 
    {
        FishingSpot CurrentFishingSpotScript = PlayerCharacterScript.CurrentFishingSpot.GetComponent<FishingSpot>();
        if(PlayerCharacterScript.CurrentFishingSpot) 
        {
           CurrentFishingSpotScript.SwapDamageType(EDamageType.RIGHT);
        }
    }

    public void PullPressed() 
    {
        FishingSpot CurrentFishingSpotScript = PlayerCharacterScript.CurrentFishingSpot.GetComponent<FishingSpot>();
        if(PlayerCharacterScript.CurrentFishingSpot) 
        {
            CurrentFishingSpotScript.SwapDamageType(EDamageType.PULL);
        }
    }

    public void DropAnchor()
    {
        PlayerCharacterScript.bAnchorDropped = ! PlayerCharacterScript.bAnchorDropped;
    }

    public void ShowAd()
    {
        const string PlacementID = "video";
        if(Advertisement.IsReady())
        {
            //Doesnt really matter if the player watches the video or not
            Advertisement.Show(PlacementID);
            Debug.Log("Showed Ad");
        }
    }
}
