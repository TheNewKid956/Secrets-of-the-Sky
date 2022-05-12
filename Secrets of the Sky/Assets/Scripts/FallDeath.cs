using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
      PlayerStats.health -= 1;
    }
}
