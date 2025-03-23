using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class K_SceneManager : MonoBehaviour
{
    public enum TargetScene
    {
        Title,
        Main
    }
    public TargetScene targetScene;

    public void ChangeScene()
    {
        if (targetScene == TargetScene.Title)
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if (targetScene == TargetScene.Main)
        {
            SceneManager.LoadScene("Susan_TestScene");
        }
    }
}
