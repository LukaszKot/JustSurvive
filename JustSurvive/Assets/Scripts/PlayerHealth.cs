using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private float _previousHealth = 100f;
    private Coroutine _healingCoroutine;
    private bool _isHealing = false;
    private void Start()
    {
        StartCoroutine(WaitForHealingCoroutine());
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("game over!!");
            Application.Quit();
        }
    }

    IEnumerator WaitForHealingCoroutine()
    {
        while (true)
        {
            if (_previousHealth - health == 0 && !_isHealing && health < 100)
            {
                _healingCoroutine = StartCoroutine(HealCoroutine());
                _isHealing = true;
            }
            else if(_previousHealth - health > 0 && _isHealing)
            {
                StopCoroutine(_healingCoroutine);
                _isHealing = false;
            }

            _previousHealth = health;
            yield return new WaitForSecondsRealtime(5);
        }
    }

    IEnumerator HealCoroutine()
    {
        while (health < 100)
        {
            health += 1;
            yield return new WaitForSecondsRealtime(1);
        }

        _isHealing = false;
        health = 100;
    }
}
