using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSortColorGame : MonoBehaviour
{
    public GameGraphic graphic;

    public List<Bottle> bottles;
   
    public GameLevelReader levelReader;
    public int levelPlaying;
    public static BallSortColorGame instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BallSortColorUIManager.Instance.UpdateLevelText(DataManager.Instance.GetLevel());
    }

    public void LoadLevel(List<int[]> listArray)
    {
        

        bottles = new List<Bottle>();

        foreach (int[] arr in listArray) 
        {
            Bottle b = new Bottle();

            for (int i = 0; i < arr.Length; i++)
            {
                int element = arr[i];
                if(element == 0)
                {
                    continue;   
                }

                b.balls.Add(new Ball
                {
                    type = element
                });
            }
            bottles.Add(b);
        }

        graphic.CreateBottleGraphic(bottles);
    }

    

    public bool CheckWin()
    {
        bool winFlag = true;
        foreach (Bottle bottle in bottles)
        {
            if (bottle.balls.Count == 0)
            {
                continue;
            }

            if (bottle.balls.Count < 4)
            {
                winFlag = false;
                break;
            }

            bool sameTypeFlag = true;
            int type = bottle.balls[0].type;
            foreach (Ball ball in bottle.balls)
            {
                if (ball.type != type)
                {
                    sameTypeFlag = false;
                    break;
                }
            }
            if (!sameTypeFlag)
            {
                winFlag = false;
                break;
            }
        }

        return winFlag;
    }

    


    public void OnWin()
    {
        graphic.SetUndoUiState(false);
        graphic.hasMovedBall = false;
        graphic.freeUndoCount = 1;
        graphic.AddCount = 1;
        if (graphic.AddCount == 1)
        {
            graphic.SetSupUiState(true);

        }
        else
        {
            graphic.SetSupUiState(false);

        }
        BallSortColorUIManager.Instance.TurnAds(false);
        graphic.CanClick = false;
        //graphic.ClearBottleGraphics();
        int level = DataManager.Instance.GetLevel() + 1;
        DataManager.Instance.SetLevel(level);
        if (levelReader.curLevel != null) { Destroy(levelReader.curLevel);  }
        levelReader.LoadLevel(level);
        graphic.IsBottleAdded = false;
        BallSortColorUIManager.Instance.CheckGuide(DataManager.Instance.GetLevel());
        BallSortColorUIManager.Instance.UpdateLevelText(level);
        BallSortColorUIManager.Instance.NextLevel();
        StartCoroutine(graphic.EnableClickAfterDelay(3.5f));
        BackgroundManager.Instance.ChangeBackground();
        //Nadeem Ads BallSort
        //if (AdsController.Instance.InternAdsTime <= 0)
        //{
        //    AdManager.instance.ShowInter(() =>
        //    {
        //        AdsController.Instance.ResetTime();

        //    },
        //    () =>
        //    {
        //        AdsController.Instance.ResetTime();


        //    }, "Null");
        //}

    }




    /*public void InitBottles()
    {
        Debug.Log("bottles-------------");
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bottles.Count; i++)
        {
            Bottle b = bottles[i];
            sb.Append("Bottle" + (i + 1) + ":");
            foreach (Ball ball in b.balls)
            {
                sb.Append(" " + ball.type);
                sb.Append(",");
            }
            Debug.Log(sb.ToString());

            sb.Clear();
        }
        bool isWin = CheckWin();
        Debug.Log("isWin" + isWin);
    }*/





    public void SwithcBall(int bottleIndex1, int bottleIndex2)
    {
        Bottle b1 = bottles[bottleIndex1];
        Bottle b2 = bottles[bottleIndex2];

        SwitchBall(b1, b2);

        graphic.RefreshBottle(bottles);
    }
    public void SwitchBall(Bottle bottle1, Bottle bottle2)
    {
        // khi add them ball vao thi se them vao sau cung cua list 
        // VD ball0, ball1, ball2, ball3 nen => phan tu o tren cung se la list.count - 1 (4-1 = 3 chinh la {ball3})
        List<Ball> bottle1Balls = bottle1.balls;
        List<Ball> bottle2Balls = bottle2.balls;

        if (bottle1Balls.Count == 0)
            return;

        if (bottle2Balls.Count == 4)
            return;

        int index = bottle1Balls.Count - 1;
        Ball b = bottle1Balls[index];
        var type = b.type;

        if (bottle2Balls.Count > 0 && bottle2Balls[bottle2Balls.Count - 1].type != type)
        {
            return;
        }
        for (int i = index; i >= 0; i--)
        {
            Ball ball = bottle1Balls[i];
            if (ball.type == type)
            {
                bottle1Balls.RemoveAt(i);
                bottle2Balls.Add(ball);

                if (bottle2Balls.Count == 4)
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }
    }

    
    public List<SwtichBallCommand> CheckSwitchBall(int bottleIndex1, int bottleIndex2)
    {
        List<SwtichBallCommand> commands = new List<SwtichBallCommand>();
        Bottle bottle1 = bottles[bottleIndex1];
        Bottle bottle2 = bottles[bottleIndex2];

        List<Ball> bottle1Balls = bottle1.balls;
        List<Ball> bottle2Balls = bottle2.balls;

        if (bottle1Balls.Count == 0)    
            return commands;

        if (bottle2Balls.Count == 4)
            return commands;

        int index = bottle1Balls.Count - 1;
        Ball b = bottle1Balls[index];
        var type = b.type;

        if (bottle2Balls.Count > 0 && bottle2Balls[bottle2Balls.Count - 1].type != type)
        {
            return commands;
        }

        int targetIndex = bottle2Balls.Count;

        for (int i = index; i >= 0; i--)
        {
            Ball ball = bottle1Balls[i];
            if (ball.type == type)
            {
                // chuyen tu i sang targetIndex / targetIndex ++ / if targetIndex >= 4 break
                int fromBallIndex = i;
                int toBallIndex  = targetIndex;
                int fromBottleIndex = bottleIndex1;
                int toBottleIndex = bottleIndex2;

                commands.Add(new SwtichBallCommand
                {
                    type = type,
                    fromBallIndex = fromBallIndex,
                    toBallIndex = toBallIndex,
                    fromBottleIndex = fromBottleIndex,
                    toBottleIndex = toBottleIndex,
                });

                targetIndex++;
                

                if (targetIndex == 4)
                {
                    
                    break;
                }
            }
            else
                break;

        }
        return commands;
    }

    public class SwtichBallCommand
    {
        public int type;
        public int fromBottleIndex;
        public int fromBallIndex;

        public int toBallIndex;
        public int toBottleIndex;


    }

    

    public class Ball
    {
        public int type;
    }

    public class Bottle
    {
        public List<Ball> balls = new List<Ball>();
    }

   
}
