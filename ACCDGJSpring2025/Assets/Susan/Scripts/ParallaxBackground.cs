using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public GameObject prefab; // 引用到背景层的Prefab
        public float scrollSpeed; // 滚动速度
        private Material material; // 背景层使用的材料
        private Vector2 offset = Vector2.zero; // 纹理偏移量

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

    public ParallaxLayer[] layers; // 存储所有视差层
    public Transform container; // 容器对象，用于存放所有背景层的实例
    private float direction = 1.0f; // 默认向右滚动
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

        if (Input.GetKeyDown(KeyCode.Space)) // 空格键用于改变方向
        {
            direction *= -1; // 切换方向
        }

        foreach (var layer in layers)
        {
            layer.MoveLayer(direction);
        }
    }
}
