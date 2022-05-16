using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int health = 1;
    public static bool hasDied = false;

    public GameOver go = new GameOver();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasDied == false)
        {
            health--;
            if (health <= 0)
            {
                hasDied = true;
            }
            if (hasDied == true)
            {
                go.Toggle();
            }
        }
    }
}