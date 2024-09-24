using Coffee.UIExtensions;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.IO;
using System.Linq;

public class LevelGenerate : MonoBehaviour
{
    public int totalBottle;
    public int ballperBotle;
    public BottleGenerate bottle;
    public BottleGenerate bottleEmpty;
    public List<Sprite> sprites = new();
    public Game game;
    public Transform parent;
    public Vector2 spacing = new Vector2(0.2f, 0.2f); // Khoảng cách giữa các điểm ma trận

    public Vector3 startPosition = new Vector3(0, 0, 0); // Vị trí bắt đầu
    private List<GameObject> objs;
    public bool isCreateBottleEmpty;
    public int totalBottleEmpty;

    public int indexLevel;

    void InstantiateObjectsInMatrix()
    {
        ballData = new int[totalBottle * totalBottle];
        RandomSpriteBall();
        for (int col = 0; col < totalBottle; col++)
        {
            Vector3 position = new Vector3(
                startPosition.x + col * spacing.x,   // Khoảng cách theo trục X
                startPosition.y,                    // Không thay đổi theo trục Y (tùy bạn có muốn hay không)
                0f   // Khoảng cách theo trục Z
            );

            // Instantiate đối tượng tại vị trí đã tính toán
            BottleGenerate level = Instantiate(bottle, position, Quaternion.identity);
            GenerateColorBallBottle(level);
            level.transform.parent = parent;
            objs.Add(level.gameObject);
        }
    }

    public void GenerateColorBallBottle(BottleGenerate bottleGenerate)
    {
        for (int i = 0; i < bottleGenerate.sprites.Count; i++) {
            bottleGenerate.sprites[i].sprite = sprites[i];
            ballData[i] = CharacterToInt(sprites[i].name);
        }
    }

    private int CharacterToInt(string name)
    {
        int number = int.Parse(name.Substring(1));
        return number;
    }

    public void RandomSpriteBall()
    {
        Random random = new Random();
        ShuffleList(sprites, random);
    }

    public static void ShuffleList<T>(List<T> list, Random random)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    [Button("generate Level Demo")]
    public void Generate()
    {
        List<int[]> bottleArray = new List<int[]>();
        InstantiateObjectsInMatrix();
    }
    private int[] ballData;

    [Button("generate file text")]
    public void GenerateAndSaveTextFile()
    {
        string fileContent = totalBottle + "," + ballperBotle + "\n";

        for (int i = 0; i < totalBottle; i++)
        {
            for (int j = 0; j < ballperBotle; j++)
            {
                fileContent += ballData[i * ballperBotle + j];
            }

            fileContent += "\n";
        }

        string path = Path.Combine(Application.dataPath, "Resources/Levels", "Level"+indexLevel+".txt");

        File.WriteAllText(path, fileContent);

        Debug.Log("File saved to: " + path);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    [Button("clear Level Demo")]
    public void Clear()
    {
        foreach(GameObject obj in objs)
        {
            DestroyImmediate(obj);
        }
    }

}
