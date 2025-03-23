using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public GameObject prefab; // ���õ��������Prefab
        public float scrollSpeed; // �����ٶ�
        private Material material; // ������ʹ�õĲ���
        private Vector2 offset = Vector2.zero; // ����ƫ����

        public void Initialize(Transform parent)
        {
            GameObject instance = Instantiate(prefab, parent);
            material = instance.GetComponent<Renderer>().material;
        }

        public void MoveLayer(float direction)
        {
            if (material != null)
            {
                offset.x += scrollSpeed * direction * Time.deltaTime;
                material.mainTextureOffset = offset;
            }
        }
    }

    public ParallaxLayer[] layers; // �洢�����Ӳ��
    public Transform container; // �����������ڴ�����б������ʵ��
    private float direction = 1.0f; // Ĭ�����ҹ���
    public K_WizardBehavior playerScript; // Reference to the player script

    void Start()
    {
        foreach (var layer in layers)
        {
            layer.Initialize(container);
        }
    }

    void Update()
    {
        // Automatically set direction based on player state
        direction = playerScript.CurrentState == K_WizardBehavior.WizardStates.Weak ? 1.0f : -1.0f;

        if (Input.GetKeyDown(KeyCode.Space)) // �ո�����ڸı䷽��
        {
            direction *= -1; // �л�����
        }

        foreach (var layer in layers)
        {
            layer.MoveLayer(direction);
        }
    }
}
