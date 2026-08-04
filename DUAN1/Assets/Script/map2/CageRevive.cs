using UnityEngine;

public class CageRevive : MonoBehaviour
{
    public CameraMap1 cameraMap;
    public PuzzleBase revivePuzzle;
    public GameObject deadCrow;

    bool wasFocus;

    void Update()
    {
        if (wasFocus && !cameraMap.isFocus)
        {
            Debug.Log("Đã thoát zoom");

            if (deadCrow.activeSelf)
            {
                Debug.Log("Đang hồi sinh quạ");
                Debug.Log("Gọi PuzzleBase");

                revivePuzzle.currentState = 0;

                // THÊM: cho phép chạy dù đã thoát zoom
                revivePuzzle.forceRun = true;

                revivePuzzle.OnClick();
            }
        }

        wasFocus = cameraMap.isFocus;
    }
}