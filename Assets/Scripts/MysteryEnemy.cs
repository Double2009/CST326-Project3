using UnityEngine;
using System.Collections;

public class MysteryEnemy : MonoBehaviour
{

    public delegate void MysteryEnemyDied(int points);
    public static event MysteryEnemyDied OnMysteryEnemyDied;

    public float speed = 5f;
    public float waitTime = 3f;
    
    private bool movingRight = true;
    public float leftBoundary = -50f;
    public float rightBoundary = 50f;

    void Update(){

        if(movingRight){
            transform.position += Vector3.right * speed * Time.deltaTime;
            if(transform.position.x >= rightBoundary){
                movingRight = false;
            }
        }
        else{
            transform.position += Vector3.left * speed * Time.deltaTime;
            if(transform.position.x <= leftBoundary){
                movingRight = true;
            }
        }
    }

    // Use triggers for fast-moving bullets.
    void OnTriggerEnter2D(Collider2D collision){

        Destroy(collision.gameObject);

        int increments = Random.Range(0, 21); 
        int points = 100 + increments * 10;
        GameManager.Instance.AddScore(points);
        Destroy(gameObject);
    }
}
