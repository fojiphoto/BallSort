using UnityEngine;
using UnityEngine.UI; // Nếu sử dụng Image component
using DG.Tweening;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance;
    public List<Sprite> backgrounds; // List chứa các hình ảnh nền
    public Image backgroundImage; // Hình ảnh nền hiện tại
    public float transitionDuration = 1f; // Thời gian chuyển cảnh

    private Sprite currentBackground;

    protected void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Gọi hàm này mỗi khi kết thúc màn chơi
    public void ChangeBackground()
    {
        // Chọn background mới ngẫu nhiên nhưng không trùng với background hiện tại
        Sprite newBackground;
        do
        {
            newBackground = backgrounds[Random.Range(0, backgrounds.Count)];
        } while (newBackground == currentBackground);

        currentBackground = newBackground;

        // Sử dụng DoTween để làm mờ nền cũ và hiển thị nền mới
        backgroundImage.DOFade(0f, transitionDuration).OnComplete(() =>
        {
            backgroundImage.sprite = newBackground;
            backgroundImage.DOFade(1f, transitionDuration);
        });
    }
}
