using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;

    private void Start()
    {
        PositionWalls();
    }

    private void PositionWalls()
    {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenTopRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        float wallThickness = 1f;

        // Верхняя стена
        topWall.transform.position = new Vector3(0, screenTopRight.y + wallThickness / 2, 0);
        topWall.transform.localScale = new Vector3(screenTopRight.x * 2, wallThickness, 1);

        // Нижняя стена
        bottomWall.transform.position = new Vector3(0, screenBottomLeft.y - wallThickness / 2, 0);
        bottomWall.transform.localScale = new Vector3(screenTopRight.x * 2, wallThickness, 1);

        // Левая стена
        leftWall.transform.position = new Vector3(screenBottomLeft.x - wallThickness / 2, 0, 0);
        leftWall.transform.localScale = new Vector3(wallThickness, screenTopRight.y * 2, 1);

        // Правая стена
        rightWall.transform.position = new Vector3(screenTopRight.x + wallThickness / 2, 0, 0);
        rightWall.transform.localScale = new Vector3(wallThickness, screenTopRight.y * 2, 1);
    }
}