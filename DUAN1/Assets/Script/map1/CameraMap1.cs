using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMap1 : MonoBehaviour
{
    [Header("Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    [Header("Fade")]
    public GameObject fadePanel;
    public float fadeTime = 0.4f;

    [Header("Rotation")]
    public float rotateSpeed = 8f;
    public float ceilingAngle = -80f;

    float currentY;
    float wallAngle;
    bool lookingCeiling;
    bool isChanging;
    public bool isFocus;
    public GameObject currentPopup;
    Quaternion targetRotation;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool hasSavedPosition = false;

    void Start()
    {
        Vector3 rot = transform.eulerAngles;
        wallAngle = rot.x;
        currentY = rot.y;
        targetRotation = transform.rotation;

        if (fadePanel) fadePanel.SetActive(false);

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        upButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(false);

        downButton.onClick.RemoveAllListeners();
        downButton.onClick.AddListener(LookDown);
    }

    void Update()
    {
        if (!isFocus)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotateSpeed);
        }

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            isChanging = false;
    }

    public void RotateLeft()
    {
        if (isChanging || isFocus) return;
        currentY -= 90;
        StartCoroutine(ChangeRotation());
    }

    public void RotateRight()
    {
        if (isChanging || isFocus) return;
        currentY += 90;
        StartCoroutine(ChangeRotation());
    }

    public void LookUp()
    {
        if (isChanging || isFocus) return;
        lookingCeiling = true;
        StartCoroutine(ChangeRotation());
    }

    public void LookDown()
    {
        if (isChanging || isFocus) return;
        lookingCeiling = false;
        StartCoroutine(ChangeRotation());
    }

    IEnumerator ChangeRotation()
    {
        isChanging = true;

        if (fadePanel) fadePanel.SetActive(true);
        yield return new WaitForSeconds(fadeTime);

        float x = lookingCeiling ? ceilingAngle : wallAngle;
        targetRotation = Quaternion.Euler(x, currentY, 0);

        yield return new WaitForSeconds(0.05f);

        if (fadePanel) fadePanel.SetActive(false);

        upButton.gameObject.SetActive(!lookingCeiling);
        downButton.gameObject.SetActive(lookingCeiling);

        if (lookingCeiling)
        {
            downButton.onClick.RemoveAllListeners();
            downButton.onClick.AddListener(LookDown);
        }

        isChanging = false;
    }

    public void Focus(Transform point)
    {
        if (isChanging)
            return;

        StartCoroutine(FocusRoutine(point));
    }

    IEnumerator FocusRoutine(Transform point)
    {
        isChanging = true;

        if (fadePanel)
            fadePanel.SetActive(true);

        yield return new WaitForSeconds(fadeTime);

        // Chỉ lưu vị trí ban đầu đúng 1 lần
        if (!hasSavedPosition)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            hasSavedPosition = true;
        }

        transform.position = point.position;
        transform.rotation = point.rotation;

        isFocus = true;

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(true);

        downButton.onClick.RemoveAllListeners();
        downButton.onClick.AddListener(Back);

        yield return new WaitForSeconds(0.05f);

        if (fadePanel)
            fadePanel.SetActive(false);

        isChanging = false;
    }

    public void Back()
    {
        if (isChanging || !isFocus) return;
        StartCoroutine(BackRoutine());
    }

    IEnumerator BackRoutine()
    {
        isChanging = true;

        if (fadePanel)
            fadePanel.SetActive(true);

        yield return new WaitForSeconds(fadeTime);

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Vector3 rot = transform.eulerAngles;

        wallAngle = rot.x;
        currentY = rot.y;
        targetRotation = transform.rotation;

        isFocus = false;
        hasSavedPosition = false;

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        upButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(false);

        downButton.onClick.RemoveAllListeners();
        downButton.onClick.AddListener(LookDown);

        yield return new WaitForSeconds(0.05f);

        if (fadePanel)
            fadePanel.SetActive(false);

        isChanging = false;
    }
    public void OpenPopup(GameObject popup)
    {
        if (popup == null)
            return;

        currentPopup = popup;

        popup.SetActive(true);

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);

        downButton.gameObject.SetActive(true);

        downButton.onClick.RemoveAllListeners();
        downButton.onClick.AddListener(ClosePopup);
    }
    public void ClosePopup()
    {
        if (currentPopup != null)
        {
            currentPopup.SetActive(false);
            currentPopup = null;
        }

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);

        downButton.gameObject.SetActive(true);

        downButton.onClick.RemoveAllListeners();
        downButton.onClick.AddListener(Back);
    }
}
