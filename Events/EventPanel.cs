using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    public static EventPanel Instance { get; private set; }
    public Text eventText, eventTitle;
    public GameObject textPanel;
    public GameObject continueButton;
    public GameObject canvas;
    public bool continueEvent = false;


    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
        canvas.SetActive(false);
    }

    public void ShowText(string _text, string _title)
    {
        eventText.text = _text;
        eventTitle.text = _title;
        this.gameObject.SetActive(true);
        canvas.SetActive(true);
        textPanel.SetActive(true);
    }

    public bool IsShowing()
    {
        return textPanel.activeSelf;
    }

    public void CloseText()
    {
        textPanel.SetActive(false);
    }

    /*
    public void ClosePanel()
    {
        UIManager.Instance.fadeImage.SetActive(true);
        UIManager.Instance.Transition();
        UIManager.Instance.canvas.SetActive(true);

        this.gameObject.SetActive(false);
        canvas.SetActive(false);
        continueEvent = false;
    }
    */

    public void Continue()
    {
        if (!EventManager.Instance.youAreDead)
        {
            if (continueEvent == true)
            {
                StartCoroutine(ClosePanel());
            }

            continueEvent = true;
        }
        else
        {
            Dead();
        }
    }

    public void Dead()
    {
        UIManager.Instance.fadeImage.SetActive(true);
        UIManager.Instance.Transition();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    IEnumerator ClosePanel()
    {
        UIManager.Instance.fadeImage.SetActive(true);
        UIManager.Instance.Transition();

        yield return new WaitForSeconds(1);

        UIManager.Instance.canvas.SetActive(true);
        this.gameObject.SetActive(false);
        canvas.SetActive(false);
        continueEvent = false;
    }

}
