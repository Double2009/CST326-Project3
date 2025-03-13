using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class DemoButton : MonoBehaviour
{

    public GameObject someReference;

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    public void LoadCredits(){
        SceneManager.LoadScene("CreditsScene");
    }

   public void LoadGameScene(){
        
        StartCoroutine(_LoadGameScene());

        IEnumerator _LoadGameScene(){
            AsyncOperation loadOp = SceneManager.LoadSceneAsync("GameScene");

           // yield return new WaitUntil(() => loadOp.isDone);

            while(!loadOp.isDone) yield return null;

            GameObject player = GameObject.Find("Player");
            Debug.Log(player.name);
        }
   }
}
