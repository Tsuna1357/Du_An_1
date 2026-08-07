using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    [Header("Move Points")]
    public Transform[] movePoints;

    [Header("Button")]
    public GameObject moveButton;

    [Header("Fade")]
    public GameObject fadePanel;
    public float fadeTime = 0.4f;

    [Header("Move")]
    public float moveSpeed = 8f;

    private int currentIndex;
    private bool isChanging;
    private Vector3 targetPosition;

    void Start()
    {
        if (movePoints == null || movePoints.Length < 2)
            return;

        targetPosition = transform.position;

        // Xác định camera đang ở A hay B
        if (Vector3.Distance(transform.position, movePoints[0].position) <
            Vector3.Distance(transform.position, movePoints[1].position))
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex = 1;
        }

        if (fadePanel != null)
            fadePanel.SetActive(false);

        UpdateMoveButton();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isChanging = false;

            // Luôn cập nhật vị trí hiện tại
            if (Vector3.Distance(transform.position, movePoints[0].position) <
                Vector3.Distance(transform.position, movePoints[1].position))
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = 1;
            }
        }

        UpdateMoveButton();
    }

    public void MoveForward()
    {
        Debug.Log("MoveForward");
        if (isChanging)
            return;

        if (movePoints == null || movePoints.Length < 2)
            return;

        // Xác định lại camera đang đứng ở đâu
        if (Vector3.Distance(transform.position, movePoints[0].position) <
            Vector3.Distance(transform.position, movePoints[1].position))
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex = 1;
        }

        int dir = GetDirection();

        // A -> B
        if (currentIndex == 0 && dir == 1)
        {
            currentIndex = 1;
        }
        // B -> A
        else if (currentIndex == 1 && dir == -1)
        {
            currentIndex = 0;
        }
        else
        {
            return;
        }

        StartCoroutine(ChangePosition());
    }

    IEnumerator ChangePosition()
    {
        isChanging = true;

        if (fadePanel != null)
            fadePanel.SetActive(true);

        yield return new WaitForSeconds(fadeTime);

        targetPosition = movePoints[currentIndex].position;

        if (fadePanel != null)
            fadePanel.SetActive(false);
    }

    // 1 = nhìn về B (180°)
    // -1 = nhìn về A (0°)
    // 0 = hướng khác
    int GetDirection()
    {
        float y = transform.eulerAngles.y;

        if (Mathf.Abs(Mathf.DeltaAngle(y, 180f)) < 2f)
            return 1;

        if (Mathf.Abs(Mathf.DeltaAngle(y, 0f)) < 2f)
            return -1;

        return 0;
    }

    void UpdateMoveButton()
    {
        if (moveButton == null)
            return;

        if (movePoints == null || movePoints.Length < 2)
        {
            moveButton.SetActive(false);
            return;
        }

        int dir = GetDirection();

        if (currentIndex == 0)
        {
            // Chỉ hiện khi ở A và quay sang B (180°)
            moveButton.SetActive(dir == 1);
        }
        else
        {
            // Chỉ hiện khi ở B và quay về A (0°)
            moveButton.SetActive(dir == -1);
        }
    }
}