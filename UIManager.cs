using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Bars")]
    public Image hungerBar;
    public Image thirstBar;
    public Image madnessBar;
    public Image radiationBar;

    [Header("Text")]
    public Text dayIndicatorText;
    public Text remainingActionsText;
    public Text resources;

    [Header("Fade")]
    public Animator anim;
    public bool transition = false;
    public GameObject fadeImage;

    public GameObject canvas;
    public GameObject panelResources;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        BarUpdates();
        TextUpdates();
    }

    void BarUpdates()
    {
        hungerBar.fillAmount = (float)GameManager.Instance.hunger / (float)GameManager.Instance.maxHunger;
        thirstBar.fillAmount = (float)GameManager.Instance.thirst / (float)GameManager.Instance.maxThirst;
        madnessBar.fillAmount = (float)GameManager.Instance.madness / (float)GameManager.Instance.maxMadness;
        radiationBar.fillAmount = (float)GameManager.Instance.radiation / (float)GameManager.Instance.maxRadiation;
    }

    void TextUpdates()
    {
        dayIndicatorText.text = GameManager.Instance.day.ToString();
        remainingActionsText.text = GameManager.Instance.numberActions.ToString();
        resources.text = "Resources: " + InventaryManager.Instance.resources;
    }

    public void Transition()
    {
        transition = true;
        anim.Play("Fade_In_Fade_Out");
    }

    public void AllowContinue()
    {
        transition = false;
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Escena 1");
    }

}
