using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BallSortColorSceneController : MonoBehaviour
{
    public static BallSortColorSceneController Instance;
    public CanvasGroup transition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RTransitionLoad();
    }

    public void TransitionLoad()
    {
        transition.gameObject.SetActive(true);
        transition.DOFade(0, 0f);
        transition.DOFade(1, 0.5f).OnComplete(() =>
        {
            SceneManager.LoadScene("GamePlay");
        });
    }

    public void RTransitionLoad()
    {
        transition.gameObject.SetActive(true);
        transition.DOFade(1, 0.5f);
        transition.DOFade(0, 0.5f).OnComplete(() =>
        {
            transition.gameObject.SetActive(false);
        });
    }



}
