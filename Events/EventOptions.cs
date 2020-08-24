using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventOptions : MonoBehaviour
{
    public static EventOptions Instance { get; private set; }
    public GameObject buttonPrefab;
    public Transform layout;
    public int optionChosen;

    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }

    public void ShowOptions(params string[] options)
    {
        while (layout.childCount > 0)
        {
            DestroyImmediate(layout.GetChild(0).gameObject);
        }

        for (int i = 0; i < options.Length; i++)
        {
            Button button = Instantiate(buttonPrefab, layout).GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = options[i];
            int number = i;
            button.onClick.AddListener(() =>
            {
                ChooseOption(number);
            });
        }

        this.gameObject.SetActive(true);
    }

    public bool IsShowing()
    {
        return this.gameObject.activeSelf;
    }

    public void ChooseOption(int id)
    {
        optionChosen = id;
        this.gameObject.SetActive(false);
        EventPanel.Instance.CloseText();

        while (layout.childCount > 0)
        {
            DestroyImmediate(layout.GetChild(0).gameObject);
        }
    }

}
