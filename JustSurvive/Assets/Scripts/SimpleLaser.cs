using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SimpleLaser : MonoBehaviour
{
    public GameObject laserPrefab;

    public GameObject firePoint;

    public Camera fpsCam;

    public float range = 10f;

    public GameObject colisionPointPrefab;

    private GameObject colisionPointInstance;
    public AudioSource beamSound;
    public AudioSource collisionPointSound;
    

    private GameObject spawnedLaser;
    // Start is called before the first frame update
    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        DisableLaser();
        collisionPointSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }
    }

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
        beamSound.Play();
        Shoot();
    }

    void UpdateLaser()
    {
        if (firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }
        Shoot();
    }

    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
        beamSound.Stop();
        Destroy(colisionPointInstance, 2.0f);
        collisionPointSound.Stop();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && hit.transform.CompareTag("world"))
        {
            if (colisionPointInstance == null)
            {
                Debug.Log(hit.transform.name);
                colisionPointInstance = Instantiate(colisionPointPrefab, hit.point, Quaternion.identity) as GameObject;
                if(!collisionPointSound.isPlaying)
                    collisionPointSound.Play();
            }
            else
            {
                colisionPointInstance.transform.position = hit.point;
            }
        }
        else
        {
            Destroy(colisionPointInstance, 2.0f);
            collisionPointSound.Stop();
        }
    }
    
}
