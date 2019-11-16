using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBlockSprites : MonoBehaviour
{
    public static myBlockSprites instance;
    [Header("World Set")]
    public Sprite[] oneSpritesSet;
    public Sprite[] twoSpritesSet;
    public Sprite[] threeSpritesSet;
    public Sprite fourSpriteSet;
    public Sprite otherSpriteSet;
    public Sprite cornerSpriteSet;

    [Header("World 1")]
    public Sprite[] oneSprites1;
    public Sprite[] twoSprites1;
    public Sprite[] threeSprites1;
    public Sprite fourSprite1;
    public Sprite otherSprite1;
    public Sprite cornerSpriteSet1;

    [Header("World 2")]
    public Sprite[] oneSprites2;
    public Sprite[] twoSprites2;
    public Sprite[] threeSprites2;
    public Sprite fourSprite2;
    public Sprite otherSprite2;
    public Sprite cornerSpriteSet2;

    [Header("World 3")]
    public Sprite[] oneSprites3;
    public Sprite[] twoSprites3;
    public Sprite[] threeSprites3;
    public Sprite fourSprite3;
    public Sprite otherSprite3;
    public Sprite cornerSpriteSet3;

    [Header("World 4")]
    public Sprite[] oneSprites4;
    public Sprite[] twoSprites4;
    public Sprite[] threeSprites4;
    public Sprite fourSprite4;
    public Sprite otherSprite4;
    public Sprite cornerSpriteSet4;

    void Awake()
    {
        instance = this;

        int currentLevel = PlayerPrefs.GetInt("Level");

        if (currentLevel < 4)
        {
            oneSpritesSet = oneSprites1;
            twoSpritesSet = twoSprites1;
            threeSpritesSet = threeSprites1;
            fourSpriteSet = fourSprite1;
            otherSpriteSet = otherSprite1;
            cornerSpriteSet = cornerSpriteSet1;
        }
        else if (currentLevel >= 4 && currentLevel < 8)
        {
            oneSpritesSet = oneSprites2;
            twoSpritesSet = twoSprites2;
            threeSpritesSet = threeSprites2;
            fourSpriteSet = fourSprite2;
            otherSpriteSet = otherSprite2;
            cornerSpriteSet = cornerSpriteSet2;
        }
        else if (currentLevel >= 8 && currentLevel < 12)
        {
            oneSpritesSet = oneSprites3;
            twoSpritesSet = twoSprites3;
            threeSpritesSet = threeSprites3;
            fourSpriteSet = fourSprite3;
            otherSpriteSet = otherSprite3;
            cornerSpriteSet = cornerSpriteSet3;
        }
        else if (currentLevel >= 12)
        {
            oneSpritesSet = oneSprites4;
            twoSpritesSet = twoSprites4;
            threeSpritesSet = threeSprites4;
            fourSpriteSet = fourSprite4;
            otherSpriteSet = otherSprite4;
            cornerSpriteSet = cornerSpriteSet4;
        }
    }
}
