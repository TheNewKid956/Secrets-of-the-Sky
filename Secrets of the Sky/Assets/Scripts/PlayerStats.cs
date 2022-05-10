using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int health = 1;
    public static bool canMove = true;
    public static bool canRotate = true;
    public static bool hasDied = false;
    public static int timesChecked = 0;

    public GameOver go = new GameOver();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
            print(health);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            canMove = false;
            canRotate = false;
            hasDied = true;
        }
        if(hasDied == true && timesChecked <= 0)
        {
            timesChecked++;
            go.Toggle();
        }
    }
}