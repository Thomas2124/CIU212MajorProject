﻿using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    public Texture2D[] levelSprites;

    public TestPlatformJoin joinScript;

    public ReplaceSprites spriteScript;

    public bool isDone = false;

    public int highestNum = 0;

    public int longestNum = 0;

    // Start is called before the first frame update
    void Awake()
    {
        map = levelSprites[PlayerPrefs.GetInt("Level")];
        Instance = this;
        highestNum = map.height;
        longestNum = map.width;
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }

        //joinScript.enabled = true;
        spriteScript.enabled = true;
        isDone = true;
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            return;
        }

        //print(x.ToString() + ", " + y.ToString());
        
        foreach (ColorToPrefab item in colorMappings)
        {
            if (item.color.Equals(pixelColor))
            {
                Vector3 pos = new Vector3(x, y, 0f);
                Instantiate(item.prefab, pos, Quaternion.identity, this.gameObject.transform);
            }
        }
    }
}
