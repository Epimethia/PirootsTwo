using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum EDamageType {
    NONE,
    LEFT,
    RIGHT,
    PULL
}

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> {}

[System.Serializable]
public class FishingSpotEvent : UnityEvent<Fish> {}

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
    [SerializeField] 
    private float SpawnTimer = 0.0f;

    public float DespawnTime = 10.0f;
    [SerializeField] 
    private float DespawnTimer = 0.0f;

    [SerializeField] 
    private float TimeSinceActivation = 0.0f;

    public float MaxDamageDuration = 3.0f;
    private float CurrentDamageDuration = 0.0f;
    public float PullDamageValue = 10.0f;
    public float LeftDamageValue = 2.0f;
    public float RightDamageValue = 2.0f;

    private bool PlayerPresent = false;

    public GameObjectEvent DestructionEvent;
    public FishingSpotEvent FishingEventSuccessful;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        SpawnTimer = Random.Range(MinSpawnTime, MaxSpawnTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        Indicator.enabled = false;
        if (DestructionEvent == null)
        {
            DestructionEvent = new GameObjectEvent();
        }
        if(FishingEventSuccessful == null)
        {
            FishingEventSuccessful = new FishingSpotEvent();
        }
    }

    void Update()
    {
        
        if(spawnedFish != null) 
        {
            if(PlayerPresent == true && DespawnTimer != 0.0f)
            {
                DespawnTimer = 0.0f;
            }
            else if (PlayerPresent == false)
            {
                DespawnTimer += Time.deltaTime;
                if(DespawnTimer >= DespawnTime)
                {
                    DestroyFishingSpot();
                }
            }
        }

        else if(PlayerPresent == true)
        {
            if(TimeSinceActivation >= SpawnTimer)
            {
                //if the TimeSinceActivation is over the spawn time, spawn a fish.
                GameObject fishType = FishTypes[Random.Range(0, 7)];
                spawnedFish = GameObject.Instantiate(fishType, transform.position - new Vector3(0.0f, 1.5f, 0.0f), Quaternion.identity);
                Fish fishingScript = spawnedFish.GetComponent<Fish>();

                if(fishingScript) 
                {
                    fishingScript.m_ParentFishingSpot = this;
                }
                Indicator.enabled = true;

                //Break out of the coroutine and stop its execution
                return;
            }
            else
            {
                //Otherwise if the TimeSinceActivation is not over the spawn time yet, increment it 
                //by delta time
                TimeSinceActivation += Time.deltaTime;
            }
        }
        else
        {
            //Otherwise decrement the TimeSinceActivation back down to zero
            if (TimeSinceActivation > 0.0f)
            {
                TimeSinceActivation -= Time.deltaTime;
            }
            else
            {
                TimeSinceActivation = 0.0f;
                //break out of the coroutine since we dont want it to run anymore once it reaches zero
            }
        }
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
        
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.SetFloat("Vector1_E4BA77E7", 1.0f);
            PlayerPresent = true;

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
            PlayerPresent = false;
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

    void OnDestroy()
    {
        CameraFollow CameraFollowScript = Camera.main.GetComponent<CameraFollow>();
        CameraFollowScript.FollowObject = GameObject.Find("Player");
    }

    public void DestroyFishingSpot()
    {
        Destroy(spawnedFish);
        DestructionEvent.Invoke(this.gameObject);
    }
}
