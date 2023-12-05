using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public float currentHealth, maxHealth = 3f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void entityDestroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void takeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            entityDestroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Invoke("QuitGame", 3f);
        }
    }


}
