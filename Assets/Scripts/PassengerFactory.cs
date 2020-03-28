using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerFactory : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();
    private bool IsSelected;

    private void Start()
    {
        IsSelected = true;
        InitializeSprite();
    }


public void InitializeSprite()
    {
        for (int i = 0; i < spriteList.Count; i++)
        {
            int spriteIndex = Random.Range(0, spriteList.Count);
            Sprite newSprite = spriteList[spriteIndex];
            SpriteRenderer spriteRendered = gameObject.GetComponent<SpriteRenderer>();
            spriteRendered.sprite = newSprite;
        }
        //PassengersList.Add(this.gameObject);

    }

    void Update()
    {
    }
}
