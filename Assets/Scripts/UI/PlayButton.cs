using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // public CardFactory cardFactory;
    public void ChangeScene()
    {
        // if (cardFactory != null)
        // {
        //     cardFactory.ProcessInput("sds"); 
        // }
        SceneManager.LoadScene("SetPlayer");
    }
}
