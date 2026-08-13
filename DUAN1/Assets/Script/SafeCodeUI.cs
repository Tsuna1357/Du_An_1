using UnityEngine;
using TMPro;

public class SafeCodeUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text passwordDisplay;
    public GameObject passwordPanel;

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

        CheckCode();
    }

    private void CheckCode()
    {
        if (currentCode.Length < 6)
            return;

        if (currentCode == correctCode)
        {
            Debug.Log("MẬT MÃ ĐÚNG!");

            // Ẩn object cần mở/biến mất
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }

            // TẮT TOÀN BỘ UI NHẬP MẬT KHẨU
            if (passwordPanel != null)
            {
                passwordPanel.SetActive(false);
            }
            else
            {
                Debug.LogWarning("CHƯA GÁN PASSWORD PANEL!");
            }

            // Xóa mã đã nhập
            currentCode = "";
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