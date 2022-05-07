using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField] int levelNumber;

    public TMP_Text countDownText;
    public Texture2D defaultCursor;

    static Menu gameOverMenu;
    static Menu escMenu;
    static Menu goalMenu;
    static Menu startButton;
    //static Timer timer;

    [SerializeField] GameObject playerController;

    //public MenuActions menuControls;
    private InputAction pause;
    private InputAction click;
    private Vector2 hotSpot = new Vector2(0, 0);

    
    private void Awake()
    {
        Instance = this;

        escMenu = transform.GetChild(0).GetComponent<Menu>();
        gameOverMenu = transform.GetChild(1).GetComponent<Menu>();
        goalMenu = transform.GetChild(2).GetComponent<Menu>();
        startButton = transform.GetChild(3).GetComponent<Menu>();
        //timer = transform.GetChild(4).GetComponent<Timer>();

        //menuControls = new MenuActions();
        PauseGame();
        StartGame();
    }

    private void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }
    void FixedUpdate()
    {
    }

    private void OnEnable()
    {
        //pause = menuControls.Buttons.Pause;
        pause.Enable();
        pause.performed += Pause;
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if (!escMenu.open && !gameOverMenu.open && !goalMenu.open && !startButton.open) OpenEscMenu(); 
        else if (escMenu.open) CloseEscMenu();
    }

    public void OpenEscMenu() 
    {
        PauseGame();
        escMenu.Open();
    }

    public void CloseEscMenu() 
    { 
        escMenu.Close();
        ResumeGame();       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    public void LeaveGame()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelNumber+1);
    }

    public void LevelComplete()
    {
        PauseGame();
        goalMenu.Open();
    }

    public void OpenGameOverMenu()
    {
        Cursor.visible = true;
        gameOverMenu.Open();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

}
