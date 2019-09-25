using System;
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
        if(_Fish != null)
        {
            FishImage.sprite = GetFishIcon(_Fish.m_FishType);
            FishRarity.color = GetFishRarityColor(_Fish.m_Grade);
            FishRarity.text = GetFishRarityText(_Fish.m_Grade);
            FishName.text = GetFishText(_Fish.m_FishType);
            FishWeight.text = _Fish.m_Weight.ToString();
        }
        else
        {
            Debug.Log("Fish Null");
        }
       
    }

    public void PopIn()
    {
        GetComponent<Animator>().SetBool("Active", true);
    }

    public void PopOut()
    {
        GetComponent<Animator>().SetBool("Active", false);
    }

    private string GetFishRarityText(EFishGrade _Grade)
    {
        return _Grade.ToString();
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

    private string GetFishText(EFishType _Type)
    {
        String GradeString = _Type.ToString();
        GradeString = GradeString.Replace("_", " ");
        return GradeString;
    }

    private Color GetFishRarityColor(EFishGrade _FishGrade)
    {
        switch (_FishGrade)
        {
            case EFishGrade.COMMON:
            return Color.green;

            case EFishGrade.RARE:
            return Color.cyan;

            case EFishGrade.EXOTIC:
            return Color.magenta;

            default:break;
        }
        return Color.gray;
    }
}
