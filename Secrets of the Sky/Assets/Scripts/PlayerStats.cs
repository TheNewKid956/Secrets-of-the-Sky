using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int health = 1;
    public static bool canMove = true;
    public static bool canRotate = true;

    void Update()
    {
        if (health <= 0)
        {
            canMove = false;
            canRotate = false;
            StartCoroutine(Death());
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        health = 5;
        SceneManager.LoadScene(0);
        canMove = true;
        canRotate = true;
    }
}