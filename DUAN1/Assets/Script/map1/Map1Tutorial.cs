using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Map1Tutorial : MonoBehaviour
{
    [Header("Camera")]
    public CameraMap1 cameraMap;

    //====================================================
    // UI GAME
    //====================================================

    [Header("Game UI")]
    public GameObject inventoryUI;

    [Tooltip("Toàn bộ UI game sẽ bị tắt khi tutorial bắt đầu")]
    public GameObject[] gameUI;

    //====================================================
    // CAMERA BUTTONS
    //====================================================

    [Header("Camera Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    //====================================================
    // CLOTH TUTORIAL
    //====================================================

    [Header("Cloth")]
    public GameObject cloth;

    public Transform clothEndPoint;

    public float clothMoveTime = 0.5f;

    [Header("Cloth Tutorial UI")]
    public GameObject clothArrow;
    public GameObject clothClickArea;
    public TMP_Text clothText;

    //====================================================
    // ROTATION TUTORIAL
    //====================================================

    [Header("Turn Tutorial")]
    public GameObject turnLeftArrow;
    public GameObject turnRightArrow;

    public TMP_Text turnText;

    //====================================================
    // PAINTING TUTORIAL
    //====================================================

    [Header("Painting Tutorial")]
    public GameObject paintingArrow;
    public GameObject paintingClickArea;
    public TMP_Text paintingText;

    //====================================================
    // STORY
    //====================================================

    [Header("Story")]
    public TMP_Text storyText;

    //====================================================
    // SCISSORS TUTORIAL
    //====================================================

    [Header("Scissors")]
    public GameObject scissorsArrow;
    public GameObject scissorsClickArea;
    public TMP_Text scissorsText;

    public ItemData scissorsItem;

    //====================================================
    // STATE
    //====================================================

    private enum TutorialState
    {
        Cloth,
        Turn,
        TurnBack,
        Painting,
        Story,
        Scissors,
        Complete
    }

    private TutorialState currentState;

    private bool turnedLeft;
    private bool turnedRight;

    //====================================================
    // START
    //====================================================

    void Start()
    {
        StartTutorial();
    }

    public void StartTutorial()
    {
        currentState = TutorialState.Cloth;

        // Tắt inventory
        if (inventoryUI != null)
            inventoryUI.SetActive(false);

        // Tắt UI game
        foreach (GameObject ui in gameUI)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        // Tắt toàn bộ nút camera
        SetCameraButtons(false, false, false, false);

        // Ẩn toàn bộ tutorial UI trước
        HideAllTutorialUI();

        // Hiện tutorial khăn
        ShowClothTutorial();
    }

    //====================================================
    // CAMERA BUTTON CONTROL
    //====================================================

    void SetCameraButtons(
        bool left,
        bool right,
        bool up,
        bool down)
    {
        if (leftButton != null)
            leftButton.gameObject.SetActive(left);

        if (rightButton != null)
            rightButton.gameObject.SetActive(right);

        if (upButton != null)
            upButton.gameObject.SetActive(up);

        if (downButton != null)
            downButton.gameObject.SetActive(down);
    }

    //====================================================
    // HIDE ALL TUTORIAL UI
    //====================================================

    void HideAllTutorialUI()
    {
        if (clothArrow != null)
            clothArrow.SetActive(false);

        if (clothClickArea != null)
            clothClickArea.SetActive(false);

        if (clothText != null)
            clothText.gameObject.SetActive(false);

        if (turnLeftArrow != null)
            turnLeftArrow.SetActive(false);

        if (turnRightArrow != null)
            turnRightArrow.SetActive(false);

        if (turnText != null)
            turnText.gameObject.SetActive(false);

        if (paintingArrow != null)
            paintingArrow.SetActive(false);

        if (paintingClickArea != null)
            paintingClickArea.SetActive(false);

        if (paintingText != null)
            paintingText.gameObject.SetActive(false);

        if (storyText != null)
            storyText.gameObject.SetActive(false);

        if (scissorsArrow != null)
            scissorsArrow.SetActive(false);

        if (scissorsClickArea != null)
            scissorsClickArea.SetActive(false);

        if (scissorsText != null)
            scissorsText.gameObject.SetActive(false);
    }

    //====================================================
    // STEP 1 - CLOTH
    //====================================================

    void ShowClothTutorial()
    {
        if (clothArrow != null)
            clothArrow.SetActive(true);

        if (clothClickArea != null)
            clothClickArea.SetActive(true);

        if (clothText != null)
        {
            clothText.text = "CLICK HERE";
            clothText.gameObject.SetActive(true);
        }
    }

    public void ClickCloth()
    {
        if (currentState != TutorialState.Cloth)
            return;

        StartCoroutine(RemoveCloth());
    }

    IEnumerator RemoveCloth()
    {
        HideClothTutorial();

        if (cloth == null)
            yield break;

        Vector3 startPosition = cloth.transform.position;
        Vector3 endPosition;

        if (clothEndPoint != null)
            endPosition = clothEndPoint.position;
        else
            endPosition = startPosition + Vector3.up * 3f;

        float time = 0;

        while (time < clothMoveTime)
        {
            time += Time.deltaTime;

            cloth.transform.position =
                Vector3.Lerp(
                    startPosition,
                    endPosition,
                    time / clothMoveTime
                );

            yield return null;
        }

        cloth.SetActive(false);

        currentState = TutorialState.Turn;

        ShowTurnTutorial();
    }

    void HideClothTutorial()
    {
        if (clothArrow != null)
            clothArrow.SetActive(false);

        if (clothClickArea != null)
            clothClickArea.SetActive(false);

        if (clothText != null)
            clothText.gameObject.SetActive(false);
    }

    //====================================================
    // STEP 2 - TURN
    //====================================================

    void ShowTurnTutorial()
    {
        SetCameraButtons(true, true, false, false);

        if (turnText != null)
        {
            turnText.text = "TURN AROUND";
            turnText.gameObject.SetActive(true);
        }

        if (turnLeftArrow != null)
            turnLeftArrow.SetActive(true);

        if (turnRightArrow != null)
            turnRightArrow.SetActive(true);
    }

    //====================================================
    // PLAYER TURN LEFT
    //====================================================

    public void TurnLeft()
    {
        if (currentState != TutorialState.Turn)
            return;

        turnedLeft = true;

        currentState = TutorialState.TurnBack;

        if (turnLeftArrow != null)
            turnLeftArrow.SetActive(false);

        if (turnRightArrow != null)
            turnRightArrow.SetActive(true);

        if (turnText != null)
            turnText.text = "TURN BACK";

        // Chỉ còn nút phải
        SetCameraButtons(false, true, false, false);
    }

    //====================================================
    // PLAYER TURN RIGHT
    //====================================================

    public void TurnRight()
    {
        if (currentState != TutorialState.Turn)
            return;

        turnedRight = true;

        currentState = TutorialState.TurnBack;

        if (turnRightArrow != null)
            turnRightArrow.SetActive(false);

        if (turnLeftArrow != null)
            turnLeftArrow.SetActive(true);

        if (turnText != null)
            turnText.text = "TURN BACK";

        // Chỉ còn nút trái
        SetCameraButtons(true, false, false, false);
    }

    //====================================================
    // PLAYER TURN BACK
    //====================================================

    public void TurnBack()
    {
        if (currentState != TutorialState.TurnBack)
            return;

        if (turnLeftArrow != null)
            turnLeftArrow.SetActive(false);

        if (turnRightArrow != null)
            turnRightArrow.SetActive(false);

        if (turnText != null)
            turnText.gameObject.SetActive(false);

        currentState = TutorialState.Painting;

        SetCameraButtons(false, false, false, false);

        ShowPaintingTutorial();
    }

    //====================================================
    // STEP 3 - PAINTING
    //====================================================

    void ShowPaintingTutorial()
    {
        if (paintingArrow != null)
            paintingArrow.SetActive(true);

        if (paintingClickArea != null)
            paintingClickArea.SetActive(true);

        if (paintingText != null)
        {
            paintingText.text = "CLICK HERE";
            paintingText.gameObject.SetActive(true);
        }
    }

    public void ClickPainting(Transform focusPoint)
    {
        if (currentState != TutorialState.Painting)
            return;

        if (paintingArrow != null)
            paintingArrow.SetActive(false);

        if (paintingClickArea != null)
            paintingClickArea.SetActive(false);

        if (paintingText != null)
            paintingText.gameObject.SetActive(false);

        // Focus vào tranh
        cameraMap.Focus(focusPoint);

        currentState = TutorialState.Story;

        ShowStory();
    }

    //====================================================
    // STEP 4 - STORY
    //====================================================

    void ShowStory()
    {
        if (storyText != null)
            storyText.gameObject.SetActive(true);
    }

    // Hàm này gọi bằng Button / click để tắt story
    public void CloseStory()
    {
        if (currentState != TutorialState.Story)
            return;

        if (storyText != null)
            storyText.gameObject.SetActive(false);

        currentState = TutorialState.Scissors;

        ShowScissorsTutorial();
    }

    //====================================================
    // STEP 5 - SCISSORS
    //====================================================

    void ShowScissorsTutorial()
    {
        if (scissorsArrow != null)
            scissorsArrow.SetActive(true);

        if (scissorsClickArea != null)
            scissorsClickArea.SetActive(true);

        if (scissorsText != null)
        {
            scissorsText.text = "CLICK HERE";
            scissorsText.gameObject.SetActive(true);
        }
    }

    public void PickUpScissors()
    {
        if (currentState != TutorialState.Scissors)
            return;

        if (InventoryManager.Instance == null)
            return;

        // Nhặt kéo
        if (!InventoryManager.Instance.AddItem(scissorsItem))
            return;

        // Tắt tutorial kéo
        if (scissorsArrow != null)
            scissorsArrow.SetActive(false);

        if (scissorsClickArea != null)
            scissorsClickArea.SetActive(false);

        if (scissorsText != null)
            scissorsText.gameObject.SetActive(false);

        // Hiện inventory
        if (inventoryUI != null)
            inventoryUI.SetActive(true);

        // Hiện nút xuống để Back khỏi Focus
        SetCameraButtons(false, false, false, true);

        // Active lại toàn bộ UI game
        foreach (GameObject ui in gameUI)
        {
            if (ui != null)
                ui.SetActive(true);
        }

        currentState = TutorialState.Complete;

        CompleteTutorial();
    }

    //====================================================
    // COMPLETE
    //====================================================

    void CompleteTutorial()
    {
        Debug.Log("Map 1 Tutorial Complete");

        // Tắt toàn bộ GameObject chứa tutorial
        gameObject.SetActive(false);
    }
}