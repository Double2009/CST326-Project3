using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;

    public GameObject enemyBulletPrefab;


    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log("Ouch!");
      Destroy(collision.gameObject);

      int points = 0;
      if(CompareTag("Top Enemy")){
        points = 30;
      }
      else if(CompareTag("Middle Enemy")){
        points = 20;
      }
      else if(CompareTag("Bottom Enemy")){
        points = 10;
      }

      OnEnemyDied?.Invoke(points);

      GameManager.Instance.AddScore(points);

      //todo kill enemy
      Destroy(gameObject);
    }

    public void FireBullet(){
      if(enemyBulletPrefab != null){
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
      }
    }
}
