using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("game over!!");
            Application.Quit();
        }
    }
}
