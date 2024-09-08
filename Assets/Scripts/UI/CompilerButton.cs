using UnityEngine;
using UnityEngine.SceneManagement;

public class CompilerButton : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("CardFactory");
    }
}
