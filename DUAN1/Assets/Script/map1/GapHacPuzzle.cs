using UnityEngine;
using System.Collections.Generic;

public class GapHac : MonoBehaviour
{
    [Header("Click Order")]
    public List<int> clickOrder = new List<int>();

    //==================================================
    // STEP 1
    //==================================================

    [Header("Step 1")]
    public GameObject step1Deactivate;
    public GameObject step1Activate;

    //==================================================
    // STEP 2
    //==================================================

    [Header("Step 2")]
    public GameObject step2Deactivate;
    public GameObject step2Activate;

    //==================================================
    // STEP 3
    //==================================================

    [Header("Step 3")]
    public GameObject step3Deactivate;
    public GameObject step3Activate;

    //==================================================
    // STEP 4
    //==================================================

    [Header("Step 4")]
    public GameObject step4Deactivate;

    //==================================================
    // SUCCESS
    //==================================================

    [Header("Success")]
    public GameObject conhac;
    public GameObject conhacreal;

    public GameObject successDeactivate1;
    public GameObject successDeactivate2;

    //==================================================
    // FAIL
    //==================================================

    [Header("Fail")]
    public GameObject trash;

    //==================================================
    // RESET
    //==================================================

    [Header("Reset - Active Again")]
    public GameObject[] resetActiveObjects;

    [Header("Reset - Deactive Again")]
    public GameObject[] resetDeactivateObjects;

    //==================================================
    // CLICK STEP
    //==================================================

    public void ClickStep(int stepID)
    {
        // Đã bấm đủ 4 lần thì không cho bấm tiếp
        if (clickOrder.Count >= 4)
            return;

        // Lưu thứ tự click
        clickOrder.Add(stepID);

        // Thực hiện action của step
        switch (stepID)
        {
            case 1:
                ExecuteStep1();
                break;

            case 2:
                ExecuteStep2();
                break;

            case 3:
                ExecuteStep3();
                break;

            case 4:
                ExecuteStep4();
                break;
        }

        // Chỉ check sau khi đã bấm đủ 4 lần
        if (clickOrder.Count == 4)
        {
            CheckResult();
        }
    }

    //==================================================
    // STEP 1
    //==================================================

    void ExecuteStep1()
    {
        SetActive(step1Deactivate, false);
        SetActive(step1Activate, true);
    }

    //==================================================
    // STEP 2
    //==================================================

    void ExecuteStep2()
    {
        SetActive(step2Deactivate, false);
        SetActive(step2Activate, true);
    }

    //==================================================
    // STEP 3
    //==================================================

    void ExecuteStep3()
    {
        SetActive(step3Deactivate, false);
        SetActive(step3Activate, true);
    }

    //==================================================
    // STEP 4
    //==================================================

    void ExecuteStep4()
    {
        SetActive(step4Deactivate, false);
    }

    //==================================================
    // CHECK RESULT
    //==================================================

    void CheckResult()
    {
        bool isCorrect =
            clickOrder[0] == 1 &&
            clickOrder[1] == 2 &&
            clickOrder[2] == 3 &&
            clickOrder[3] == 4;

        if (isCorrect)
        {
            Success();
        }
        else
        {
            Fail();
        }
    }

    //==================================================
    // SUCCESS
    //==================================================

    void Success()
    {
        Debug.Log("Correct Order!");

        SetActive(conhac, true);
        SetActive(conhacreal, true);

        SetActive(successDeactivate1, false);
        SetActive(successDeactivate2, false);
    }

    //==================================================
    // FAIL
    //==================================================

    void Fail()
    {
        Debug.Log("Wrong Order!");

        SetActive(trash, true);
    }

    //==================================================
    // RESET PUZZLE
    //==================================================

    public void ResetPuzzle()
    {
        // Reset thứ tự
        clickOrder.Clear();

        // Tắt trash
        SetActive(trash, false);

        // Active lại các object ban đầu
        foreach (GameObject obj in resetActiveObjects)
        {
            SetActive(obj, true);
        }

        // Tắt các object đã được active trong step
        foreach (GameObject obj in resetDeactivateObjects)
        {
            SetActive(obj, false);
        }

        Debug.Log("Puzzle Reset!");
    }

    //==================================================
    // HELPER
    //==================================================

    void SetActive(GameObject obj, bool value)
    {
        if (obj != null)
            obj.SetActive(value);
    }
}