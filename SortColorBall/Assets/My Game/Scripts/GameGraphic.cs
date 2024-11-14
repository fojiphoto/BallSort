using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BallSortSkin
{
    public string nameSkin;
    public Sprite[] sprites;
}
public class GameGraphic : MonoBehaviour
{
    public static GameGraphic Instance;
    private BallSortColorGame game;
    private BallGraphic previewBall;
    public bool isSwitchingBall = false;
    private bool canClick = false;
    private bool isBottleAdded = false; // Biến kiểm tra việc thêm bottle
    private bool isMoving = false;
    private int pendingBalls = 0;
    [SerializeField] private int bottlesPerRow = 5; // Số lượng lọ tối đa trong 1 hàng
    private bool isBallMoved = false;

    public bool hasMovedBall = false; // Kiểm tra bóng đã được chuyển chưa

    public int selectedBottleIndex = -1;
    public List<BottleGraphic> bottleGraphics;
    public BallGraphic prefabBallGraphic;
    public BottleGraphic prefabBottleGraphic;
    public Vector3 bottleStartPosition;
    public Vector3 bottleDistance;
    public GameLevelReader gameLevelReader;
    public GameObject effectBallDown;
    public int maxBallsInBottle = 4;
    public Button undoUi;
    public CanvasGroup undoCanvasGroup;
    public int freeUndoCount = 1; // Số lần sử dụng miễn phí ban đầu
    public int AddCount = 1; // Số lần sử dụng miễn phí ban đầu

    public int idSkin;

    public List<BallSortSkin> skins;


    public Sprite[] Skin()
    {
        return skins[idSkin].sprites;
    }

    public bool IsBottleAdded { get => isBottleAdded; set => isBottleAdded = value; }
    public bool CanClick { get => canClick; set => canClick = value; }

    private void Awake()
    {
        Instance = this;
        gameLevelReader = GetComponent<GameLevelReader>();
       // AdManager.instance.ShowBanner();
        idSkin = PlayerPrefs.GetInt("ballskinPref", 0);


    }


    private void Start()
    {
        Application.targetFrameRate = 60;

        selectedBottleIndex = -1;
        game = FindAnyObjectByType<BallSortColorGame>();
        StartCoroutine(EnableClickAfterDelay(3.5f));
        previewBall = Instantiate(prefabBallGraphic);
        SetUndoUiState(false);


    }




    public void SetUndoUiState(bool isInteractable)
    {
        // Set the alpha based on interactability
        undoCanvasGroup.alpha = isInteractable ? 1f : 0.5f;
        undoCanvasGroup.interactable = isInteractable;
        undoCanvasGroup.blocksRaycasts = isInteractable;
    }
    public Button supBottleButton; // Tham chiếu tới button trên UI

    public CanvasGroup supCanvasGroup;
    public void SetSupUiState(bool isInteractable)
    {
        // Set the alpha based on interactability
        supCanvasGroup.alpha = isInteractable ? 1f : 0.5f;
        supCanvasGroup.interactable = isInteractable;
        supCanvasGroup.blocksRaycasts = isInteractable;
    }
    private Queue<Vector3> GetCommandPath(BallSortColorGame.SwtichBallCommand command)
    {
        Queue<Vector3> queueMovement = new Queue<Vector3>();

        queueMovement.Enqueue(bottleGraphics[command.fromBottleIndex].GetBallPosition(command.fromBallIndex));
        queueMovement.Enqueue(bottleGraphics[command.fromBottleIndex].GetBottleUpPosition());
        queueMovement.Enqueue(bottleGraphics[command.toBottleIndex].GetBottleUpPosition());
        queueMovement.Enqueue(bottleGraphics[command.toBottleIndex].GetBallPosition(command.toBallIndex));

        return queueMovement;
    }

    public void ClearBottleGraphics()
    {
        foreach (var bottleGraphic in bottleGraphics)
        {
            if (bottleGraphic == null) break;
            Destroy(bottleGraphic.gameObject); // Xóa đối tượng chai khỏi scene
        }

        bottleGraphics.Clear(); // Xóa danh sách chai sau khi đã hủy các đối tượng
    }
    public Camera mainCamera; // Tham chiếu đến Camera chính của bạn
    private void AdjustCameraToFitBottles(int totalRows, int totalBottles)
    {
        // Tính toán chiều rộng và chiều cao cần thiết để hiển thị tất cả các lọ
        float requiredWidth = (Mathf.Min(totalBottles, bottlesPerRow) - 1) * bottleDistance.x;
        float requiredHeight = (totalRows - 1) * bottleDistance.y;

        // Lấy kích thước của camera theo chiều ngang và chiều dọc
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraWidth = mainCamera.orthographicSize * 2 * screenAspect;
        float cameraHeight = mainCamera.orthographicSize * 2;

        // Điều chỉnh camera nếu cần thiết
        if (requiredWidth > cameraWidth || requiredHeight > cameraHeight)
        {
            float newOrthographicSize = Mathf.Max(requiredWidth / (2 * screenAspect), requiredHeight / 2);
            mainCamera.orthographicSize = newOrthographicSize;
        }
    }

    public void AddNewBottle()
    {
        if (!isBottleAdded && !isMoving)
        {
            AddCount--;
            BottleGraphic bg = Instantiate(prefabBottleGraphic);
            bg.transform.parent = gameLevelReader.curLevel.transform;
            bottleGraphics.Add(bg);

            BallSortColorGame.Bottle newBottle = new BallSortColorGame.Bottle();
            newBottle.balls = new List<BallSortColorGame.Ball>();  // Tạo danh sách bóng rỗng
            game.bottles.Add(newBottle);
            List<int> ballTypes = new List<int>();
            BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);
            bg.SetGraphic(ballTypes.ToArray());


            // Cập nhật vị trí của tất cả các bottle
            PositionBottles();
            SetSupUiState(false);
            isBottleAdded = true;

            // Điều chỉnh camera để hiển thị đủ tất cả các bottles
            //AdjustCameraToFitBottles();
        }
    }

    public void AddNewBottleReward()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //UIManager.Instance.ShowNoInternetPopUp();
            Debug.Log("khong co internet");
            return;
        }

        //AdManager.instance.ShowReward(() =>
        //{
        //    //AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);
        //    AddNewBottle();
        //    //ResetTimer();

        //}, () =>
        //{
        //    //ResetTimer();

        //}, "YourPlacementID");
        //Nadeem Ads BallSort
        AddNewBottle();
    }

    private void PositionBottles()
    {
        // Tính tổng số lượng hàng cần thiết
        int totalBottles = bottleGraphics.Count;
        int totalRows = Mathf.CeilToInt((float)totalBottles / bottlesPerRow);

        // Tính kích thước của màn hình dựa trên camera
        float screenWidth = 2f * Camera.main.orthographicSize * Screen.width / Screen.height;
        float screenHeight = 2f * Camera.main.orthographicSize;

        // Tính toán vị trí bắt đầu để căn giữa
        float rowHeight = bottleDistance.y;
        float colWidth = bottleDistance.x;

        // Tính chiều cao tổng cộng các hàng để căn chỉnh theo chiều dọc
        float totalHeight = (totalRows - 1) * rowHeight;
        float startY = bottleStartPosition.y + totalHeight / 2;

        for (int row = 0; row < totalRows; row++)
        {
            // Tính số lượng bottle trên hàng hiện tại
            int bottlesInCurrentRow = Mathf.Min(bottlesPerRow, totalBottles - row * bottlesPerRow);

            // Tính tổng chiều rộng của hàng hiện tại để căn giữa theo chiều ngang
            float totalRowWidth = (bottlesInCurrentRow - 1) * colWidth;
            float startX = bottleStartPosition.x - totalRowWidth / 2;

            for (int i = 0; i < bottlesInCurrentRow; i++)
            {
                int bottleIndex = row * bottlesPerRow + i;

                // Đặt vị trí cho mỗi bottleGraphics
                Vector3 pos = new Vector3(startX + i * colWidth, startY - row * rowHeight, bottleStartPosition.z);
                bottleGraphics[bottleIndex].transform.position = pos;
                bottleGraphics[bottleIndex].index = bottleIndex;
            }
        }
    }
    public void CreateBottleGraphic(List<BallSortColorGame.Bottle> bottles)
    {
        isSwitchingBall = false;
        ClearBottleGraphics();
        GameObject selectedPrefab = SkinManager.equippedPrefab; // This holds the currently selected skin prefab

        foreach (BallSortColorGame.Bottle b in bottles)
        {
            BottleGraphic bg = Instantiate(selectedPrefab).GetComponent<BottleGraphic>();
            bg.transform.parent = gameLevelReader.curLevel.transform;
            bottleGraphics.Add(bg);

            List<int> ballTypes = new List<int>();

            foreach (var ball in b.balls)
            {
                ballTypes.Add(ball.type);
            }

            bg.SetGraphic(ballTypes.ToArray());
        }

        // Tính tổng số lượng hàng cần thiết
        int totalBottles = bottleGraphics.Count;
        int totalRows = Mathf.CeilToInt((float)totalBottles / bottlesPerRow);

        // Tính kích thước của màn hình dựa trên camera
        float screenWidth = 2f * Camera.main.orthographicSize * Screen.width / Screen.height;
        float screenHeight = 2f * Camera.main.orthographicSize;

        // Tính toán vị trí bắt đầu để căn giữa
        float rowHeight = bottleDistance.y;
        float colWidth = bottleDistance.x;

        // Tính chiều cao tổng cộng các hàng để căn chỉnh theo chiều dọc
        float totalHeight = (totalRows - 1) * rowHeight;
        float startY = bottleStartPosition.y + totalHeight / 2;  // Bắt đầu từ vị trí trên cùng

        for (int row = 0; row < totalRows; row++)
        {
            // Tính số lượng lọ trên hàng hiện tại (tối đa 5)
            int bottlesInCurrentRow = Mathf.Min(bottlesPerRow, totalBottles - row * bottlesPerRow);

            // Tính tổng chiều rộng của hàng hiện tại để căn giữa theo chiều ngang
            float totalRowWidth = (bottlesInCurrentRow - 1) * colWidth;
            float startX = bottleStartPosition.x - totalRowWidth / 2;  // Bắt đầu căn giữa từ vị trí ngang

            for (int i = 0; i < bottlesInCurrentRow; i++)
            {
                int bottleIndex = row * bottlesPerRow + i;

                // Đặt vị trí cho mỗi bottleGraphics
                Vector3 pos = new Vector3(startX + i * colWidth, startY - row * rowHeight, bottleStartPosition.z);
                bottleGraphics[bottleIndex].transform.position = pos;
                bottleGraphics[bottleIndex].index = bottleIndex;
            }
        }

        // Tự động điều chỉnh camera để đảm bảo tất cả lọ đều hiển thị
        AdjustCameraToFitBottles(totalRows, totalBottles);

    }

    public void RefreshBottle(List<BallSortColorGame.Bottle> bottles)
    {
        for (int i = 0; i < bottles.Count; i++)
        {
            BallSortColorGame.Bottle gb = bottles[i];
            BottleGraphic bottleGraphic = bottleGraphics[i];

            List<int> ballTypes = new List<int>();

            foreach (var ball in gb.balls)
            {
                ballTypes.Add(ball.type);
            }

            bottleGraphic.SetGraphic(ballTypes.ToArray());
        }
    }

    public IEnumerator EnableClickAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canClick = true;
    }
    public void OnClickBottle(int bottleIndex)
    {

        // trang thai mac dinh: -1
        // trang thai co ball: bottleIndex

        if (isSwitchingBall || !canClick)
        {
            return;
        }



        if (selectedBottleIndex == -1)
        {
            if (game.bottles[bottleIndex].balls.Count != 0)
            {
                selectedBottleIndex = bottleIndex;
                StartCoroutine(MoveBallUp(bottleIndex));
                // Vô hiệu hóa nút Undo khi bóng chưa được chuyển
                //SetUndoUiState(false);
                if (DataManager.Instance.GetLevel() == 1)
                {
                    BallSortColorUIManager.Instance.ShowMoveGuide();

                }
                else
                {
                    BallSortColorUIManager.Instance.guideMoveBall.SetActive(false);
                }

                // Đặt cờ về false vì chưa có di chuyển nào
                isBallMoved = false;
            }
        }
        else
        {
            // Nếu chọn lại cùng một bottle, trả bóng về vị trí cũ
            if (selectedBottleIndex == bottleIndex)
            {
                StartCoroutine(MoveBallDown(bottleIndex));

                selectedBottleIndex = -1;

                // Chỉ vô hiệu hóa Undo nếu bóng chưa từng được di chuyển
                /*if (!isBallMoved)
                {
                    SetUndoUiState(false);
                }*/
            }
            else
            {
                // Kiểm tra xem bóng ở bottle đích có cùng loại với bóng đang chọn không
                BallSortColorGame.Bottle selectedBottle = game.bottles[selectedBottleIndex];
                BallSortColorGame.Bottle targetBottle = game.bottles[bottleIndex];

                if (targetBottle.balls.Count > 0 && targetBottle.balls[targetBottle.balls.Count - 1].type != selectedBottle.balls[selectedBottle.balls.Count - 1].type)
                {
                    // Nếu không cùng loại, trả bóng về vị trí ban đầu
                    StartCoroutine(MoveBallDown(selectedBottleIndex));
                    selectedBottleIndex = -1;
                    // Không cho phép Undo vì bóng không được chuyển
                    /*if (!isBallMoved)
                    {
                        SetUndoUiState(false);
                    }*/
                }
                else
                {

                    // Nếu cùng loại, thực hiện chuyển bóng
                    StartCoroutine(SwtichBallCoroutine(selectedBottleIndex, bottleIndex));
                    // Bật trạng thái Undo khi bóng đã được chuyển sang bottle khác
                    SetUndoUiState(true);
                    if (AddCount == 1)
                    {
                        SetSupUiState(true);

                    }
                    else
                    {
                        SetSupUiState(false);

                    }
                    // Đặt cờ isBallMoved thành true khi bóng đã thực sự di chuyển
                    isBallMoved = true;

                    selectedBottleIndex = -1; // Sau khi chuyển xong, reset trạng thái
                }
            }
        }
    }

    private IEnumerator SwitchBall(BallSortColorGame.SwtichBallCommand command, Queue<Vector3> movement)
    {
        // tat graphic o vi tri from.
        //sau do tao 1 ball o vi tri from co cung type
        //di chuyen ball theo dung duong
        //xoa ball di chuyen
        // xoa ball di chuyen, bat graphic o vi tri to
        MoveCommand moveCommand = new MoveCommand
        {
            fromBottleIndex = command.fromBottleIndex,
            toBottleIndex = command.toBottleIndex,
            ballType = command.type,

        };

        moveHistory.Push(moveCommand);

        bottleGraphics[command.fromBottleIndex].SetGraphicNone(command.fromBallIndex);

        Vector3 spawnPosition = movement.Peek();

        var ballObject = Instantiate(prefabBallGraphic, spawnPosition, Quaternion.identity);

        ballObject.SetColor(command.type);



        while (movement.Count > 0)
        {
            Vector3 target = movement.Dequeue();

            while (Vector3.Distance(ballObject.transform.position, target) > 0.005f)
            {
                ballObject.transform.position = Vector3.MoveTowards(ballObject.transform.position, target, 10 * Time.deltaTime);
                yield return null;
            }
        }


        yield return null;


        Destroy(ballObject.gameObject);
        bottleGraphics[command.toBottleIndex].SetGraphic(command.toBallIndex, command.type);

        // Check if the destination bottle now contains 4 balls of the same color


        Vector3 effectPosition = bottleGraphics[command.toBottleIndex].GetBallPosition(command.toBallIndex);
        Instantiate(effectBallDown, effectPosition + new Vector3(0f, -0.2f, 0f), Quaternion.identity);

        CheckBottleForLessThanFourBalls(command.toBottleIndex);


        pendingBalls--;
    }

    private IEnumerator SwtichBallCoroutine(int fromBottleIndex, int toBottleIndex)
    {
        isSwitchingBall = true;
        List<BallSortColorGame.SwtichBallCommand> commands = game.CheckSwitchBall(fromBottleIndex, toBottleIndex);
        List<BallSortColorGame.Ball> ballList = game.bottles[fromBottleIndex].balls;

        if (commands.Count == 0)
        {


            yield return StartCoroutine(MoveBallDown(selectedBottleIndex));

            BallSortColorGame.Ball b = ballList[ballList.Count - 1];
            bottleGraphics[fromBottleIndex].SetGraphic(ballList.Count - 1, b.type);
            isSwitchingBall = false;

            yield break;
        }
        else
        {
            pendingBalls = commands.Count;

            previewBall.gameObject.SetActive(false);

            for (int i = 0; i < commands.Count; i++)
            {
                BallSortColorGame.SwtichBallCommand command = commands[i];
                Queue<Vector3> moveQueue = GetCommandPath(command);

                if (i == 0)
                {
                    moveQueue.Dequeue();
                }
                StartCoroutine(SwitchBall(command, moveQueue));
                yield return new WaitForSeconds(0.06f);


            }


            while (pendingBalls > 0)
            {
                yield return null;
            }
            // game.SwitchBall(game.bottles[fromBottleIndex], game.bottles[toBottleIndex]);

            // Sau khi di chuyển, thực hiện kiểm tra chiến thắng
            game.SwitchBall(game.bottles[fromBottleIndex], game.bottles[toBottleIndex]);
            /*if (game.bottles[toBottleIndex].balls.Count >= maxBallsInBottle)
            {
               
                AudioController.Instance.PlaySound(AudioController.Instance.completeTube);
            }*/
            BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.ballDown);


            // Kiểm tra nếu người chơi chiến thắng
            if (game.CheckWin())
            {

                StartCoroutine(BallSortColorUIManager.Instance.ShowWin());
                supBottleButton.interactable = true;
                // Thêm logic dừng game và thông báo chiến thắng
                // Ví dụ: Có thể hiện UI chiến thắng hoặc tắt các hành động chuyển bóng tiếp theo
                yield break;
            }

            //game.SwitchBall(fromBottleIndex, toBottleIndex);
        }
        hasMovedBall = true; // Bóng đã được chuyển sang bottle khác
        selectedBottleIndex = -1;
        isSwitchingBall = false;

    }

    private IEnumerator MoveBallDown(int bottleIndex)
    {
        isSwitchingBall = true;
        List<BallSortColorGame.Ball> ballList = game.bottles[bottleIndex].balls;
        /*if (ballList.Count == 0) // Kiểm tra nếu không có bóng nào để di chuyển
        {
            isSwitchingBall = false;
            yield break;
        }*/
        Vector3 downPosition = bottleGraphics[bottleIndex].GetBallPosition(ballList.Count - 1);

        Vector3 ballPostion = bottleGraphics[bottleIndex].GetBottleUpPosition();

        previewBall.transform.position = ballPostion;




        while (Vector3.Distance(previewBall.transform.position, downPosition) > 0.005f)
        {
            previewBall.transform.position = Vector3.MoveTowards(previewBall.transform.position, downPosition, 10 * Time.deltaTime);

            yield return null;
        }

        previewBall.gameObject.SetActive(false);
        SetUndoUiState(true);


        if (AddCount == 1)
        {
            SetSupUiState(true);

        }
        else
        {
            SetSupUiState(false);

        }




        BallSortColorGame.Ball b = ballList[ballList.Count - 1];

        bottleGraphics[bottleIndex].SetGraphic(ballList.Count - 1, b.type);

        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.ballDown);
        Instantiate(effectBallDown, downPosition + new Vector3(0f, -0.2f, 0f), Quaternion.identity);
        isSwitchingBall = false;

    }

    private IEnumerator MoveBallUp(int bottleIndex)
    {
        isSwitchingBall = true;

        Vector3 upPosition = bottleGraphics[bottleIndex].GetBottleUpPosition();
        List<BallSortColorGame.Ball> ballList = game.bottles[bottleIndex].balls;
        if (ballList.Count == 0) // Kiểm tra nếu không có bóng nào để di chuyển
        {
            isSwitchingBall = false;
            yield break;
        }
        //lay qua bong cao nhat
        BallSortColorGame.Ball b = ballList[ballList.Count - 1];
        Vector3 ballPosition = bottleGraphics[bottleIndex].GetBallPosition(ballList.Count - 1);

        bottleGraphics[bottleIndex].SetGraphicNone(ballList.Count - 1);

        previewBall.SetColor(b.type);

        previewBall.transform.position = ballPosition;

        previewBall.gameObject.SetActive(true);
        //AudioController.Instance.PlaySound(AudioController.Instance.ballUp);
        SetUndoUiState(false);


        if (AddCount == 1)
        {
            SetSupUiState(true);

        }
        else
        {
            SetSupUiState(false);

        }




        while (Vector3.Distance(previewBall.transform.position, upPosition) > 0.005f)
        {
            previewBall.transform.position = Vector3.MoveTowards(previewBall.transform.position, upPosition, 10 * Time.deltaTime);
            yield return null;
        }

        isSwitchingBall = false;

    }

    private void CheckBottleForLessThanFourBalls(int bottleIndex)
    {
        BallSortColorGame.Bottle bottle = game.bottles[bottleIndex];

        // Đảm bảo chai có ít nhất 4 bóng
        if (bottle.balls.Count >= 4)
        {
            int type = bottle.balls[bottle.balls.Count - 1].type; // Lấy loại của bóng ở đỉnh chai
            int count = 0;

            // Đếm số bóng liên tiếp cùng loại từ đỉnh chai
            for (int i = bottle.balls.Count - 1; i >= 0; i--)
            {
                if (bottle.balls[i].type == type)
                {
                    count++;
                }
                else
                {
                    break;
                }

                // Nếu số bóng cùng loại < 4, ghi log
                if (count < 4)
                {
                    Debug.Log($"Bottle {bottleIndex} has less than 4 balls of type {type}. Count = {count}");
                    return;
                }
            }
        }
    }
    private Stack<MoveCommand> moveHistory = new Stack<MoveCommand>();

    public class MoveCommand
    {
        public int fromBottleIndex;
        public int toBottleIndex;
        public int ballType;

    }


    public void UndoLastMove()
    {

        if (moveHistory.Count > 0)
        {
            if (!isMoving)
            {
                MoveCommand lastMove = moveHistory.Pop();
                BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);
                // Reverse the move
                StartCoroutine(UndoMoveCoroutine(lastMove));
            }

        }
    }

    public void AdsUndoLastMove()
    {
        if (!hasMovedBall)
        {
            SetUndoUiState(false);
            return;
        }
        if (freeUndoCount > 0)
        {

            freeUndoCount--;
            UndoLastMove();
            BallSortColorUIManager.Instance.TurnAds(true);

        }
        else
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                //UIManager.Instance.ShowNoInternetPopUp();
                Debug.Log("showInternet");
                return;
            }
            //Nadeem Ads BallSort
            //AdManager.instance.ShowReward(() =>
            //{

            //    UndoLastMove();
            //    //ResetTimer();

            //}, () =>
            //{
            //    //ResetTimer();

            //}, "YourPlacementID");

            UndoLastMove();
        }

    }

    private IEnumerator UndoMoveCoroutine(MoveCommand moveCommand)
    {
        // Đánh dấu là đang di chuyển
        isMoving = true;
        undoUi.interactable = false; // Kích hoạt lại nút Undo

        //undoUi.interactable = false; // Vô hiệu hóa nút Undo

        // Lấy đồ họa chai
        BottleGraphic fromBottleGraphic = bottleGraphics[moveCommand.fromBottleIndex];
        BottleGraphic toBottleGraphic = bottleGraphics[moveCommand.toBottleIndex];

        // Lấy dữ liệu chai và quả bóng
        BallSortColorGame.Bottle fromBottle = game.bottles[moveCommand.fromBottleIndex];
        BallSortColorGame.Bottle toBottle = game.bottles[moveCommand.toBottleIndex];



        // Loại bỏ quả bóng khỏi chai đích và thêm nó trở lại chai nguồn
        toBottle.balls.RemoveAt(toBottle.balls.Count - 1);
        fromBottle.balls.Add(new BallSortColorGame.Ball { type = moveCommand.ballType });

        // Ẩn đồ họa chai tạm thời trước khi di chuyển quả bóng
        toBottleGraphic.SetGraphicNone(toBottle.balls.Count);
        fromBottleGraphic.SetGraphicNone(fromBottle.balls.Count - 1);

        // Di chuyển quả bóng trở lại chai nguồn
        Vector3 fromPosition = fromBottleGraphic.GetBallPosition(fromBottle.balls.Count - 1);
        Vector3 toPosition = toBottleGraphic.GetBallPosition(toBottle.balls.Count);



        var ballObject = Instantiate(prefabBallGraphic, toPosition, Quaternion.identity);
        ballObject.SetColor(moveCommand.ballType);

        Queue<Vector3> moveQueue = new Queue<Vector3>();
        moveQueue.Enqueue(toPosition);
        moveQueue.Enqueue(fromPosition);



        while (moveQueue.Count > 0)
        {
            Vector3 target = moveQueue.Dequeue();
            while (Vector3.Distance(ballObject.transform.position, target) > 0.005f)
            {
                ballObject.transform.position = Vector3.MoveTowards(ballObject.transform.position, target, 10 * Time.deltaTime);
                yield return null;
            }
        }



        Destroy(ballObject.gameObject);

        // Cập nhật đồ họa chai sau khi quả bóng đã hoàn tất di chuyển
        toBottleGraphic.SetGraphicNone(toBottle.balls.Count);
        fromBottleGraphic.SetGraphic(fromBottle.balls.Count - 1, moveCommand.ballType);


        Vector3 effectPosition = fromBottleGraphic.GetBallPosition(fromBottle.balls.Count - 1);
        Instantiate(effectBallDown, effectPosition + new Vector3(0f, -0.2f, 0f), Quaternion.identity);

        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.ballDown);

        // Đánh dấu là không còn di chuyển
        isMoving = false;
        undoUi.interactable = true; // Kích hoạt lại nút Undo
        //undoUi.interactable = true; // Kích hoạt lại nút Undo
    }







}
