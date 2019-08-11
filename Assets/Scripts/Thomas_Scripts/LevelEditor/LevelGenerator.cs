using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    public bool isDone = false;

    // Start is called before the first frame update
    void Awake()
    {
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
