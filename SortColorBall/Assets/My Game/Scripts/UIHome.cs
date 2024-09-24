using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHome : MonoBehaviour
{

    
    public void Replay()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        SceneManager.LoadScene("GamePlay");

    }

    public void Back()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        SceneManager.LoadScene("Menu");

    }

    


}
