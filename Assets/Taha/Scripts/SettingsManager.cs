using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    bool isPause;
    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject OptionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        SettingsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePressed();
        }

        if (StartPanel != null)
        {
            if (Input.anyKeyDown)
            {
                StartPanel.SetActive(false);
            }
        }
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        if (StartPanel != null)
        {
            PausePressed();
        }
        else
        {
            OptionsPanel.SetActive(false);
        }
    }

    public void PausePressed()
    {
        isPause = !isPause;
        Time.timeScale = !isPause ? 1.0f : 0;
        SettingsCanvas.SetActive(isPause);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
