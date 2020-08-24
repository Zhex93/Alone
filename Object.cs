using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    public enum ObjType { SHOWER, KITCHEN, BOTTLES, RADIO, BED, STAIRS };
    public ObjType objType;

    [Header("Canvas")]
    public GameObject canvas;
    public Transform kitchenPanel;
    public Transform gameObjectsOrigin;
    public Vector3 offsetPos;

    [Header("Shower Minigame")]
    public GameObject bubble;
    public float maxShowerTime = 10.0f;
    private float currentTime = 0;
    private bool minigameFailed = false;

    [System.Serializable]
    public struct QTEInput
    {
        public KeyCode[] keys;
        public float startingTime;
        public float duration;
    }


    [Header("QTEVENT")]
    public List<QTEInput> inputs;
    public int maxFails;
    int oks;
    int fails;
    float sequenceTime;


    [Header("Water Minigame")]
    public GameObject[] buttonsSprites;
    public Vector3[] spriteSpawnPoint;

    [Header("Kitchen Minigame")]
    public Image ingImage;
    public GameObject[] gameObjectIngredients;
    public bool succesOne, succesTwo, succesThree;
    private float maxKitchenTime = 10.0f;

    [Header("Stairs")]
    public GameObject stairsCanvas;
    public Transform firstFloor, secondFloor;

    [Header("Audio")]
    public AudioSource audioSorce;

    [System.Serializable]
    public struct Ingredient
    {
        public string name;
        public Sprite ingredientsImages;
    }
    public Ingredient[] ingredients;

    private void Start()
    {
        if (objType == ObjType.STAIRS)
        {
            stairsCanvas.SetActive(false);
        }

        audioSorce.GetComponent<AudioSource>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxShowerTime)
        {
            minigameFailed = true;
        }
        if (currentTime >= maxKitchenTime)
        {
            minigameFailed = true;
        }
    }

    void Stairs()
    {
        stairsCanvas.SetActive(true);
    }

    public void GoFirstFloor()
    {
        PlayerController.Instance.gameObject.transform.position = firstFloor.position;
        UIManager.Instance.Transition();
        PlayerController.Instance.interactCollider.enabled = true;
        stairsCanvas.SetActive(false);
        PlayerController.Instance.isDoingAMinigame = false;
    }

    public void GoSecondFloor()
    {
        PlayerController.Instance.gameObject.transform.position = secondFloor.position;
        UIManager.Instance.Transition();
        PlayerController.Instance.interactCollider.enabled = true;
        stairsCanvas.SetActive(false);
        PlayerController.Instance.isDoingAMinigame = false;
    }

    public void Exit()
    {
        if (GameManager.Instance.numberActions > 0)
        {
            UIManager.Instance.Transition();
            EventManager.Instance.SelectEvent();
            PlayerController.Instance.isDoingAMinigame = false;
            stairsCanvas.SetActive(false);
        }
    }

    void CustomOKAction()
    {
        Debug.Log("HAS GANADO");
    }

    void CustomFailAction()
    {
        Debug.Log("HAS PERDIDO");
    }

    public void DoAction()
    {
        switch (objType)
        {
            case ObjType.SHOWER:
                StartCoroutine(ShowerMiniGame());
                currentTime = 0;
                PlayerController.Instance.isDoingAMinigame = true;
                InventaryManager.Instance.resources--;
                break;

            case ObjType.BOTTLES:
                StartCoroutine(BottlesMiniGame());
                currentTime = 0;
                PlayerController.Instance.isDoingAMinigame = true;
                InventaryManager.Instance.resources--;
                break;

            case ObjType.KITCHEN:
                StartCoroutine(KitchenMiniGame());
                currentTime = 0;
                PlayerController.Instance.isDoingAMinigame = true;
                InventaryManager.Instance.resources--;
                break;

            case ObjType.RADIO:
                StartCoroutine(RadioMiniGame());
                currentTime = 0;
                PlayerController.Instance.isDoingAMinigame = true;
                InventaryManager.Instance.resources--;
                break;

            case ObjType.BED:
                StartCoroutine(Sleep());
                PlayerController.Instance.isDoingAMinigame = true;
                break;

            case ObjType.STAIRS:
                Stairs();
                PlayerController.Instance.isDoingAMinigame = true;
                break;
        }
    }

    IEnumerator ShowerMiniGame()
    {
        Debug.Log("Ducha");
        audioSorce.Play();
        GameManager.Instance.currentBubbles = 0;
        GameManager.Instance.currentShower = 0;
        minigameFailed = false;
        int showerComplete = 10, maxBubbles = 15;

        canvas.SetActive(true);

        while (GameManager.Instance.currentBubbles < maxBubbles && GameManager.Instance.currentShower <= showerComplete && !minigameFailed)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1));
            offsetPos = new Vector3(Random.Range(-3, 3), 0, 0);
            GameObject newBubble = Instantiate(bubble, gameObjectsOrigin.position + offsetPos, Quaternion.identity);
            GameManager.Instance.currentBubbles++;
        }

        if (!minigameFailed)
        {
            InventaryManager.Instance.resources--;
            PlayerController.Instance.isDoingAMinigame = false;
            PlayerController.Instance.interactCollider.enabled = true;
        }
        else if (GameManager.Instance.currentShower >= showerComplete)
        {
            InventaryManager.Instance.resources--;
            GameManager.Instance.radiation -= 25;
            PlayerController.Instance.isDoingAMinigame = false;
            PlayerController.Instance.interactCollider.enabled = true;
        }
        minigameFailed = false;
        PlayerController.Instance.isDoingAMinigame = false;
        PlayerController.Instance.interactCollider.enabled = true;

        audioSorce.Stop();
    }

    IEnumerator BottlesMiniGame()
    {
        List<GameObject> spritesSpawned = new List<GameObject>();

        audioSorce.Play();

        oks = 0;
        fails = 0;
        sequenceTime = 0.0f;

        for (int i = 0; i < inputs.Count; i++)
        {
            bool completed = false;
            bool spawnCopleted = false;
            KeyCode keyToPress = inputs[i].keys[Random.Range(0, inputs[i].keys.Length)];

            List<GameObject> buttonsSpawned = new List<GameObject>();

            GameObject buttonSpawned;

            while (!completed)
            {
                sequenceTime += Time.deltaTime;
                Debug.Log(keyToPress);
                yield return null;

                if (sequenceTime < inputs[i].startingTime)
                {

                }
                else if (sequenceTime < inputs[i].startingTime + inputs[i].duration)
                {


                    if (keyToPress == inputs[i].keys[0] && spawnCopleted == false)
                    {
                        buttonSpawned = GameObject.Instantiate(buttonsSprites[0], spriteSpawnPoint[0], Quaternion.identity);

                        buttonsSpawned.Add(buttonSpawned);


                        spawnCopleted = true;

                        if (completed == true)
                        {
                            buttonSpawned.SetActive(false);
                        }
                    }
                    else if (keyToPress == inputs[i].keys[1] && spawnCopleted == false)
                    {
                        buttonSpawned = GameObject.Instantiate(buttonsSprites[1], spriteSpawnPoint[1], Quaternion.identity);

                        buttonsSpawned.Add(buttonSpawned);

                        spawnCopleted = true;

                        if (completed == true)
                        {
                            buttonSpawned.SetActive(false);
                        }
                    }
                    else if (keyToPress == inputs[i].keys[2] && spawnCopleted == false)
                    {
                        buttonSpawned = GameObject.Instantiate(buttonsSprites[2], spriteSpawnPoint[2], Quaternion.identity);

                        buttonsSpawned.Add(buttonSpawned);

                        spawnCopleted = true;

                        if (completed == true)
                        {
                            buttonSpawned.SetActive(false);
                        }

                    }
                    else if (keyToPress == inputs[i].keys[3] && spawnCopleted == false)
                    {
                        buttonSpawned = GameObject.Instantiate(buttonsSprites[3], spriteSpawnPoint[3], Quaternion.identity);

                        buttonsSpawned.Add(buttonSpawned);

                        spawnCopleted = true;

                        if (completed == true)
                        {
                            buttonSpawned.SetActive(false);
                        }

                    }

                    if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown(keyToPress))
                        {
                            oks++;
                            spawnCopleted = false;

                            Destroy(buttonsSpawned[0]);

                            completed = true;
                        }
                        else
                        {
                            fails++;
                            spritesSpawned.Clear();

                            if (fails > maxFails)
                            {
                                spritesSpawned.Clear();
                                CustomFailAction();
                                yield break;
                            }
                            completed = true;
                        }
                    }
                }
                else
                {

                    fails++;
                    if (fails > maxFails)
                    {

                        CustomFailAction();
                        yield break;
                    }
                    completed = true;
                }
            }
        }



        if (!minigameFailed)
        {
            InventaryManager.Instance.resources--;
            PlayerController.Instance.isDoingAMinigame = false;
            PlayerController.Instance.interactCollider.enabled = true;

        }
        else if (fails <= maxFails)
        {

            InventaryManager.Instance.resources--;
            PlayerController.Instance.isDoingAMinigame = false;
            PlayerController.Instance.interactCollider.enabled = true;

            GameManager.Instance.thirst -= 20;
        }

        minigameFailed = false;

        audioSorce.Stop();

        yield return null;

    }

    IEnumerator KitchenMiniGame()
    {

        audioSorce.Play();


        int totalIngredients = 3, maxIngredients = 10, currentIngredients = 0;
        int currentFails = 0, maxFails = 3;
        bool minigameSucces = false;
        minigameFailed = false;



        List<Ingredient> recipes = new List<Ingredient>();

        for (int i = 0; i < totalIngredients; i++)
        {
            recipes.Add(ingredients[Random.Range(0, ingredients.Length)]);
            Image newImage = Instantiate(ingImage, kitchenPanel);
            newImage.GetComponent<Image>().sprite = recipes[i].ingredientsImages;
        }

        yield return null;

        List<GameObject> newIngredients = new List<GameObject>();

        while (currentIngredients < maxIngredients && !minigameFailed)
        {
            offsetPos = new Vector3(Random.Range(-3, 3), Random.Range(0, 5), 0);
            GameObject newIngredient = Instantiate(gameObjectIngredients[Random.Range(0, gameObjectIngredients.Length)], gameObjectsOrigin.position + offsetPos, Quaternion.identity);
            currentIngredients++;
            newIngredients.Add(newIngredient);
        }

        RaycastHit outHit;

        while (!minigameFailed)
        {
            if (currentFails >= maxFails)
            {
                minigameFailed = true;
            }

            if (currentIngredients < maxIngredients)
            {
                offsetPos = new Vector3(Random.Range(-3, 3), Random.Range(3, 5), 0);
                GameObject newIngredient = Instantiate(gameObjectIngredients[Random.Range(0, gameObjectIngredients.Length)], gameObjectsOrigin.position + offsetPos, Quaternion.identity);
                currentIngredients++;
                newIngredients.Add(newIngredient);
            }

            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out outHit))
            {
                if (!succesOne)
                {
                    if (outHit.collider.CompareTag(recipes[0].name) && Input.GetMouseButton(0))
                    {
                        succesOne = true;
                        Destroy(outHit.collider.gameObject);
                        currentIngredients--;
                    }
                    else if (!outHit.collider.CompareTag(recipes[0].name) && Input.GetMouseButton(0))
                    {
                        if (outHit.collider.CompareTag("Untagged") || outHit.collider.CompareTag("Object"))
                        {

                        }
                        else
                        {
                            Destroy(outHit.collider.gameObject);
                            currentIngredients--;
                            currentFails++;
                        }
                    }
                }
                else if (!succesTwo)
                {
                    if (outHit.collider.CompareTag(recipes[1].name) && Input.GetMouseButton(0))
                    {
                        succesTwo = true;
                        Destroy(outHit.collider.gameObject);
                        currentIngredients--;
                    }
                    else if (!outHit.collider.CompareTag(recipes[1].name) && Input.GetMouseButton(0))
                    {
                        if (outHit.collider.CompareTag("Untagged") || outHit.collider.CompareTag("Object"))
                        {

                        }
                        else
                        {
                            Destroy(outHit.collider.gameObject);
                            currentIngredients--;
                            currentFails++;
                        }
                    }
                }
                else if (!succesThree)
                {
                    if (outHit.collider.CompareTag(recipes[2].name) && Input.GetMouseButton(0))
                    {
                        succesThree = true;
                        minigameSucces = true;
                        Destroy(outHit.collider.gameObject);
                        currentIngredients--;
                    }
                    else if (!outHit.collider.CompareTag(recipes[2].name) && Input.GetMouseButton(0))
                    {
                        if (outHit.collider.CompareTag("Untagged") || outHit.collider.CompareTag("Object"))
                        {

                        }
                        else
                        {
                            Destroy(outHit.collider.gameObject);
                            currentIngredients--;
                            currentFails++;
                        }
                    }
                }
            }
            yield return null;
        }

        if (!minigameSucces)
        {
            InventaryManager.Instance.resources--;
            PlayerController.Instance.isDoingAMinigame = false;
            PlayerController.Instance.interactCollider.enabled = true;
        }
        else
        {
            InventaryManager.Instance.resources--;
            GameManager.Instance.hunger -= 20;
            PlayerController.Instance.isDoingAMinigame = false;
            PlayerController.Instance.interactCollider.enabled = true;
        }

        recipes.Clear();
        for (int i = 0; i < kitchenPanel.childCount; i++)
        {
            Destroy(kitchenPanel.GetChild(i).gameObject);
        }

        for (int i = 0; i < newIngredients.Count; i++)
        {
            Destroy(newIngredients[i]);
        }
        minigameFailed = false;

        audioSorce.Stop();
    }

    IEnumerator RadioMiniGame()
    {
        audioSorce.Play();

        PlayerController.Instance.isDoingAMinigame = true;
        yield return null;
        GameManager.Instance.madness -= 10;
        PlayerController.Instance.interactCollider.enabled = true;
        PlayerController.Instance.isDoingAMinigame = false;

        audioSorce.Stop();
    }

    IEnumerator Sleep()
    {
        audioSorce.Play();

        GameManager.Instance.ChangeDay();
        yield return new WaitForSeconds(2);
        PlayerController.Instance.interactCollider.enabled = true;
        PlayerController.Instance.isDoingAMinigame = false;

        audioSorce.Stop();
    }

}

