using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public float speed = 5f;
  public GameObject bulletPrefab;
  public Transform shottingOffset;
  public AudioClip fireClip;
  public AudioClip playerExplosionClip;


  Animator playerAnimator;

    void Start(){
      Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
      playerAnimator = GetComponent<Animator>();
    }


    void EnemyOnOnEnemyDied(int points){
      Debug.Log($"I know about dead enemy, points: {points}");
    }

    void OnDestroy(){
      Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {

        playerAnimator.SetTrigger("Shoot Trigger");

        AudioSource audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = fireClip;
        audioSrc.Play();

        GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);

      }
      
      float horizontalInput = Input.GetAxis("Horizontal");
      Vector3 movement = new Vector3(horizontalInput * speed * Time.deltaTime, 0f, 0f);
      transform.position += movement;
    }

    void OnCollisionEnter2D(Collision2D collision){
      if(collision.gameObject.CompareTag("Top Enemy") || collision.gameObject.CompareTag("EnemyBullet")
         || collision.gameObject.CompareTag("Middle Enemy") || collision.gameObject.CompareTag("Bottom Enemy")){
        
          playerAnimator.SetTrigger("Explosion Trigger");
          
          AudioSource audioSrc = GetComponent<AudioSource>();
          audioSrc.clip = playerExplosionClip;
          AudioSource.PlayClipAtPoint(playerExplosionClip, transform.position, 1.0f);

          StartCoroutine(LoadCreditsAfterDelay(playerExplosionClip.length));
      }
    }

    IEnumerator LoadCreditsAfterDelay(float delay){
        
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        SceneManager.LoadScene("CreditsScene");
    }
}
