using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
  public float speed = 5f;
  public GameObject bulletPrefab;
  public Transform shottingOffset;

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

        GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");
      }
      
      float horizontalInput = Input.GetAxis("Horizontal");
      Vector3 movement = new Vector3(horizontalInput * speed * Time.deltaTime, 0f, 0f);
      transform.position += movement;
    }
}
