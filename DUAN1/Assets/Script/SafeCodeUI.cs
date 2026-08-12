using UnityEngine;
using TMPro;

public class SafeCodeUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text passwordDisplay;

    [Header("Password")]
    public string correctCode = "170463";

    [Header("Object khi nhập đúng")]
    public GameObject objectToHide;

    private string currentCode = "";

    private void Start()
    {
        currentCode = "";

        if (passwordDisplay != null)
        {
            passwordDisplay.text = "";
        }
    }

    public void AddDigit(int number)
    {
        Debug.Log("Đã bấm số: " + number);

        if (currentCode.Length >= 6)
            return;

        currentCode += number.ToString();

        if (passwordDisplay != null)
        {
            passwordDisplay.text = currentCode;
        }
        else
        {
            Debug.LogError("CHƯA GÁN PASSWORD DISPLAY!");
        }

        CheckCode();
    }

    private void CheckCode()
    {
        if (currentCode.Length < 6)
            return;

        // MẬT MÃ ĐÚNG
        if (currentCode == correctCode)
        {
            Debug.Log("MẬT MÃ ĐÚNG!");

            // Ẩn object được kéo từ Hierarchy
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }
            else
            {
                Debug.LogWarning("CHƯA GÁN OBJECT TO HIDE!");
            }

            // Đóng UI
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("MẬT MÃ SAI!");

            currentCode = "";

            if (passwordDisplay != null)
            {
                passwordDisplay.text = "";
            }
        }
    }

    public void ClearCode()
    {
        currentCode = "";

        if (passwordDisplay != null)
        {
            passwordDisplay.text = "";
        }
    }
}