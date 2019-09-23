using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public GameObject PlayerCharacterObj;
    private PlayerCharacter PlayerCharacterScript;

    void Start() {
        PlayerCharacterScript = PlayerCharacterObj.GetComponent<PlayerCharacter>();
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

    public void FishSuccessful(Fish _FishScript)
    {
        
    }
}
