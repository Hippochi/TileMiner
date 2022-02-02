using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gridManager;
    [SerializeField] private GameObject introTxt;
    [SerializeField] private GameObject modeTxt;
    [SerializeField] private GameObject scoreTxt;
    private bool hasOpened = false;
    public bool scanMode = true;
    public int extracts, scans, total;
    void Update()
    {
        if (Input.GetKeyDown("f") && hasOpened == false)
        {
            gridManager.SetActive(true);
            introTxt.SetActive(false);
            modeTxt.SetActive(true);
            scoreTxt.SetActive(true);
            hasOpened = true;
        }
        scoreTxt.GetComponent<TextMeshProUGUI>().text = ""+total;

        if (Input.GetKeyDown("x") && hasOpened == true)
        {
            if (scanMode == true)
            {
                modeTxt.GetComponent<TextMeshProUGUI>().text = "Extraction Mode";
                scanMode = !scanMode;
            }
            else if (scanMode == false)
            {
                modeTxt.GetComponent<TextMeshProUGUI>().text = "Scanning Mode";
                scanMode = !scanMode;
            }
        }

        if (extracts == 0)
        {
            modeTxt.GetComponent<TextMeshProUGUI>().text = "Game Over! Your Score is: ";
        }
        
    }
}
