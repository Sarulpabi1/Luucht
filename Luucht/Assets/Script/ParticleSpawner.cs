using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Timeline;

[System.Serializable]
public class AttackParameters
{
    public int numberOfColumns;
    public float speed;
    public float lifetime;
    public float fireRate;
    public float size;
    public float spinSpeed;
}



public class ParticleSpawner : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] public int numberOfColumns;
    [SerializeField] public float speed;
    [SerializeField] public Sprite texture;
    [SerializeField] public Color color;
    [SerializeField] public float lifetime;
    [SerializeField] public float fireRate;
    [SerializeField] public float size;
    [SerializeField] public Material material;
    [SerializeField] public float spinSpeed;
    private float time;

    public AttackParameters attack1Params;
    public AttackParameters attack2Params;
    public AttackParameters attack3Params;


    public ParticleSystem system;
    

    private float angle;

    private void Awake()
    {
        
        

    }


    private void FixedUpdate()
    {

        time += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, time * spinSpeed);
    }

    
    void Summon()
    {
        
        angle = 360f / numberOfColumns;
        for (int i = 0; i < numberOfColumns; i++)
        {
            Material particleMaterial = material;

            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * i, 90, 0);
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            system = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;
            mainModule.startSpeed = speed;
            mainModule.maxParticles = 1000000;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;

            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

            var emission = system.emission;
            emission.enabled = false;

            var shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            shape.sprite = null;

            var text = system.textureSheetAnimation;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(texture);
            text.enabled = true;
        }
        InvokeRepeating("DoEmit", 0f, fireRate);
        
    }

    
    void DoEmit()
    {
        
        foreach (Transform child in transform)
        {
            system = child.GetComponent<ParticleSystem>();
          
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = size;
            emitParams.startLifetime = lifetime;
            system.Emit(emitParams, 10);
        }
    }

    public void ResetValues()
    {
        // Réinitialisation des valeurs par défaut
        numberOfColumns = 0;
        speed = 0f;
        lifetime = 0f;
        fireRate = 0f;
        size = 0f;
        spinSpeed = 0f;
    }

    private void SetAttackParameters(AttackParameters parameters)
    {
        numberOfColumns = parameters.numberOfColumns;
        speed = parameters.speed;
        lifetime = parameters.lifetime;
        fireRate = parameters.fireRate;
        size = parameters.size;
        spinSpeed = parameters.spinSpeed;

        angle = 360f / numberOfColumns;
    }
    public void Attack1()
    {

        AttackParameters attack1Params = new AttackParameters
        {
            numberOfColumns = 16,
            speed = 5f,
            lifetime = 5f,
            fireRate = 0.3f,
            size = 1f,
            spinSpeed = 10f
        };
        SetAttackParameters(attack1Params);

    }

    public void Attack2()
    {

        AttackParameters attack2Params = new AttackParameters
        {
            numberOfColumns = 16,
            speed = 8f,
            lifetime = 5f,
            fireRate = 0.5f,
            size = 1f,
            spinSpeed = 250f
        };
        SetAttackParameters(attack2Params);

    }

    public void Attack3()
    {

        AttackParameters attack3Params = new AttackParameters
        {
            numberOfColumns = 24,
            speed = 5f,
            lifetime = 5f,
            fireRate = 1f,
            size = 1f,
            spinSpeed = 250f
        };
        SetAttackParameters(attack3Params);

    }

    void DestroyChildren()
    {
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            
        }
    }

    

    public void RandomAttackPattern()
    {
        DestroyChildren();
        int patternIndex = Random.Range(1, 3);
        switch (patternIndex)
        {
            case 1:
                Attack1();
                Summon();
                break;
        
            case 2: 
                Attack2();
                Summon();
                break;

            case 3: 
                Attack3();
                Summon();
                break;
            default:
                Debug.Log("NoPattern");
                break;
        }
        
    }
}

 

