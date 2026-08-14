using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Map1Tutorial : MonoBehaviour
{
    [Header("Camera")]
    public CameraMap1 cameraMap;

    //==================================================
    // GAME UI
    //==================================================

    [Header("Inventory UI")]
    public GameObject inventoryUI;

    [Header("Other Game UI")]
    public GameObject[] gameUI;

    //==================================================
    // CAMERA BUTTONS
    //==================================================

    [Header("Camera Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button upButton;
    public Button downButton;

    //==================================================
    // CLOTH
    //==================================================

    [Header("Cloth")]
    public GameObject cloth;

    [Tooltip("Empty Object là vị trí cuối của khăn")]
    public Transform clothEndPoint;

    public float clothMoveTime = 0.5f;

    [Header("Cloth Tutorial UI")]
    public GameObject clothArrow;
    public GameObject clothClickFrame;
    public TMP_Text clothText;

    //==================================================
    // TURN
    //==================================================

    [Header("Turn Tutorial UI")]
    public GameObject leftArrow;
    public GameObject rightArrow;
    public TMP_Text turnText;

    //==================================================
    // PAINTING
    //==================================================

    [Header("Painting Tutorial UI")]
    public GameObject paintingArrow;
    public GameObject paintingClickFrame;
    public TMP_Text paintingText;

    //==================================================
    // STORY
    //==================================================

    [Header("Story")]
    [TextArea]
    public string storyMessage;

    public TMP_Text storyText;

    //==================================================
    // SCISSORS
    //==================================================

    [Header("Scissors")]
    public ItemData scissorsItem;

    public GameObject scissorsObject;

    [Header("Scissors Tutorial UI")]
    public GameObject scissorsArrow;
    public GameObject scissorsClickFrame;
    public TMP_Text scissorsText;

    //==================================================
    // STATE
    //==================================================

    public enum TutorialState
    {
        Cloth,
        Turn,
        TurnBack,
        Painting,
        Story,
        Scissors,
        Complete
    }

    [HideInInspector]
    public TutorialState currentState;

    private bool turnedLeft;

    //==================================================
    // START
    //==================================================

    void Start()
    {
        StartTutorial();
    }

    public void StartTutorial()
    {
        currentState = TutorialState.Cloth;

        // Tắt Inventory
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }

        // Tắt UI game khác
        foreach (GameObject ui in gameUI)
        {
            if (ui != null)
            {
                ui.SetActive(false);
            }
        }

        // Tắt nút camera
        SetCameraButtons(false, false, false, false);

        // Tắt toàn bộ tutorial UI
        HideAllTutorialUI();

        // Hiện tutorial khăn
        ShowClothTutorial();
    }

    //==================================================
    // CAMERA BUTTON
    //==================================================

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

    //==================================================
    // HIDE ALL TUTORIAL UI
    //==================================================

    void HideAllTutorialUI()
    {
        SetActive(clothArrow, false);
        SetActive(clothClickFrame, false);
        SetActiveText(clothText, false);

        SetActive(leftArrow, false);
        SetActive(rightArrow, false);
        SetActiveText(turnText, false);

        SetActive(paintingArrow, false);
        SetActive(paintingClickFrame, false);
        SetActiveText(paintingText, false);

        SetActiveText(storyText, false);

        SetActive(scissorsArrow, false);
        SetActive(scissorsClickFrame, false);
        SetActiveText(scissorsText, false);
    }

    //==================================================
    // STEP 1 - CLOTH
    //==================================================

    void ShowClothTutorial()
    {
        SetActive(clothArrow, true);
        SetActive(clothClickFrame, true);

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

        StartCoroutine(RemoveClothRoutine());
    }

    IEnumerator RemoveClothRoutine()
    {
        // Tắt UI hướng dẫn khăn
        SetActive(clothArrow, false);
        SetActive(clothClickFrame, false);
        SetActiveText(clothText, false);

        if (cloth != null)
        {
            Vector3 startPosition = cloth.transform.position;

            Vector3 endPosition;

            if (clothEndPoint != null)
                endPosition = clothEndPoint.position;
            else
                endPosition = startPosition + Vector3.up * 3f;

            float time = 0f;

            while (time < clothMoveTime)
            {
                time += Time.deltaTime;

                cloth.transform.position = Vector3.Lerp(
                    startPosition,
                    endPosition,
                    time / clothMoveTime
                );

                yield return null;
            }

            cloth.SetActive(false);
        }

        currentState = TutorialState.Turn;

        ShowTurnTutorial();
    }

    //==================================================
    // STEP 2 - TURN
    //==================================================

    void ShowTurnTutorial()
    {
        SetCameraButtons(true, true, false, false);

        SetActive(leftArrow, true);
        SetActive(rightArrow, true);

        if (turnText != null)
        {
            turnText.text = "TURN AROUND";
            turnText.gameObject.SetActive(true);
        }
    }

    public void TurnLeft()
    {
        if (currentState != TutorialState.Turn)
            return;

        turnedLeft = true;

        currentState = TutorialState.TurnBack;

        // Tắt hướng dẫn trái
        SetActive(leftArrow, false);

        // Hiện hướng dẫn phải
        SetActive(rightArrow, true);

        if (turnText != null)
            turnText.text = "TURN BACK";

        // Chỉ còn nút phải
        SetCameraButtons(false, true, false, false);
    }

    public void TurnRight()
    {
        if (currentState != TutorialState.Turn)
            return;

        turnedLeft = false;

        currentState = TutorialState.TurnBack;

        // Tắt hướng dẫn phải
        SetActive(rightArrow, false);

        // Hiện hướng dẫn trái
        SetActive(leftArrow, true);

        if (turnText != null)
            turnText.text = "TURN BACK";

        // Chỉ còn nút trái
        SetCameraButtons(true, false, false, false);
    }

    public void TurnBack()
    {
        if (currentState != TutorialState.TurnBack)
            return;

        SetActive(leftArrow, false);
        SetActive(rightArrow, false);
        SetActiveText(turnText, false);

        // Tắt camera button
        SetCameraButtons(false, false, false, false);

        currentState = TutorialState.Painting;

        ShowPaintingTutorial();
    }

    //==================================================
    // STEP 3 - PAINTING
    //==================================================

    void ShowPaintingTutorial()
    {
        SetActive(paintingArrow, true);
        SetActive(paintingClickFrame, true);

        if (paintingText != null)
        {
            paintingText.text = "CLICK HERE";
            paintingText.gameObject.SetActive(true);
        }
    }

    // Hàm được FocusPainting gọi
    public void PaintingFocused()
    {
        if (currentState != TutorialState.Painting)
            return;

        // Tắt hướng dẫn tranh
        SetActive(paintingArrow, false);
        SetActive(paintingClickFrame, false);
        SetActiveText(paintingText, false);

        currentState = TutorialState.Story;

        ShowStory();
    }

    //==================================================
    // STEP 4 - STORY
    //==================================================

    void ShowStory()
    {
        if (storyText == null)
            return;

        storyText.text = storyMessage;
        storyText.gameObject.SetActive(true);
    }

    public void CloseStory()
    {
        if (currentState != TutorialState.Story)
            return;

        SetActiveText(storyText, false);

        currentState = TutorialState.Scissors;

        ShowScissorsTutorial();
    }

    //==================================================
    // STEP 5 - SCISSORS
    //==================================================

    void ShowScissorsTutorial()
    {
        SetActive(scissorsArrow, true);
        SetActive(scissorsClickFrame, true);

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

        if (scissorsItem == null)
            return;

        // Add item vào Inventory
        bool success =
            InventoryManager.Instance.AddItem(scissorsItem);

        if (!success)
            return;

        // Xóa kéo trên map
        SetActive(scissorsObject, false);

        // Tắt UI hướng dẫn
        SetActive(scissorsArrow, false);
        SetActive(scissorsClickFrame, false);
        SetActiveText(scissorsText, false);

        // Hiện Inventory
        SetActive(inventoryUI, true);

        // Hiện nút Back
        SetCameraButtons(false, false, false, true);

        // Active lại UI game
        foreach (GameObject ui in gameUI)
        {
            SetActive(ui, true);
        }

        currentState = TutorialState.Complete;

        CompleteTutorial();
    }

    //==================================================
    // COMPLETE
    //==================================================

    void CompleteTutorial()
    {
        Debug.Log("Tutorial Complete");

        // Script tự dừng hoàn toàn
        gameObject.SetActive(false);
    }

    //==================================================
    // HELPER
    //==================================================

    void SetActive(GameObject obj, bool value)
    {
        if (obj != null)
            obj.SetActive(value);
    }

    void SetActiveText(TMP_Text text, bool value)
    {
        if (text != null)
            text.gameObject.SetActive(value);
    }
}