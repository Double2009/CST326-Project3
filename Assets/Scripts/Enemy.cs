using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;

    public GameObject enemyBulletPrefab;
    public AudioClip enemyFireClip;
    public AudioClip enemyExplosionClip;

    public float explosionDuration = 1.5f;

    Animator enemyAnimator;

    void Start(){
      enemyAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.CompareTag("PlayerBullet")){

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

        enemyAnimator.SetTrigger("Enemy Explosion Trigger");
        
        if(enemyExplosionClip != null){
          AudioSource audioSrc = GetComponent<AudioSource>();
          audioSrc.clip = enemyExplosionClip;
          AudioSource.PlayClipAtPoint(enemyExplosionClip, transform.position, 1.0f);
        }

        if(EnemyManager.Instance != null){
            EnemyManager.Instance.RemoveEnemy(this);
        }

        //todo kill enemy
        StartCoroutine(WaitForDestroy());
      }
    }

    public void FireBullet(){
      if(enemyBulletPrefab != null){

        enemyAnimator.SetTrigger("Enemy Shoot Trigger");
        
        AudioSource audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = enemyFireClip;
        audioSrc.volume = 0.1f;
        audioSrc.Play();

        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
      }   
      else{
        Debug.LogWarning("EnemyBullet prefab is not assigned!");
      }
    }

        IEnumerator WaitForDestroy(){
        
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
        
    }
}
