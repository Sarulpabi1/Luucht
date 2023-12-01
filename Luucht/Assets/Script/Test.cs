using UnityEngine;

public class ParticleBulletHell : MonoBehaviour
{
    public ParticleSystem bulletParticleSystem;
    public Transform target; // La cible vers laquelle les balles seront dirigées
    public float bulletSpeed = 5f;
    public float fireRate = 0.5f;
    public int numberOfBullets = 50;

    private float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time;
        InvokeRepeating("FireBulletPattern", 0f, fireRate);
    }

    void FireBulletPattern()
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = transform.position;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            emitParams.velocity = direction * bulletSpeed;
            bulletParticleSystem.Emit(emitParams, 1);
        }
    }
}