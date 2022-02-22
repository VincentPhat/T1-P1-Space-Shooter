using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCONTROL : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameManagerGO;
    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;
    public GameObject PlayerBulletGO;
    public GameObject BulletPos01;
    public GameObject BulletPos02;
    public GameObject PlayerBlastGO;
    public GameObject BlastPos;
    public GameObject ExplosionGO;
    public GameObject PlayerShieldGO;
    public GameObject ShieldPos;
    public float speed; 
    private AudioSource Laser;
    public float cdTime = 1f;
    public float ShieldcdTime = 10f;
    private float nextInputTime = 0f;
    private float nextShieldInputTime = 0f;
    void Start()
    {
        Laser = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextInputTime){
            if(Input.GetKeyDown("space")){
                Laser.PlayOneShot(Laser.clip);
                GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
                bullet01.transform.position = BulletPos01.transform.position;
                GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
                bullet02.transform.position = BulletPos02.transform.position;
                nextInputTime = Time.time + cdTime;
            }
        }
        if(Input.GetKeyDown("z")){
            GameObject blast = (GameObject)Instantiate (PlayerBlastGO);
            blast.transform.position = BlastPos.transform.position;
        }
        if (Time.time > nextShieldInputTime){
            if(Input.GetKeyDown("x")){
                GameObject shield = (GameObject)Instantiate (PlayerShieldGO);
                shield.transform.position = ShieldPos.transform.position;
                nextShieldInputTime = Time.time + ShieldcdTime;
            }
        }

        float x = Input.GetAxisRaw ("Horizontal");
        float y = Input.GetAxisRaw ("Vertical");
        Vector2 direction = new Vector2 (x, y).normalized;

        Move (direction);
    }

    void Move(Vector2 direction){
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1,1));

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp (pos.x, min.x, max.x);
        pos.y = Mathf.Clamp (pos.y, min.y, max.y);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
    if (col.tag == "EnemyShipTag" || col.tag == "EnemyBlastTag"){
        Explosion();
        lives--;
        LivesUIText.text = lives.ToString();
        if(lives == 0){
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
        gameObject.SetActive(false);
        }
    }
        
    }
    void Explosion()
    {
    GameObject explosion = (GameObject)Instantiate (ExplosionGO);
    explosion.transform.position = transform.position;
    }

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        gameObject.SetActive(true);
        transform.position = new Vector2(0,0);
    }
}
