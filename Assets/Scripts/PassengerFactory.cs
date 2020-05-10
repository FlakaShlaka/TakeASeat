using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerFactory : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();

    public List<Sprite> AngryspriteList = new List<Sprite>();

    public Sprite angry;
    public Sprite newSprite;
    private void Start()
    {
        InitializeSprite();

    }


public void InitializeSprite()
    {
        GameObject controller = GameObject.Find("Controller");
        int tempIndex = controller.GetComponent<gameController>().lastIndex;
        //for (int i = 0; i < spriteList.Count; i++)
        //{
            int spriteIndex = UnityEngine.Random.Range(0, spriteList.Count);
            if (spriteIndex >= tempIndex)
            {
                newSprite = spriteList[spriteIndex];
                SpriteRenderer spriteRendered = gameObject.GetComponent<SpriteRenderer>();
                spriteRendered.sprite = newSprite;
                angry = AngryspriteList[spriteIndex];
                controller.GetComponent<gameController>().lastIndex = spriteIndex;
            }
            else if (spriteIndex <= tempIndex)
            {
            newSprite = spriteList[spriteIndex];
            SpriteRenderer spriteRendered = gameObject.GetComponent<SpriteRenderer>();
            spriteRendered.sprite = newSprite;
            angry = AngryspriteList[spriteIndex];
            controller.GetComponent<gameController>().lastIndex = spriteIndex;
            }

        //}

    }

    void Update()
    {

    }
}
