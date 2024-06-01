using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotScript : MonoBehaviour
{
    [SerializeField] GameObject NormalPanel;
    [SerializeField] GameObject SelectedPanel;
    [SerializeField] GameObject SelectedText;
    [SerializeField] GameObject NormalText;

    bool isSelected;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCursorEnter()
    {
        NormalPanel.SetActive(false);
        SelectedPanel.SetActive(true);
    }

    public void OnCursorExit()
    {
        if (isSelected)
            return;

        NormalPanel.SetActive(true);
        SelectedPanel.SetActive(false);
    }

    public void OnCursorUp()
    {
        isSelected = true;
        NormalText.SetActive(false);
        SelectedText.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
