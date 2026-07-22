using UnityEngine;
using System.Collections;
public class Camera : MonoBehaviour
{
    // CÁC ĐIỂM NHÌN TƯỜNG
    public Transform frontView;
    public Transform rightView;
    public Transform backView;
    public Transform leftView;


    // CÁC ĐIỂM NHÌN TRẦN

    public Transform ceilingFront;
    public Transform ceilingRight;
    public Transform ceilingBack;
    public Transform ceilingLeft;


    // BUTTON UI

    public GameObject upButton;       // Nút nhìn lên
    public GameObject downButton;     // Nút nhìn xuống

    // FADE MÀN HÌNH
    public GameObject fadePanel;      // Panel màu đen phủ màn hình
    public float fadeTime = 0.4f;

    // TỐC ĐỘ XOAY CAMERA
    public float rotateSpeed = 10f;
    private Quaternion targetRotation;

    // Chống spam nút
    private bool isChanging = false;

    // TRẠNG THÁI CAMERA

    enum ViewState
    {
        Front,
        Right,
        Back,
        Left,
        CeilingFront,
        CeilingRight,
        CeilingBack,
        CeilingLeft
    }
    ViewState currentState;

    // KHỞI TẠO
    void Start()
    {

        // Camera bắt đầu nhìn phía trước
        targetRotation = frontView.rotation;
        currentState = ViewState.Front;



        // Hiện nút nhìn lên
        if (upButton != null)
            upButton.SetActive(true);



        // Ẩn nút nhìn xuống
        if (downButton != null)
            downButton.SetActive(false);



        // Tắt màn hình đen      
        if (fadePanel != null)
            fadePanel.SetActive(false);

    }

    // XOAY CAMERA MƯỢT

    void Update()
    {

        transform.rotation = Quaternion.Slerp(

            transform.rotation,

            targetRotation,

            Time.deltaTime * rotateSpeed

        );


        // Khi xoay xong cho phép bấm tiếp

        if (Quaternion.Angle(transform.rotation, targetRotation) < 8f)
        {
            isChanging = false;
        }

    }

    // QUAY TRÁI

    public void RotateLeft()
    {

        if (isChanging)
            return;


        switch (currentState)
        {

            // TƯỜNG

            case ViewState.Front:
                Change(leftView, ViewState.Left);
                break;


            case ViewState.Left:
                Change(backView, ViewState.Back);
                break;


            case ViewState.Back:
                Change(rightView, ViewState.Right);
                break;


            case ViewState.Right:
                Change(frontView, ViewState.Front);
                break;



            // TRẦN

            case ViewState.CeilingFront:
                Change(ceilingLeft, ViewState.CeilingLeft);
                break;


            case ViewState.CeilingLeft:
                Change(ceilingBack, ViewState.CeilingBack);
                break;


            case ViewState.CeilingBack:
                Change(ceilingRight, ViewState.CeilingRight);
                break;


            case ViewState.CeilingRight:
                Change(ceilingFront, ViewState.CeilingFront);
                break;

        }

    }

    // QUAY PHẢI

    public void RotateRight()
    {

        if (isChanging)
            return;


        switch (currentState)
        {

            // TƯỜNG

            case ViewState.Front:
                Change(rightView, ViewState.Right);
                break;


            case ViewState.Right:
                Change(backView, ViewState.Back);
                break;


            case ViewState.Back:
                Change(leftView, ViewState.Left);
                break;


            case ViewState.Left:
                Change(frontView, ViewState.Front);
                break;




            // TRẦN

            case ViewState.CeilingFront:
                Change(ceilingRight, ViewState.CeilingRight);
                break;


            case ViewState.CeilingRight:
                Change(ceilingBack, ViewState.CeilingBack);
                break;


            case ViewState.CeilingBack:
                Change(ceilingLeft, ViewState.CeilingLeft);
                break;


            case ViewState.CeilingLeft:
                Change(ceilingFront, ViewState.CeilingFront);
                break;

        }

    }

    // NHÌN LÊN TRẦN

    public void LookUp()
    {

        if (isChanging)
            return;


        switch (currentState)
        {

            case ViewState.Front:
                Change(ceilingFront, ViewState.CeilingFront);
                break;


            case ViewState.Right:
                Change(ceilingRight, ViewState.CeilingRight);
                break;


            case ViewState.Back:
                Change(ceilingBack, ViewState.CeilingBack);
                break;


            case ViewState.Left:
                Change(ceilingLeft, ViewState.CeilingLeft);
                break;

        }

    }

    // NHÌN XUỐNG LẠI TƯỜNG

    public void LookDown()
    {

        if (isChanging)
            return;


        switch (currentState)
        {

            case ViewState.CeilingFront:
                Change(frontView, ViewState.Front);
                break;



            case ViewState.CeilingRight:
                Change(rightView, ViewState.Right);
                break;



            case ViewState.CeilingBack:
                Change(backView, ViewState.Back);
                break;



            case ViewState.CeilingLeft:
                Change(leftView, ViewState.Left);
                break;

        }

    }

    // HÀM ĐỔI GÓC NHÌN

    void Change(Transform view, ViewState state)
    {

        if (view == null)
            return;



        StartCoroutine(ChangeCamera(view, state));

    }


    // FADE + ĐỔI CAMERA

    IEnumerator ChangeCamera(Transform view, ViewState state)
    {

        // Khóa nút

        isChanging = true;
        // Màn hình đen

        if (fadePanel != null)
            fadePanel.SetActive(true);


        yield return new WaitForSeconds(fadeTime);

        // Đổi hướng nhìn

        targetRotation = view.rotation;
        currentState = state;
        yield return new WaitForSeconds(0.05f);




        // Tắt màn hình đen

        if (fadePanel != null)
            fadePanel.SetActive(false);

        // ĐIỀU KHIỂN BUTTON

        bool lookingCeiling =

            state == ViewState.CeilingFront ||
            state == ViewState.CeilingRight ||
            state == ViewState.CeilingBack ||
            state == ViewState.CeilingLeft;



        if (lookingCeiling)
        {

            upButton.SetActive(false);
            downButton.SetActive(true);

        }
        else
        {

            upButton.SetActive(true);
            downButton.SetActive(false);

        }


    }
}
