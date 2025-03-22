using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public GameObject[] backgrounds; // ������б���������
    private float backgroundWidth;

    void Start()
    {
        // �������б����Ŀ����ͬ
        backgroundWidth = backgrounds[0].GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        foreach (GameObject bg in backgrounds)
        {
            // �����ƶ�����
            bg.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // ��������ȫ�Ƴ������Ļʱ�����¶�λ�����Ҳ�
            if (bg.transform.position.x < -backgroundWidth)
            {
                Vector3 newPos = bg.transform.position;
                newPos.x += 2 * backgroundWidth;
                bg.transform.position = newPos;
            }
        }
    }
}
