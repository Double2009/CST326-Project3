using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MysteryEnemy : Enemy
{
    public float speed = 5f;
    public float offscreenBuffer = 1f; // How far offscreen before wrapping
    public AudioClip mysteryExplosionClip;
    private bool movingRight = true;

    void Start()
    {
        // Optionally, set starting position on the left side.
        Vector2 screenBounds = GetScreenBounds();
        transform.position = new Vector3(-screenBounds.x - offscreenBuffer, transform.position.y, transform.position.z);
        movingRight = true;
    }

    void Update()
    {

        transform.position += (movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime;

        Vector2 screenBounds = GetScreenBounds();

        if(movingRight && transform.position.x > screenBounds.x + offscreenBuffer){
            transform.position = new Vector3(-screenBounds.x - offscreenBuffer, transform.position.y, transform.position.z);
        }
        else if (!movingRight && transform.position.x < -screenBounds.x - offscreenBuffer){
            transform.position = new Vector3(screenBounds.x + offscreenBuffer, transform.position.y, transform.position.z);
        }
    }

    Vector2 GetScreenBounds(){
        
        Camera cam = Camera.main;
        if(cam != null){
            Vector3 bounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
            return new Vector2(bounds.x, bounds.y);
        }
        return new Vector2(8f, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet")){
            Destroy(collision.gameObject);

            if(mysteryExplosionClip != null){
                AudioSource audioSrc = GetComponent<AudioSource>();
                audioSrc.clip = mysteryExplosionClip;
                AudioSource.PlayClipAtPoint(mysteryExplosionClip, transform.position, 1.0f);
            }

            int increments = Random.Range(0, 21);
            int points = 100 + increments * 10;

            GameManager.Instance.AddScore(points);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if(Application.isPlaying && MysteryEnemySpawner.Instance != null){
            MysteryEnemySpawner.Instance.SpawnMysteryEnemyAfterDelay(5f);
        }
    }
}


