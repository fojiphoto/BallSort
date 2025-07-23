using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHome : MonoBehaviour
{

    
    public void Replay()
    {

        AdManager.instance.ShowInterstitialWithoutConditions("");
        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

        SceneManager.LoadScene("GamePlay");

    }

    public void Back()
    {

        AdManager.instance.ShowInterstitialWithoutConditions("");
        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

        SceneManager.LoadScene("MenuBAllSort");

    }

    


}
