/* UI - Multiple Scenes Game */
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
class UI : MonoBehaviour
{
    public PlayerScr player;
    public Transform camTarget;
    Vector3 camOffset;
    float amplitude = 10, smoothIG = .01f, smoothIM = .1f;
    public GameObject blur;
    // Game State
    public bool isPaused, isMenued, isSettinged, isCredited;
    // UI Objects
    // | Layers
    GameObject Layer_Runtime, Layer_Pause, Layer_Menu, Layer_Settings, Layer_Credits;
    // | Buttons
    Button Button_Resume, Button_Retry, Button_Play, Button_Menu, Button_Quit_P, Button_Quit_M, Button_Settings_P, Button_Settings_M, Button_Credits;
    void Awake()
    {
        camOffset = Camera.main.transform.position;
        // UI Objects
        // | Layers
        Layer_Runtime = GameObject.Find("Layer_Runtime");
        Layer_Pause = GameObject.Find("Layer_Pause");
        Layer_Menu = GameObject.Find("Layer_Menu");
        Layer_Settings = GameObject.Find("Layer_Settings");
        Layer_Credits = GameObject.Find("Layer_Credits");
        // | Buttons
        Button_Resume = GameObject.Find("Button_Resume").GetComponent<Button>();
        Button_Retry = GameObject.Find("Button_Retry").GetComponent<Button>();
        Button_Play = GameObject.Find("Button_Play").GetComponent<Button>();
        Button_Menu = GameObject.Find("Button_Menu").GetComponent<Button>();
        Button_Quit_P = GameObject.Find("Button_Quit_P").GetComponent<Button>();
        Button_Quit_M = GameObject.Find("Button_Quit_M").GetComponent<Button>();
        Button_Settings_P = GameObject.Find("Button_Settings_P").GetComponent<Button>();
        Button_Settings_M = GameObject.Find("Button_Settings_M").GetComponent<Button>();
        Button_Credits = GameObject.Find("Button_Credits").GetComponent<Button>();
        // | Listeners
        Button_Resume.onClick.AddListener(SwitchIsPaused);
        Button_Retry.onClick.AddListener(Play);
        Button_Play.onClick.AddListener(Play);
        Button_Menu.onClick.AddListener(Menu);
        Button_Quit_P.onClick.AddListener(Application.Quit);
        Button_Quit_M.onClick.AddListener(Application.Quit);
        Button_Settings_P.onClick.AddListener(SwitchIsSettinged);
        Button_Settings_M.onClick.AddListener(SwitchIsSettinged);
        Button_Credits.onClick.AddListener(SwitchIsCredited);
    }
    void Update()
    {
        if (!isMenued) player.lockMov = isPaused;
        if (isPaused || isMenued) Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position, new Vector3(camTarget.position.x + camOffset.x +
                (Input.mousePosition.x - Screen.width / 2) / (amplitude * 1000),
                (Input.mousePosition.y - Screen.height / 2) / (amplitude * 1000), camOffset.z), smoothIM);
        if (!isPaused && !isMenued) Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position, camTarget.position + camOffset, smoothIG);
        if (!isMenued) blur.SetActive(isPaused);
        // Game State
        Time.timeScale = isPaused ? 0 : 1;
        Cursor.visible = isPaused ? true : isMenued ? true : false;
        if (!isMenued && !isSettinged && Input.GetKeyDown(KeyCode.Escape)) SwitchIsPaused();
        if (isSettinged && Input.GetKeyDown(KeyCode.Escape)) SwitchIsSettinged();
        if (isCredited && Input.GetKeyDown(KeyCode.Escape)) SwitchIsCredited();
        // UI Objects
        // | Layers
        Layer_Runtime.SetActive(!isPaused && !isMenued && !isSettinged && !isCredited);
        Layer_Pause.SetActive(isPaused && !isSettinged && !isCredited);
        Layer_Menu.SetActive(isMenued && !isSettinged && !isCredited);
        Layer_Settings.SetActive(isSettinged);
        Layer_Credits.SetActive(isCredited);
    }
    void SwitchIsPaused() { isPaused ^= true; }
    void Play() { SceneManager.LoadScene("Game"); }
    void Menu() { SceneManager.LoadScene("Menu"); }
    void SwitchIsSettinged() { isSettinged ^= true; }
    void SwitchIsCredited() { isCredited ^= true; }
}