using UnityEngine;

public class TestScript : MonoBehaviour
{
    private BackgroundManager bgManager;
    private bool isSwole = false; 

    void Start()
    {
        bgManager = FindFirstObjectByType<BackgroundManager>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // press space to change status
        {
            isSwole = !isSwole;  
            bgManager.SetBackgroundState(isSwole); 
        }
    }
}
