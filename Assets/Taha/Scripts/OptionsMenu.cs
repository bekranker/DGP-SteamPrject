using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject AudioPanel;
    [SerializeField] GameObject ControllerPanel;

    private void OnDisable()
    {
        AudioPanel.SetActive(true);
        ControllerPanel.SetActive(false);
    }
}
