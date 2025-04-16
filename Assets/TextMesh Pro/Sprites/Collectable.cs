using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTrigerEnter2D (Collider2D collider)
    {
        Player player = GetComponent<Player>();
        if (player)
        {
            player.numSeed++;
            Destroy(this.gameObject);
        }
    }
   
}
