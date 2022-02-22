using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject playerShip;
    private AudioSource ShieldFX;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        ShieldFX = GetComponent<AudioSource>();
        ShieldFX.PlayOneShot(ShieldFX.clip);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2 (position.x, position.y + speed * Time.deltaTime);
        transform.position = position;
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
    if (col.tag == "EnemyShipTag" || col.tag == "EnemyBlastTag"){
        ShieldFX.PlayOneShot(ShieldFX.clip);
        Destroy(gameObject);
    }
    }
}
