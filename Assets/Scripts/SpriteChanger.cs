using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float switchInterval = 1f;

    private Image imageComponent;
    private float timer;
    private bool showSprite1 = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageComponent = GetComponent<Image>();
        if(imageComponent != null && sprite1 != null){
            imageComponent.sprite = sprite1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= switchInterval){
            timer = 0f;

            showSprite1 = !showSprite1;
            imageComponent.sprite = showSprite1 ? sprite1 : sprite2;
        }
    }
}
