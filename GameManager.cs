using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void PreloadManagers()
    {
        SceneManager.LoadScene("Managers", LoadSceneMode.Additive);
    }

    public static GameManager Instance { get; private set; }
    public int numberActions = 2;
    public int day = 1;

    public int madness, maxMadness = 100;
    public int hunger, maxHunger = 100;
    public int thirst, maxThirst = 100;
    public int radiation, maxRadiation = 100;

    [Header("MiniGames")]
    public int currentShower = 0;
    public int currentBubbles = 0;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        InitializeModifiers();
    }

    public void InitializeModifiers()
    {
        madness = 50;
        hunger = 50;
        thirst = 50;
        radiation = 50;
    }

    private void Update()
    {
        if (numberActions <= 0)
        {
            numberActions = 0;
        }

        Die();
        Win();
    }

    public void ChangeDay()
    {
        day++;

        int madnessModifier = 0;
        int hungerModifier = 0;
        int thirstModifier = 0;
        int radiationModifier = 0;

        for (int i = 0; i < InventaryManager.Instance.items.Count; i++)
        {
            if (InventaryManager.Instance.items[i].type == InventaryManager.ItemType.MADNESS && InventaryManager.Instance.items[i].active)
            {
                madnessModifier += InventaryManager.Instance.items[i].modifier;
            }
            else if (InventaryManager.Instance.items[i].type == InventaryManager.ItemType.HUNGER && InventaryManager.Instance.items[i].active)
            {
                hungerModifier += InventaryManager.Instance.items[i].modifier;
            }
            else if (InventaryManager.Instance.items[i].type == InventaryManager.ItemType.THIRST && InventaryManager.Instance.items[i].active)
            {
                thirstModifier += InventaryManager.Instance.items[i].modifier;
            }
            else if (InventaryManager.Instance.items[i].type == InventaryManager.ItemType.RADIATION && InventaryManager.Instance.items[i].active)
            {
                radiationModifier += InventaryManager.Instance.items[i].modifier;
            }
        }

        madness += 10 + madnessModifier;
        hunger += 10 + hungerModifier;
        thirst += 10 + thirstModifier;
        radiation -= 10 + radiationModifier;

        numberActions = 2;
    }

    public void Die()
    {
        
        if (madness <= 0 || hunger <= 0 || thirst <= 0 || radiation <= 0)
        {
            EventManager.Instance.SelectEvent(true);
            day = 1;
            numberActions = 2;
        }
        else if (madness >= 100 || hunger >= 100 || thirst >= 100 || radiation >= 100)
        {
            EventManager.Instance.SelectEvent(true);
            day = 1;
            numberActions = 2;
        }
    }

    void Win()
    {
        if (day >= 10)
        {
            Debug.Log("WIN!!");
            InitializeModifiers();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
