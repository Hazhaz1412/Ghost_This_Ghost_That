using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Nakama;

public class NakamaManager : MonoBehaviour
{
    private const string SessionPrefName = "nakama.session";
    private const string DeviceIdPrefName = "nakama.deviceid";

    private static NakamaManager _instance;
    public static NakamaManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("[NakamaManager]");
                _instance = go.AddComponent<NakamaManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [SerializeField] private Button deviceButton;
    [SerializeField] private Button googleButton;
    [SerializeField] private MainManager mainManager;
    [SerializeField] private string scheme = "http";
    [SerializeField] private int port = 7350;
    [SerializeField] private string serverKey = "defaultkey";

    private string serverHost = "127.0.0.1";
    
    public IClient Client { get; private set; }
    public ISocket Socket { get; private set; }
    public ISession Session { get; private set; }
    
    public bool IsAuthenticated => Session != null && !Session.IsExpired;
    public string UserId => Session?.UserId;
    public string Username => Session?.Username;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        LoadEnvironmentVariables();
        InitializeClient();
        
        RestoreSession();
        
        RegisterButtonEvents(); 
        TryAutoLogin();
    }

    private void RegisterButtonEvents()
    {
        if (deviceButton != null)
        {
            deviceButton.onClick.AddListener(OnDeviceButtonClicked);
        }
        
        if (googleButton != null)
        { 
            Debug.Log("Google button registered");
        }
    }
 
    private async void OnDeviceButtonClicked()
    {
         
        if (deviceButton != null) deviceButton.interactable = false;
        if (googleButton != null) googleButton.interactable = false;
        
        bool success = await AuthenticateDevice();
         
        if (deviceButton != null) deviceButton.interactable = true;
        if (googleButton != null) googleButton.interactable = true;
        
        if (success)
        {
            ShowLoginSuccess();
        }
        else
        {
            Debug.Log("fail");
        }
    }

    private void ShowLoginSuccess()
    {
        Debug.Log("========== ĐĂNG NHẬP THÀNH CÔNG ==========");
        Debug.Log($"User ID: {Session.UserId}");
        Debug.Log($"Username: {Session.Username}");
        Debug.Log($"Auth Token: {Session.AuthToken}");
        Debug.Log($"Refresh Token: {Session.RefreshToken}");
        Debug.Log($"Created: {Session.Created}");
        Debug.Log($"Expires: {Session.ExpireTime}");
        Debug.Log($"Is Expired: {Session.IsExpired}");
        Debug.Log($"Has Refresh Token: {!string.IsNullOrEmpty(Session.RefreshToken)}");
        
        if (Session.Vars != null && Session.Vars.Count > 0)
        { 
            foreach (var v in Session.Vars)
            {
                Debug.Log($"   - {v.Key}: {v.Value}");
            }
        }
        Debug.Log("============================================");
        
        SceneManager.LoadScene(nameof(SceneEnum.HuanSandbox));
    }
   
    private void LoadEnvironmentVariables()
    {
        try
        {
            string envPath = Path.Combine(Application.dataPath, "Scripts", ".env");
            
            if (File.Exists(envPath))
            {
                string[] lines = File.ReadAllLines(envPath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    var parts = line.Split('=');
                    if (parts.Length >= 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (key == "API")
                        {
                            serverHost = value;
                        }
                    }
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void InitializeClient()
    {
        Client = new Client(scheme, serverHost, port, serverKey);
        
        Socket = Nakama.Socket.From(Client);
    }

    private void RestoreSession()
    {
        try
        {
            string authToken = PlayerPrefs.GetString(SessionPrefName, "");
            if (!string.IsNullOrEmpty(authToken))
            {
                Session = Nakama.Session.Restore(authToken);
                
                if (Session != null && !Session.IsExpired)
                {
                    
                }
                else
                {
                    Session = null;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Err");
        }
    }

    private async void TryAutoLogin()
    {
        if (IsAuthenticated)
        {
            await ConnectSocket();
        }
        else
        {
            if (mainManager != null)
            {
                mainManager.SetState(MainManager.MainMenuPanel.Active);
            }
        }
    }

    private void SaveSession()
    {
        if (Session != null)
        {
            PlayerPrefs.SetString(SessionPrefName, Session.AuthToken);
            PlayerPrefs.Save();
        }
    }
 
    public void Logout()
    { 
         
        if (Socket != null && Socket.IsConnected)
        {
            Socket.CloseAsync();
        }
         
        Session = null;
        PlayerPrefs.DeleteKey(SessionPrefName);
        PlayerPrefs.Save();
        
        Debug.Log("đăng xuất thành công");
         
        if (mainManager != null)
        {
            mainManager.SetState(MainManager.MainMenuPanel.Active);
        }
    }

    private async Task EnsureUsername()
    {
        if (Session == null) return;
        
        if (string.IsNullOrEmpty(Session.Username))
        {
            string defaultUsername = "Player_" + Session.UserId.Substring(Session.UserId.Length - 6);
            await UpdateUsername(defaultUsername); 
        }
    }

    public async Task<bool> AuthenticateDevice()
    {
        try
        {
            string deviceId = PlayerPrefs.GetString(DeviceIdPrefName, "");
            if (string.IsNullOrEmpty(deviceId))
            {
                deviceId = SystemInfo.deviceUniqueIdentifier;
                PlayerPrefs.SetString(DeviceIdPrefName, deviceId);
                PlayerPrefs.Save();
            }
 

            Session = await Client.AuthenticateDeviceAsync(deviceId, null, true);
            SaveSession(); 
            
            await EnsureUsername();
            
            await ConnectSocket();
            
            return true;
        }
        catch (Exception ex)
        { 
            return false;
        }
    }
    public async Task<bool> RegisterEmail(string email, string password, string username = null)
    {
        try
        { 

            if (string.IsNullOrEmpty(username))
            {
                username = email.Split('@')[0]; 
            }

            Session = await Client.AuthenticateEmailAsync(email, password, username, true);
            SaveSession();
 
            
            await ConnectSocket();
            
            return true;
        }
        catch (Exception ex)
        { 
            return false;
        }
    }
    public async Task<bool> AuthenticateEmail(string email, string password)
    {
        try
        { 
            Session = await Client.AuthenticateEmailAsync(email, password, null, false);
            SaveSession();
            
            await ConnectSocket();
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> AuthenticateGoogle(string googleToken)
    {
        try
        {

            Session = await Client.AuthenticateGoogleAsync(googleToken);
            SaveSession();            
            await EnsureUsername();
            
            await ConnectSocket();
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> AuthenticateCustom(string customId)
    {
        try
        {

            Session = await Client.AuthenticateCustomAsync(customId);
            SaveSession();            
            await EnsureUsername();
            
            await ConnectSocket();
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public async Task<bool> ConnectSocket()
    {
        try
        {
            if (Session == null)
            {
                return false;
            }

            await Socket.ConnectAsync(Session);
            
            RegisterSocketEvents();
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task DisconnectSocket()
    {
        try
        {
            if (Socket != null)
            {
                await Socket.CloseAsync();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void RegisterSocketEvents()
    {
        Socket.ReceivedError += OnSocketError;
        Socket.Closed += OnSocketClosed;
        Socket.Connected += OnSocketConnected;
    }

    private void OnSocketConnected()
    {
    }

    private void OnSocketClosed(string closeReason)
    {
    }

    private void OnSocketError(Exception ex)
    {
    }



    public async Task<IApiAccount> GetAccount()
    {
        try
        {
            if (Session == null)
            {
                return null;
            }

            var account = await Client.GetAccountAsync(Session);
            return account;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> UpdateUsername(string newUsername)
    {
        try
        {
            if (Session == null)
            {
                return false;
            }

            await Client.UpdateAccountAsync(Session, newUsername);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    private void OnApplicationQuit()
    {
        Socket?.CloseAsync();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
