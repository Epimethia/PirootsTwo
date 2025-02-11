﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EFishGrade 
{
    COMMON,
    RARE,
    EXOTIC
}

public enum EFishType
{
    BLUEFISH = 0,
    COD,
    STRIPED_BASS,
    SWORDFISH,
    TARPON,
    TUNA,
    YELLOWTAIL
}

public class Fish : MonoBehaviour
{

    public FishingSpot m_ParentFishingSpot;
    private Animator m_Animator;
    public Image HealthBar;
    public GameObject m_ParticleSystemPrefab;
    public EFishGrade m_Grade = EFishGrade.COMMON;
    public float m_Health = 100.0f;
    public float m_MaxHealth = 100.0f;
    public float m_MinWeight = 0.0f;
    public float m_MaxWeight = 0.0f;
    public float m_Weight = 0.0f;
    public float m_MaxScore = 0;

    public EFishType m_FishType;
    
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        int State = m_ParentFishingSpot.RandomNextState();
        m_Animator.SetInteger("CurrentState", State);
        Debug.Log("Current State:" + State);

        m_Weight = Random.Range(m_MinWeight, m_MaxWeight);
        GenerateGrade();

        m_ParticleSystemPrefab.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishedState() 
    {
        Debug.Log("I did the thing");
        int State = m_ParentFishingSpot.RandomNextState();
        m_Animator.SetInteger("CurrentState", State);
    }

    private void GenerateGrade() 
    {
        float fWeightRatio = m_Weight / m_MaxWeight;
        if (fWeightRatio <= 0.33f) 
        {
            m_Grade = EFishGrade.COMMON;
        }
        else if (fWeightRatio >= 0.66f)
        {
            m_Grade = EFishGrade.EXOTIC;
        }
        else 
        {
            m_Grade = EFishGrade.RARE;
        }
    }

    public void TakeDamage(float _Damage) 
    {
        m_Health -= _Damage;

        if (m_Health < 0.0f) 
        {
            Die();
            return;
        }

        HealthBar.fillAmount = m_Health / m_MaxHealth;
    }

    public void Die() 
    {
        m_ParentFishingSpot.DestroyFishingSpot();
    }

    public int GenerateScore()
    {
        int Score = 0;
        Score = Mathf.FloorToInt(m_MaxScore * (m_Weight / m_MaxWeight));
        return Score;
    }
}
