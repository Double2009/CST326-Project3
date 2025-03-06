using UnityEngine;

public class Barricade : MonoBehaviour
{
    public int health = 3;


    public void TakeDamage(){
        health--;
        if(health <= 0){
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision){

            TakeDamage();
            Destroy(collision.gameObject);
        
    }

}
