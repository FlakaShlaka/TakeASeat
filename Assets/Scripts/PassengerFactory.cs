using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerFactory : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();

    private void Start()
    {
        InitializeSprite();
    }


public void InitializeSprite()
    {
        for (int i = 0; i < spriteList.Count; i++)
        {
            int spriteIndex = UnityEngine.Random.Range(0, spriteList.Count);
            Sprite newSprite = spriteList[spriteIndex];
            SpriteRenderer spriteRendered = gameObject.GetComponent<SpriteRenderer>();
            spriteRendered.sprite = newSprite;
        }

    }

    void Update()
    {

    }
}
