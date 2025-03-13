using UnityEngine;
using System.Collections;

public class MysteryEnemySpawner : MonoBehaviour
{
    public static MysteryEnemySpawner Instance;
    public GameObject mysteryEnemyPrefab;

    void Awake(){
        
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void SpawnMysteryEnemyAfterDelay(float delay){
        
        if(gameObject.activeInHierarchy){
            StartCoroutine(SpawnAfterDelay(delay));
        }

        else{
            Debug.LogWarning("Mystery Spawner inactive. Coroutine not started");
        }
    }

    private IEnumerator SpawnAfterDelay(float delay){
        
        yield return new WaitForSeconds(delay);

        // Optionally, check if there isn't already a MysteryEnemy in the scene.
        if(GameObject.FindObjectOfType<MysteryEnemy>() == null && mysteryEnemyPrefab != null){
            
            Instantiate(mysteryEnemyPrefab);
        }
    }
}

