using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance;

    [Header("Movement Settings")]
    public float baseSpeed = 1f; 
    public float moveDownAmount = 0.5f;
    public float leftBoundary = -40f;
    public float rightBoundary = 40f;

    [Header("Firing Settings")]
    public float fireInterval = 3f; 

    private List<Enemy> enemies = new List<Enemy>();
    private int initialEnemyCount;
    private float currentSpeed;
    private int horizontalDirection = 1;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        
        foreach (Transform child in transform){
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null){
                enemies.Add(enemy);
            }
        }
        initialEnemyCount = enemies.Count;
        currentSpeed = baseSpeed;
        StartCoroutine(EnemyFireRoutine());
    }

    // Update is called once per frame
    void Update(){

     bool needToChangeDirection = false;


     foreach(Enemy enemy in enemies){
        if(enemy != null){
            if((horizontalDirection == 1 && enemy.transform.position.x > rightBoundary) ||
                (horizontalDirection == -1 && enemy.transform.position.x < leftBoundary)){
                needToChangeDirection = true;
                break;
            }
        }
     }

     if(needToChangeDirection){
        horizontalDirection *= -1;
        foreach(Enemy enemy in enemies){
            if(enemy != null){
                enemy.transform.position += Vector3.down * moveDownAmount;
            }
        }
     }
        
     else{
        foreach(Enemy enemy in enemies){
            if(enemy != null){
                enemy.transform.position += Vector3.right * currentSpeed * horizontalDirection * Time.deltaTime;
            }
        }
     }
        
    }

    public void RemoveEnemy(Enemy enemy){
        if(enemies.Contains(enemy)){
            enemies.Remove(enemy);
            UpdateSpeed();
        }
    }

    void UpdateSpeed(){
        int remaining = enemies.Count;
        if(remaining > 0){
            currentSpeed = baseSpeed * ((float)initialEnemyCount / remaining);
        }
    }

    IEnumerator EnemyFireRoutine(){
        while(true){
            Enemy[] allEnemies = FindObjectsOfType<Enemy>();
            if(allEnemies.Length > 0){
                int index = Random.Range(0, allEnemies.Length);
                Enemy firingEnemy = allEnemies[index];
                if(firingEnemy != null){
                    firingEnemy.FireBullet();
                }
            }
        }
    }
}
