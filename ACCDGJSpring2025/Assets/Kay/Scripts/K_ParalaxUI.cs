using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class K_ParalaxUI : MonoBehaviour
{
    public GameObject Element;

    Vector2 _startPos;

    public float Zpos;

    [SerializeField] float _intensity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = Element.transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 vpPosition = Camera.main.ScreenToViewportPoint(mousePosition);

        float posX = Mathf.Lerp(Element.transform.position.x, _startPos.x + (vpPosition.x * -_intensity), 2 * Time.deltaTime); // moves it slightly away from the mouse along the x axis (by _intensity)
        float posY = Mathf.Lerp(Element.transform.position.y, _startPos.y + (vpPosition.y * -_intensity), 2 * Time.deltaTime); // moves it slightly away from the mouse along the y axis (by _intensity)

        Element.transform.position = new Vector3(posX, posY, Zpos);
    }
}
