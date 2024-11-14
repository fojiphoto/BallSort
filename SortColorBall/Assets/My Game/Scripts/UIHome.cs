using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHome : MonoBehaviour
{

    
    public void Replay()
    {
        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

        SceneManager.LoadScene("GamePlay");

    }

    public void Back()
    {
        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

        SceneManager.LoadScene("Menu");

    }

    


}
