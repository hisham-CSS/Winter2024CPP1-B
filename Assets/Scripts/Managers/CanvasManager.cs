using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button resumeButton;
    public Button returnToMenuButton;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public TMP_Text volSliderText;
    public TMP_Text livesText;

    [Header("Slider")]
    public Slider volSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (resumeButton)
            resumeButton.onClick.AddListener(() => SetMenus(null, pauseMenu));

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));

        if (playButton)
            playButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(1));

        if (settingsButton)
            settingsButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));

        if (backButton)
            backButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            if (volSliderText)
                volSliderText.text = volSlider.value.ToString();
        }

        if (livesText)
        {
            GameManager.Instance.OnLifeValueChanged.AddListener(UpdateLifeText);
            livesText.text = "Lives: " + GameManager.Instance.lives.ToString();
        }

    }

    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate)
            menuToActivate.SetActive(true);

        if (menuToDeactivate)
            menuToDeactivate.SetActive(false);
    }

    void OnSliderValueChanged(float value)
    {
        volSliderText.text = value.ToString();
    }

    void UpdateLifeText(int value)
    {
        livesText.text = "Lives: " + value.ToString();
    }

    private void Quit()
    {
    #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            //hints for the lab

            if (pauseMenu.activeSelf)
            {
                //do something to pause
            }
            else
            {
                //do something to unpause
            }
        }

    }
}
