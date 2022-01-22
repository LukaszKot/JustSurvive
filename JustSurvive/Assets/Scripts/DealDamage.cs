using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform player;
    public float dmgTick = 1f;
    public float timeTick = 1f;
    public float damageRange = 0.5f;

    private bool doingDamage = false;

    IEnumerator DoDamage()
    {
        while (true)
        {
            Debug.Log("next loop");
            playerHealth.health -= dmgTick;
            dmgTick *= 2;
            yield return new WaitForSeconds(timeTick);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= damageRange && !doingDamage)
        {
            StartCoroutine(DoDamage());
            doingDamage = !doingDamage;
        }
        else if(distance > damageRange && doingDamage)
        {
            StopAllCoroutines();
            doingDamage = !doingDamage;
            dmgTick = 1;
        }
    }
}
