﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


  private Rigidbody2D myRigidbody2D;

  public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }

    // Update is called once per frame
    private void Fire()
    {
      myRigidbody2D.linearVelocity = Vector2.down * speed; 
      Debug.Log("Wwweeeeee");
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Barricade")){
            Barricade barricade = collision.gameObject.GetComponent<Barricade>();
            if(barricade != null){
                barricade.TakeDamage();
            }
        }
        Destroy(gameObject);
    }

}
