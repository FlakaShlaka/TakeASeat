using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatFactory : MonoBehaviour
{
    public enum SeatClass { firstclass, business, thisisus};
    public SeatClass seatClass = SeatClass.firstclass;

    public List<Sprite> spriteList = new List<Sprite>();
   
    private void Start()
    {
        InitializeSprite();
    }

    public void InitializeSprite()
    {
        int spriteIndex = 0;
        switch (seatClass)
        {
            case SeatClass.firstclass: 
                spriteIndex = 0;
                break;

            case SeatClass.business:
                spriteIndex = 1;
                break;

            case SeatClass.thisisus:
                spriteIndex = 2;
                break;
        }
        Sprite newSprite = spriteList[spriteIndex];
        Debug.Log(spriteIndex);

        SpriteRenderer spriteRendered = gameObject.GetComponent<SpriteRenderer>();
        spriteRendered.sprite = newSprite;
    }
}
