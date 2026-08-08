using UnityEngine;
using UnityEngine.UIElements;
using Firebase.Extensions;

public class LoginUI : MonoBehaviour
{
    private UIDocument ui;

    // LOGIN
    private TextField emailInput;
    private TextField passwordInput;
    private Button loginButton;
    private Button registerButton;
    private Label loginMessage;

    // LOGIN / REGISTER PANEL
    private VisualElement loginPanel;
    private VisualElement registerPanel;

    // MAIN MENU CANVAS
    public GameObject mainMenu;

    // REGISTER
    private TextField registerEmailInput;
    private TextField registerPasswordInput;
    private TextField confirmPasswordInput;

    private Button registerConfirmButton;
    private Button backLoginButton;

    private Label registerMessage;

    private void OnEnable()
    {
        ui = GetComponent<UIDocument>();

        if (ui == null)
        {
            Debug.LogError("Không tìm thấy UIDocument!");
            return;
        }

        VisualElement root = ui.rootVisualElement;

        // LOGIN
        emailInput = root.Q<TextField>("emailInput");
        passwordInput = root.Q<TextField>("passwordInput");

        loginButton = root.Q<Button>("loginButton");
        registerButton = root.Q<Button>("registerButton");

        loginMessage = root.Q<Label>("loginMessage");

        // PANEL
        loginPanel = root.Q<VisualElement>("loginPanel");
        registerPanel = root.Q<VisualElement>("registerPanel");

        // REGISTER
        registerEmailInput = root.Q<TextField>("registerEmailInput");
        registerPasswordInput = root.Q<TextField>("registerPasswordInput");
        confirmPasswordInput = root.Q<TextField>("confirmPasswordInput");

        registerConfirmButton = root.Q<Button>("registerConfirmButton");
        backLoginButton = root.Q<Button>("backLoginButton");

        registerMessage = root.Q<Label>("registerMessage");

        // PASSWORD
        if (passwordInput != null)
            passwordInput.isPasswordField = true;

        if (registerPasswordInput != null)
            registerPasswordInput.isPasswordField = true;

        if (confirmPasswordInput != null)
            confirmPasswordInput.isPasswordField = true;

        // BUTTON
        if (loginButton != null)
            loginButton.clicked += Login;

        if (registerButton != null)
            registerButton.clicked += OpenRegister;

        if (registerConfirmButton != null)
            registerConfirmButton.clicked += Register;

        if (backLoginButton != null)
            backLoginButton.clicked += BackToLogin;

        // BAN ĐẦU
        if (loginPanel != null)
            loginPanel.style.display = DisplayStyle.Flex;

        if (registerPanel != null)
            registerPanel.style.display = DisplayStyle.None;

        // ẨN MAIN MENU
        if (mainMenu != null)
            mainMenu.SetActive(false);
    }

    private void OnDisable()
    {
        if (loginButton != null)
            loginButton.clicked -= Login;

        if (registerButton != null)
            registerButton.clicked -= OpenRegister;

        if (registerConfirmButton != null)
            registerConfirmButton.clicked -= Register;

        if (backLoginButton != null)
            backLoginButton.clicked -= BackToLogin;
    }

    // =========================
    // LOGIN
    // =========================

    private void Login()
    {
        string email = emailInput.value.Trim();
        string password = passwordInput.value;

        if (string.IsNullOrEmpty(email))
        {
            ShowLoginMessage("Vui lòng nhập Email.");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowLoginMessage("Vui lòng nhập mật khẩu.");
            return;
        }

        if (FirebaseManager.Instance == null)
        {
            ShowLoginMessage("Không tìm thấy FirebaseManager.");
            return;
        }

        if (!FirebaseManager.Instance.IsReady)
        {
            ShowLoginMessage("Firebase chưa sẵn sàng.");
            return;
        }

        ShowLoginMessage("Đang đăng nhập...");

        FirebaseManager.Instance.Auth
            .SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError(task.Exception);

                    ShowLoginMessage("Email hoặc mật khẩu không đúng.");
                    return;
                }

                Debug.Log("ĐĂNG NHẬP THÀNH CÔNG!");

                // ẨN LOGIN
                if (loginPanel != null)
                    loginPanel.style.display = DisplayStyle.None;

                // ẨN REGISTER
                if (registerPanel != null)
                    registerPanel.style.display = DisplayStyle.None;

                // HIỆN MAIN MENU CANVAS
                if (mainMenu != null)
                {
                    mainMenu.SetActive(true);

                    Debug.Log("ĐÃ HIỆN MAIN MENU!");
                }
                else
                {
                    Debug.LogError("CHƯA GÁN MAIN MENU!");
                }
            });
    }

    // =========================
    // MỞ REGISTER
    // =========================

    private void OpenRegister()
    {
        if (loginPanel != null)
            loginPanel.style.display = DisplayStyle.None;

        if (registerPanel != null)
            registerPanel.style.display = DisplayStyle.Flex;

        if (registerMessage != null)
            registerMessage.text = "";
    }

    // =========================
    // REGISTER
    // =========================

    private void Register()
    {
        string email = registerEmailInput.value.Trim();
        string password = registerPasswordInput.value;
        string confirmPassword = confirmPasswordInput.value;

        if (string.IsNullOrEmpty(email))
        {
            ShowRegisterMessage("Vui lòng nhập Email.");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowRegisterMessage("Vui lòng nhập mật khẩu.");
            return;
        }

        if (password.Length < 6)
        {
            ShowRegisterMessage("Mật khẩu phải có ít nhất 6 ký tự.");
            return;
        }

        if (password != confirmPassword)
        {
            ShowRegisterMessage("Mật khẩu nhập lại không khớp.");
            return;
        }

        if (FirebaseManager.Instance == null)
        {
            ShowRegisterMessage("Không tìm thấy FirebaseManager.");
            return;
        }

        if (!FirebaseManager.Instance.IsReady)
        {
            ShowRegisterMessage("Firebase chưa sẵn sàng.");
            return;
        }

        ShowRegisterMessage("Đang tạo tài khoản...");

        FirebaseManager.Instance.Auth
            .CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    ShowRegisterMessage("Không thể tạo tài khoản.");

                    Debug.LogError(task.Exception);

                    return;
                }

                Debug.Log("ĐĂNG KÝ THÀNH CÔNG!");

                ShowRegisterMessage("Đăng ký thành công!");

                registerEmailInput.value = "";
                registerPasswordInput.value = "";
                confirmPasswordInput.value = "";

                Invoke(nameof(BackToLogin), 1.5f);
            });
    }

    // =========================
    // QUAY LẠI LOGIN
    // =========================

    private void BackToLogin()
    {
        if (registerPanel != null)
            registerPanel.style.display = DisplayStyle.None;

        if (loginPanel != null)
            loginPanel.style.display = DisplayStyle.Flex;

        if (loginMessage != null)
            loginMessage.text = "";
    }

    // =========================
    // MESSAGE
    // =========================

    private void ShowLoginMessage(string message)
    {
        if (loginMessage != null)
            loginMessage.text = message;
    }

    private void ShowRegisterMessage(string message)
    {
        if (registerMessage != null)
            registerMessage.text = message;
    }
}