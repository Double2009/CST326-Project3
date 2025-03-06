using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    private Rigidbody2D myRigidbody2D;
    public float speed = 5f;


    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }

    private void Fire()
    {
      myRigidbody2D.linearVelocity = Vector2.down * speed; 
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
