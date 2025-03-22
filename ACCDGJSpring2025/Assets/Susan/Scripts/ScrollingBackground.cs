using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public GameObject[] backgrounds; // 存放所有背景的数组
    private float backgroundWidth;

    void Start()
    {
        // 假设所有背景的宽度相同
        backgroundWidth = backgrounds[0].GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        foreach (GameObject bg in backgrounds)
        {
            // 向左移动背景
            bg.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // 当背景完全移出左侧屏幕时，重新定位到最右侧
            if (bg.transform.position.x < -backgroundWidth)
            {
                Vector3 newPos = bg.transform.position;
                newPos.x += 2 * backgroundWidth;
                bg.transform.position = newPos;
            }
        }
    }
}
