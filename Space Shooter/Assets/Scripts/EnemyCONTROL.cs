using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCONTROL : MonoBehaviour
{
    GameObject scoreUITextGO;
    public GameObject EnemyExplosionGO;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
        if (transform.position.y < min.y){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerBulletTag" || col.tag == "PlayerShipTag" || col.tag == "PlayerShieldTag")
        {
            Explosion();
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }
    void Explosion()
    {
    GameObject explosion = (GameObject)Instantiate (EnemyExplosionGO);
    explosion.transform.position = transform.position;
    }
}
