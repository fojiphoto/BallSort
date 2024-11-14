using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelReader : MonoBehaviour
{
    public TextAsset levels;
    public BallSortColorGame game;
    public GameObject curLevel;
    

    private void Start()
    {
        
        LoadLevel(DataManager.Instance.GetLevel());
    }

    public void LoadLevel(int indexLevel)
    {
        StartCoroutine(LoadTextFromResourcesAsync(indexLevel));
    }

    private IEnumerator LoadTextFromResourcesAsync(int indexLevel)
    {
        BallSortColorGame.instance.levelPlaying = indexLevel;
        string fileName = "Levelsss/Level" + indexLevel;
        ResourceRequest request = Resources.LoadAsync<TextAsset>(fileName);

        yield return request;

        if (request.asset != null)
        {
            TextAsset textAsset = request.asset as TextAsset;
            SpawnLevel(textAsset);
        }
        else
        {
            Debug.LogError("Không thể load tệp " + fileName);
        }
    }

    public void SpawnLevel(TextAsset textAsset)
    {
        GameObject level = new GameObject();
        level.name = "LevelGenerate";
        curLevel = level;

        string[] lines = textAsset.text.Split(new string[] { "\n", "\r" }, System.StringSplitOptions.RemoveEmptyEntries);

        int bottleCount = 0;
        int ballPerBottle = 0;

        List<int[]> bottleArray = new List<int[]>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (i == 0)
            {
                string[] line0Split = line.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                bottleCount = int.Parse(line0Split[0]);
                ballPerBottle = int.Parse(line0Split[1]);
            }
            else
            {
                int[] convertArray = new int[ballPerBottle];
                for (int j = 0; j < convertArray.Length; j++)
                {
                    convertArray[j] = CharacterToInt(line[j]);
                    
                }
                bottleArray.Add(convertArray);
            }
        }

        game.LoadLevel(bottleArray);
    }


    private int CharacterToInt(char c)
    {
        switch (c)
        {
            case '0': return 0;
            case '1': return 1;
            case '2': return 2;
            case '3': return 3;
            case '4': return 4;
            case '5': return 5;
            case '6': return 6;
            case '7': return 7;
            case '8': return 8;
            case '9': return 9;
            case 'A': return 10;
            case 'B': return 11;
            case 'C': return 12;
            case 'D': return 13;
            case 'E': return 14;
            case 'F': return 15;
            default: return 0;
        }
    }
}
