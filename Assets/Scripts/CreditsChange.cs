using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsChange : MonoBehaviour
{

    public float creditsDuration = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("LoadMenuScene", creditsDuration);
    }

    // Update is called once per frame
    void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
