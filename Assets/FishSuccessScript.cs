using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FishIcon
{
    
    public EFishType FishIconType;
    public Sprite Icon;
}

public class FishSuccessScript : MonoBehaviour
{
    public List<FishIcon> IconList;
    public Text FishName;
    public Text FishRarity;
    public Text FishWeight;
    public Image FishImage;

    public void Init(Fish _Fish)
    {
        FishImage.sprite = GetFishIcon(_Fish.m_FishType);
    }

    private Sprite GetFishIcon(EFishType _Type)
    {
        foreach(FishIcon icon in IconList)
        {
            if(icon.FishIconType == _Type)
            {
                Debug.Log("Success");
                return icon.Icon;
            }
        }
        Debug.Log("Fail");
        return IconList[0].Icon;
    }
}
