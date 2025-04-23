using UnityEngine;

public class SwapSpriteRandom : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteList;
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    float timer;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            ChangeSprite();
    }
    private void ChangeSprite()
    { 
        timer = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        int randIndex = Random.Range(0, spriteList.Length);
        spriteRenderer.sprite = spriteList[randIndex];
    }

}
