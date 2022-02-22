using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBlastGO;
    private AudioSource Blast;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireEnemyBlast", 1f);
        Blast = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBlast()
    {

        GameObject playerShip = GameObject.Find ("PlayerGO");
        Blast.PlayOneShot(Blast.clip);
        if (playerShip != null)
        {
            GameObject Blast = (GameObject)Instantiate(EnemyBlastGO);
            Blast.transform.position = transform.position;
            Vector2 direction = playerShip.transform.position - Blast.transform.position;
            Blast.GetComponent<EnemyBlast>().SetDirection(direction);
        }
    }
}
