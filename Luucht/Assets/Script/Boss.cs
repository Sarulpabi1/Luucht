using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float totalTime = 120f; // Temps total que le boss reste sur le terrain en secondes
    public GameObject[] attackPatterns; // Tableau contenant les scripts d'attaque du boss

    private bool isAttacking = false;

    private void Start()
    {
        StartCoroutine(DespawnAfterTime());
        StartCoroutine(PerformRandomAttacks());
    }

    IEnumerator PerformRandomAttacks()
    {
        while (true)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                int randomIndex = Random.Range(0, attackPatterns.Length);
                ActivatePattern(randomIndex);

                float attackDuration = Random.Range(5f, 10f); // Durée aléatoire pour chaque pattern d'attaque
                yield return new WaitForSeconds(attackDuration);

                DeactivateAllPatterns();
                isAttacking = false;
            }
            yield return null;
        }
    }

    void ActivatePattern(int index)
    {
        DeactivateAllPatterns();

        if (index >= 0 && index < attackPatterns.Length)
        {
            attackPatterns[index].SetActive(true);
        }
    }

    void DeactivateAllPatterns()
    {
        foreach (var pattern in attackPatterns)
        {
            pattern.SetActive(false);
        }
    }

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(totalTime);
        // Code pour désactiver ou détruire le boss après le temps spécifié
        gameObject.SetActive(false); // Ou gameObject.Destroy(gameObject);
    }
}
