using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteEverySec : MonoBehaviour
{
    private SpriteRenderer sr;
    private Image img;
    public Sprite[] sprites;
    public float spritesPerSec;
    private int currentSpriteIndex;
    private int increment = 1;
    public bool boomerang;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();

    }

    void Start()
    {
        InvokeRepeating("ChangeSprite", 0, 1 / spritesPerSec);
    }

    private void OnDisable()
    {
        CancelInvoke();
        currentSpriteIndex = 0;
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (sr != null)
        {
            sr.sprite = sprites[currentSpriteIndex];

        }
        else
        {
            img.sprite = sprites[currentSpriteIndex];
        }
        currentSpriteIndex+=increment;
        if (currentSpriteIndex == sprites.Length || currentSpriteIndex==-1)
        {
            if (boomerang)
            {
                increment *= -1;
                currentSpriteIndex += increment;
            }
            else
            {
                currentSpriteIndex = 0;
            }
            
        }
    }
}
