using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMap2 : MonoBehaviour
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


    [Header("Map 2 Puzzle")]
    public CrowCage crowCage;



    float currentY;
    float wallAngle;

    bool lookingCeiling;
    bool isChanging;

    public bool isFocus;


    Quaternion targetRotation;


    Vector3 originalPosition;
    Quaternion originalRotation;


    bool hasSavedPosition;


    bool turnedAway;



    void Start()
    {
        Vector3 rot = transform.eulerAngles;

        wallAngle = rot.x;
        currentY = rot.y;

        targetRotation = transform.rotation;


        if (fadePanel)
            fadePanel.SetActive(false);



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
                Time.deltaTime * rotateSpeed
            );
        }


        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            isChanging = false;
        }

    }





    public void RotateLeft()
    {
        if (isChanging || isFocus)
            return;


        currentY -= 90;


        turnedAway = true;


        StartCoroutine(ChangeRotation());
    }




    public void RotateRight()
    {
        if (isChanging || isFocus)
            return;


        currentY += 90;


        turnedAway = true;


        StartCoroutine(ChangeRotation());
    }






    public void LookUp()
    {
        if (isChanging || isFocus)
            return;


        lookingCeiling = true;

        StartCoroutine(ChangeRotation());
    }






    public void LookDown()
    {
        if (isChanging || isFocus)
            return;


        lookingCeiling = false;

        StartCoroutine(ChangeRotation());
    }







    IEnumerator ChangeRotation()
    {

        isChanging = true;


        if (fadePanel)
            fadePanel.SetActive(true);


        yield return new WaitForSeconds(fadeTime);



        float x = lookingCeiling ? ceilingAngle : wallAngle;


        targetRotation = Quaternion.Euler(
            x,
            currentY,
            0
        );



        yield return new WaitForSeconds(0.05f);



        if (fadePanel)
            fadePanel.SetActive(false);



        upButton.gameObject.SetActive(!lookingCeiling);

        downButton.gameObject.SetActive(lookingCeiling);



        if (lookingCeiling)
        {
            downButton.onClick.RemoveAllListeners();
            downButton.onClick.AddListener(LookDown);
        }



        // ======================
        // QUẠ HỒI SINH
        // ======================

        if (turnedAway)
        {

            if (crowCage != null)
            {
                crowCage.Revive();
            }


            turnedAway = false;

        }



        isChanging = false;

    }







    // ======================
    // ZOOM
    // ======================

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

        if (isChanging || !isFocus)
            return;


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
}