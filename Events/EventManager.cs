using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private int id;
    public bool youAreDead = false;

    [System.Serializable]
    public class ResourcesEventProperties
    {
        public string eventName;
        public int id;
        public string eventText;
        public string optionOne;
        public string optionTwo;
        public string resultOptionOne;
        public string resultOptionTwo;

        public int optionOneMadnessModifier;
        public int optionOneHungerModifier;
        public int optionOneThirstModifier;
        public int optionOneRadiationModifier;

        public int optionTwoMadnessModifier;
        public int optionTwoHungerModifier;
        public int optionTwoThirstModifier;
        public int optionTwoRadiationModifier;

        public int optionOneResourcesFound;
        public int optionTwoResourcesFound;
    }

    [System.Serializable]
    public class ItemEventProperties
    {
        public string eventName;
        public int id;
        public string eventText;
        public string optionOne;
        public string optionTwo;
        public string resultOptionOne;
        public string resultOptionTwo;

        public int optionOneMadnessModifier;
        public int optionOneHungerModifier;
        public int optionOneThirstModifier;
        public int optionOneRadiationModifier;

        public int optionTwoMadnessModifier;
        public int optionTwoHungerModifier;
        public int optionTwoThirstModifier;
        public int optionTwoRadiationModifier;

        public int optionOneItemFound;
        public int optionTwoItemFound;
    }

    [System.Serializable]
    public class DieEventProperties
    {
        public enum DieType { HUNGER, THIRST, MADNESS, RADIATION, NOT_HUNGER, NOT_THIRST, NOT_MADNESS, NOT_RADIATION }
        public DieType dieType;
        public string eventName;
        public int id;
        public string eventText;
    }



    List<ResourcesEventProperties> ResourcesEvents = new List<ResourcesEventProperties>()
    {
        new ResourcesEventProperties {eventName = "Footprints", id = 0, eventText = "You find a marks thas looks like a deer`s footprints.", optionOne = "You decide to follow the marks",
            optionTwo = "Go to hunt is a risk decision. You choose maintain in your way", resultOptionOne = "Finally you don`t find the deer and don`t get any resource",
            resultOptionTwo = "You encounter some resources in your path", optionOneHungerModifier = -10, optionOneThirstModifier = -10, optionTwoResourcesFound = 4, optionTwoHungerModifier = 10, optionTwoThirstModifier = 10},

        new ResourcesEventProperties {eventName = "To eat or not to eat", id = 1, eventText = "You go to the supermarket and find a woman´s recently dead body in the floor", optionOne = "I am so hungry. I must eat",
            optionTwo = "No. I will not eat any human. I prefer die.", resultOptionOne = "Mmmm slimy but tasty", resultOptionTwo = "I will return to bunker. It was a long day",
            optionOneHungerModifier = -25, optionOneMadnessModifier = 40, optionTwoHungerModifier = 10, optionTwoThirstModifier = 10, optionTwoMadnessModifier = 10},

        new ResourcesEventProperties {eventName = "Bunker", id = 2, eventText = "You decide to explore the hood. In one house you fond the entry to one bunker", optionOne = "Maybe i will not stay alone anymore",
            optionTwo = "Is dangerous explore this bunker. I will return", resultOptionOne = "The bunker is empty of people but full on resources.", resultOptionTwo = "Other shit day.",
            optionTwoResourcesFound = 4, optionTwoHungerModifier = 10, optionTwoMadnessModifier = 10, optionTwoThirstModifier = 10, optionTwoRadiationModifier = 10},

        new ResourcesEventProperties {eventName = "Bunker", id = 3, eventText = "You decide to explore the hood. In one house you fond the entry to one bunker", optionOne = "Maybe i will not stay alone anymore",
            optionTwo = "Is dangerous explore this bunker. I will return", resultOptionOne = "The bunker is empty of people but full on resources.", resultOptionTwo = "Other shit day.",
            optionTwoResourcesFound = 4, optionTwoHungerModifier = 10, optionTwoMadnessModifier = 10, optionTwoThirstModifier = 10, optionTwoRadiationModifier = 10},
    };

    List<ItemEventProperties> ItemEvents = new List<ItemEventProperties>()
    {
        new ItemEventProperties {eventName = "Library", id = 0, eventText = "You go to the local library. You encounter two books but only can carry one of them", optionOne = "You choose the blue book",
            optionTwo = "You choose the red book", resultOptionOne = "Is an a self help book",
            resultOptionTwo = "Is a diary from other survivors", optionOneItemFound = 0, optionTwoItemFound = 1},

        new ItemEventProperties {eventName = "Restaurant", id = 1, eventText = "You go to a near restaurant to find some food. You don`t find any food but you look two interesting objects.", optionOne = "Choose the book",
            optionTwo = "You choose the filter for water", resultOptionOne = "Is an a recipe book",
            resultOptionTwo = "Is an a water filter for your bunker", optionOneItemFound = 2, optionTwoItemFound = 3},
    };

    List<DieEventProperties> DieEvents = new List<DieEventProperties>()
    {
        new DieEventProperties {eventName = "You have so hungry", dieType = DieEventProperties.DieType.HUNGER, eventText = "You have passed the last weeks without any thing to eat. Your time in the earth is finishing.", id = 0},
        new DieEventProperties {eventName = "You have so thirst", dieType = DieEventProperties.DieType.THIRST, eventText = "You have passed the last weeks without any thing to drink. Your time in the earth is finishing.", id = 1},
        new DieEventProperties {eventName = "You are so crazy", dieType = DieEventProperties.DieType.MADNESS, eventText = "You have pass your last weeks completly alone.", id = 2},
        new DieEventProperties {eventName = "You have so radiation", dieType = DieEventProperties.DieType.RADIATION, eventText = "You have so radiation. You have been poisoned. Obviously, you are dead.", id = 3},
        new DieEventProperties {eventName = "You have a lot of food", dieType = DieEventProperties.DieType.NOT_HUNGER, eventText = "You have a lot of food in your bunker. This food atract a lot of savage animals.", id = 4},
        new DieEventProperties {eventName = "You have so much water reserves", dieType = DieEventProperties.DieType.NOT_THIRST, eventText = "You have a lot of water reserves. This reserves atract a lot of savage animals.", id = 5},
        new DieEventProperties {eventName = "You are sanity", dieType = DieEventProperties.DieType.NOT_MADNESS, eventText = "You awake completly sanity for first time in weeks. You take adventage of this momentary clarity and kill yourself in order to end whit this nigthmare.  ", id = 6},
        new DieEventProperties {eventName = "You dont have any radiation", dieType = DieEventProperties.DieType.NOT_RADIATION, eventText = "You are empty of radiation but you have take a new disease: Coronavirus.", id = 7},
    };

    ResourcesEventProperties currentResourcesEvent;
    ItemEventProperties currentItemEvent;
    DieEventProperties currentDieEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectEvent(bool deathEvent = false)
    {
        UIManager.Instance.canvas.SetActive(false);
        UIManager.Instance.panelResources.SetActive(false);
        StopAllCoroutines();
        int typeEvent;
        if (!deathEvent)
        {
            typeEvent = Random.Range(0, 2);
            if (typeEvent == 0)
            {
                id = ResourcesEvents[Random.Range(0, ResourcesEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent));
            }
            else
            {
                id = ItemEvents[Random.Range(0, ItemEvents.Count)].id;
                Debug.Log(id);
                StartCoroutine(EventCorroutine(id, typeEvent));
            }
        }
        else
        {
            typeEvent = 2;
            if (GameManager.Instance.hunger <= 0)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.NOT_HUNGER));
                GameManager.Instance.hunger = 1;
            }
            else if (GameManager.Instance.hunger >= 100)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.HUNGER));
                GameManager.Instance.hunger = 99;
            }
            else if (GameManager.Instance.thirst <= 0)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.NOT_THIRST));
                GameManager.Instance.thirst = 1;
            }
            else if (GameManager.Instance.thirst >= 100)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.THIRST));
                GameManager.Instance.thirst = 99;
            }
            else if (GameManager.Instance.madness <= 0)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.NOT_MADNESS));
                GameManager.Instance.madness = 1;
            }
            else if (GameManager.Instance.madness >= 100)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.MADNESS));
                GameManager.Instance.madness = 99;
            }
            else if (GameManager.Instance.radiation <= 0)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.NOT_RADIATION));
                GameManager.Instance.radiation = 1;

            }
            else if (GameManager.Instance.radiation >= 100)
            {
                id = DieEvents[Random.Range(0, ItemEvents.Count)].id;
                StartCoroutine(EventCorroutine(id, typeEvent, DieEventProperties.DieType.RADIATION));
                GameManager.Instance.radiation = 99;
            }

        }

        GameManager.Instance.numberActions--;
    }

    IEnumerator EventCorroutine(int _id, int _typeEvent, DieEventProperties.DieType _dieType = DieEventProperties.DieType.HUNGER)
    {
        Debug.Log("LO HA HECHO");

        EventPanel.Instance.continueEvent = false;

        yield return new WaitForSeconds(1);

        EventPanel.Instance.canvas.SetActive(true);
        UIManager.Instance.fadeImage.SetActive(false);

        if (_typeEvent == 0)
        {
            for (int i = 0; i < ResourcesEvents.Count; i++)
            {
                if (ResourcesEvents[i].id == _id)
                {
                    currentResourcesEvent = ResourcesEvents[i];
                    break;
                }
            }
            EventPanel.Instance.ShowText(currentResourcesEvent.eventText.ToString(), currentResourcesEvent.eventName.ToString());
            while (!EventPanel.Instance.continueEvent)
            {
                yield return null;
            }

            EventOptions.Instance.ShowOptions(currentResourcesEvent.optionOne.ToString(), currentResourcesEvent.optionTwo.ToString());
            EventPanel.Instance.continueButton.SetActive(false);
            while (EventOptions.Instance.IsShowing())
            {
                yield return null;
            }

            EventPanel.Instance.continueButton.SetActive(true);
            if (EventOptions.Instance.optionChosen == 0)
            {
                EventPanel.Instance.ShowText(currentResourcesEvent.resultOptionOne.ToString(), currentResourcesEvent.eventName.ToString());
                GameManager.Instance.hunger += currentResourcesEvent.optionOneHungerModifier;
                GameManager.Instance.thirst += currentResourcesEvent.optionOneThirstModifier;
                GameManager.Instance.madness += currentResourcesEvent.optionOneMadnessModifier;
                GameManager.Instance.radiation += currentResourcesEvent.optionOneRadiationModifier;
            }
            else if (EventOptions.Instance.optionChosen == 1)
            {
                EventPanel.Instance.ShowText(currentResourcesEvent.resultOptionTwo.ToString(), currentResourcesEvent.eventName.ToString());
                GameManager.Instance.hunger += currentResourcesEvent.optionTwoHungerModifier;
                GameManager.Instance.thirst += currentResourcesEvent.optionTwoThirstModifier;
                GameManager.Instance.madness += currentResourcesEvent.optionTwoMadnessModifier;
                GameManager.Instance.radiation += currentResourcesEvent.optionTwoRadiationModifier;
            }
        }
        else if (_typeEvent == 1)
        {
            for (int i = 0; i < ItemEvents.Count; i++)
            {
                if (ItemEvents[i].id == _id)
                {
                    currentItemEvent = ItemEvents[i];
                    break;
                }
            }

            EventPanel.Instance.ShowText(currentItemEvent.eventText.ToString(), currentItemEvent.eventName.ToString());
            while (!EventPanel.Instance.continueEvent)
            {
                yield return null;
            }

            EventOptions.Instance.ShowOptions(currentItemEvent.optionOne.ToString(), currentItemEvent.optionTwo.ToString());
            EventPanel.Instance.continueButton.SetActive(false);
            while (EventOptions.Instance.IsShowing())
            {
                yield return null;
            }

            EventPanel.Instance.continueButton.SetActive(true);
            if (EventOptions.Instance.optionChosen == 0)
            {
                EventPanel.Instance.ShowText(currentItemEvent.resultOptionOne.ToString(), currentItemEvent.eventName.ToString());
                id = ItemEvents[id].optionOneItemFound;
                InventaryManager.Instance.items[id].active = true;
                Debug.Log(InventaryManager.Instance.items[id].active);
            }
            else if (EventOptions.Instance.optionChosen == 1)
            {
                EventPanel.Instance.ShowText(currentItemEvent.resultOptionTwo.ToString(), currentItemEvent.eventName.ToString());
                id = ItemEvents[id].optionTwoItemFound;
                InventaryManager.Instance.items[id].active = true;
            }
        }
        else if (_typeEvent == 2)
        {
            switch (_dieType)
            {
                case DieEventProperties.DieType.HUNGER:
                    List<DieEventProperties> dieForHunger = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.HUNGER)
                        {
                            dieForHunger.Add(DieEvents[i]);
                            id = dieForHunger[Random.Range(0, dieForHunger.Count)].id;
                            if (id == DieEvents[i].id)
                            {
                                currentDieEvent = dieForHunger[Random.Range(0, dieForHunger.Count)];
                            }
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.NOT_HUNGER:
                    List<DieEventProperties> dieForNotHunger = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.NOT_HUNGER)
                        {
                            dieForNotHunger.Add(DieEvents[i]);
                            id = dieForNotHunger[Random.Range(0, dieForNotHunger.Count)].id;
                            if (id == DieEvents[i].id)
                            {
                                currentDieEvent = dieForNotHunger[Random.Range(0, dieForNotHunger.Count)];
                            }
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.THIRST:
                    List<DieEventProperties> dieForThirst = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.THIRST)
                        {
                            dieForThirst.Add(DieEvents[i]);
                            id = dieForThirst[Random.Range(0, dieForThirst.Count)].id;
                            currentDieEvent = dieForThirst[Random.Range(0, dieForThirst.Count)];
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.NOT_THIRST:
                    List<DieEventProperties> dieForNotThirst = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.NOT_THIRST)
                        {
                            dieForNotThirst.Add(DieEvents[i]);
                            id = dieForNotThirst[Random.Range(0, dieForNotThirst.Count)].id;
                            currentDieEvent = dieForNotThirst[Random.Range(0, dieForNotThirst.Count)];
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.MADNESS:
                    List<DieEventProperties> dieForMadness = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.MADNESS)
                        {
                            dieForMadness.Add(DieEvents[i]);
                            id = dieForMadness[Random.Range(0, dieForMadness.Count)].id;
                            currentDieEvent = dieForMadness[Random.Range(0, dieForMadness.Count)];
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.NOT_MADNESS:
                    List<DieEventProperties> dieForNotMadness = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.NOT_MADNESS)
                        {
                            dieForNotMadness.Add(DieEvents[i]);
                            id = dieForNotMadness[Random.Range(0, dieForNotMadness.Count)].id;
                            currentDieEvent = dieForNotMadness[Random.Range(0, dieForNotMadness.Count)];
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.RADIATION:
                    List<DieEventProperties> dieForRadiation = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.RADIATION)
                        {
                            dieForRadiation.Add(DieEvents[i]);
                            id = dieForRadiation[Random.Range(0, dieForRadiation.Count)].id;
                            currentDieEvent = dieForRadiation[Random.Range(0, dieForRadiation.Count)];
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;

                case DieEventProperties.DieType.NOT_RADIATION:
                    List<DieEventProperties> dieForNotRadiation = new List<DieEventProperties>();
                    for (int i = 0; i < DieEvents.Count; i++)
                    {
                        if (DieEvents[i].dieType == DieEventProperties.DieType.NOT_RADIATION)
                        {
                            dieForNotRadiation.Add(DieEvents[i]);
                            id = dieForNotRadiation[Random.Range(0, dieForNotRadiation.Count)].id;
                            currentDieEvent = dieForNotRadiation[Random.Range(0, dieForNotRadiation.Count)];
                        }
                    }
                    EventPanel.Instance.ShowText(currentDieEvent.eventText.ToString(), currentDieEvent.eventName.ToString());
                    while (!EventPanel.Instance.continueEvent)
                    {
                        yield return null;
                    }
                    break;
            }
            youAreDead = true;
            GameManager.Instance.InitializeModifiers();
        }
        UIManager.Instance.panelResources.SetActive(true);
    }
}
