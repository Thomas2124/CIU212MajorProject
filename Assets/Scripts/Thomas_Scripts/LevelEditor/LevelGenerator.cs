using UnityEngine;

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

    public GameObject backGround;

    // Start is called before the first frame update
    void Awake()
    {
        map = levelSprites[PlayerPrefs.GetInt("Level")];
        Instance = this;
        highestNum = map.height;
        longestNum = map.width;
        GenerateLevel();
    }

    // Checks every pixel on image.
    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }

        spriteScript.enabled = true;
        isDone = true;
    }

    // Spawns a tile based on pixel colour.
    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        Vector3 pos = new Vector3(x, y, 0f);

        if (pixelColor.a == 0) // Spawns a background sprite.
        {
            Instantiate(backGround, pos, Quaternion.identity, this.gameObject.transform);
            return;
        }

        if (pixelColor != Color.black)
        {
            Instantiate(backGround, pos, Quaternion.identity, this.gameObject.transform);
        }
        
        foreach (ColorToPrefab item in colorMappings) // Spawns object based on colour.
        {
            if (item.color.Equals(pixelColor))
            {
                Instantiate(item.prefab, pos, Quaternion.identity, this.gameObject.transform);
            }
        }
    }
}
