using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EDamageType {
    NONE,
    LEFT,
    RIGHT,
    PULL
}

public class FishingSpot : MonoBehaviour
{

    private MeshRenderer rend;

    [Header("Fish")]
    public List<GameObject> FishTypes; 
    private GameObject spawnedFish;
    EDamageType m_FishingSpotDamageType = EDamageType.NONE;
    EDamageType m_PlayerDamageType = EDamageType.NONE;

    [Header("Indicator Images")]
    public Sprite LeftImage;
    public Sprite RightImage;
    public Sprite PullImage;

    public SpriteRenderer Indicator;
    
    public float MinSpawnTime = 5.0f;
    public float  MaxSpawnTime = 10.0f;
    private float SpawnTimer = 0.0f;

    private float DespawnTime = 0.0f;
    private float DespawnTimer = 0.0f;

    public float MaxDamageDuration = 3.0f;
    private float CurrentDamageDuration = 0.0f;
    public float PullDamageValue = 10.0f;
    public float LeftDamageValue = 2.0f;
    public float RightDamageValue = 2.0f;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        SpawnTimer = Random.Range(MinSpawnTime, MaxSpawnTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        Indicator.enabled = false;
    }

    void Update()
    {

    }

    void FixedUpdate() 
    {
        if(CurrentDamageDuration > 0.0f) 
        {
            CurrentDamageDuration = Mathf.Max(CurrentDamageDuration - (0.5f * Time.fixedDeltaTime), 0.0f);
            DamageFish(m_FishingSpotDamageType, PullDamageValue * Time.fixedDeltaTime);
        }
        else if(m_PlayerDamageType != EDamageType.NONE)
        {
            m_PlayerDamageType = EDamageType.NONE;
        }
    }

    IEnumerator SpawnFish()
    {
        yield return new WaitForSecondsRealtime(SpawnTimer);

        GameObject fishType = FishTypes[Random.Range(0, 7)];
        spawnedFish = GameObject.Instantiate(fishType, transform.position - new Vector3(0.0f, 1.5f, 0.0f), Quaternion.identity);
        Fish fishingScript = spawnedFish.GetComponent<Fish>();

        if(fishingScript) 
        {
            fishingScript.m_ParentFishingSpot = this;
        }
        Indicator.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.SetFloat("Vector1_E4BA77E7", 1.0f);

            if (!spawnedFish) 
            {
                StartCoroutine(SpawnFish());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.SetFloat("Vector1_E4BA77E7", 0.0f);
        }
    }

    public int RandomNextState() 
    {
        int State = Random.Range(0, 3);
        switch(State) {
            case 0:
            m_FishingSpotDamageType = EDamageType.LEFT;
            Indicator.sprite = LeftImage;
            break;

            case 1:
            m_FishingSpotDamageType = EDamageType.PULL;
            Indicator.sprite = PullImage;
            break;

            case 2:
            m_FishingSpotDamageType = EDamageType.RIGHT;
            Indicator.sprite = RightImage;
            break;

            default: 
            m_FishingSpotDamageType = EDamageType.NONE;
            break;
        }
        CheckParticleSystem();
        return State;
    }

    public void DamageFish(EDamageType _DamageType, float _Damage) 
    {
        Fish FishScript = spawnedFish.GetComponent<Fish>();
        if(m_FishingSpotDamageType == m_PlayerDamageType && spawnedFish) 
        {
            FishScript.TakeDamage(_Damage);
        }
    }

    public void SwapDamageType(EDamageType _DamageType)
    {
        CurrentDamageDuration = MaxDamageDuration;
        m_PlayerDamageType = _DamageType;

        CheckParticleSystem();
    }

    void CheckParticleSystem() 
    {
        ParticleSystem FishPS = spawnedFish.GetComponent<Fish>().m_ParticleSystemPrefab.GetComponent<ParticleSystem>();
        if(m_FishingSpotDamageType == m_PlayerDamageType && spawnedFish) 
        {
            FishPS.Play();
        }
        else {
            FishPS.Stop();
        }
    }
}
