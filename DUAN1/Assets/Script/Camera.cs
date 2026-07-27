using UnityEngine;
using System.Collections;
public class Camera : MonoBehaviour
{
    public GameObject upButton;
    public GameObject downButton;
    public GameObject fadePanel;

    public float rotateSpeed = 8f;
    public float fadeTime = 0.4f;

    public float ceilingAngle = -80f;

    private float currentY;
    private float wallAngle;
    private bool lookingCeiling;
    private bool isChanging;

    private Quaternion targetRotation;


    void Start()
    {
        // Lấy góc camera hiện tại trong Inspector
        Vector3 rot = transform.eulerAngles;

        wallAngle = rot.x;
        currentY = rot.y;

        targetRotation = transform.rotation;


        if (fadePanel != null)
            fadePanel.SetActive(false);

        if (upButton != null)
            upButton.SetActive(true);

        if (downButton != null)
            downButton.SetActive(false);
    }


    void Update()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );


        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            isChanging = false;
    }


    public void RotateLeft()
    {
        if (isChanging) return;

        currentY -= 90;
        ChangeCamera();
    }


    public void RotateRight()
    {
        if (isChanging) return;

        currentY += 90;
        ChangeCamera();
    }


    public void LookUp()
    {
        if (isChanging) return;

        lookingCeiling = true;
        ChangeCamera();
    }


    public void LookDown()
    {
        if (isChanging) return;

        lookingCeiling = false;
        ChangeCamera();
    }


    void ChangeCamera()
    {
        StartCoroutine(ChangeCameraRoutine());
    }


    IEnumerator ChangeCameraRoutine()
    {
        isChanging = true;


        if (fadePanel != null)
            fadePanel.SetActive(true);


        yield return new WaitForSeconds(fadeTime);


        float x = lookingCeiling ? ceilingAngle : wallAngle;


        targetRotation = Quaternion.Euler(
            x,
            currentY,
            0
        );


        yield return new WaitForSeconds(0.05f);


        if (fadePanel != null)
            fadePanel.SetActive(false);


        if (upButton != null)
            upButton.SetActive(!lookingCeiling);

        if (downButton != null)
            downButton.SetActive(lookingCeiling);
    }
}
