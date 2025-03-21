using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float weakScrollSpeed = 0.1f;  
    public float swoleScrollSpeed = -0.1f; 
    private float currentSpeed;

    private Material backgroundMaterial;
    private Vector2 offset;

    public Renderer backgroundRenderer; 

    void Start()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogError("⚠️ BackgroundManager: backgroundRenderer 未设置，请在 Inspector 里赋值！");
            return;
        }

        backgroundMaterial = backgroundRenderer.material;
        currentSpeed = weakScrollSpeed; 
    }

    void Update()
    {
        if (backgroundMaterial == null) return;

        offset.x += currentSpeed * Time.deltaTime;
        backgroundMaterial.mainTextureOffset = offset;
    }

    public void SetBackgroundState(bool isSwole)
    {
        currentSpeed = isSwole ? swoleScrollSpeed : weakScrollSpeed;
    }
}
