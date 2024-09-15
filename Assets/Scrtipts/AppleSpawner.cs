using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public BoxCollider2D field;

    private Dictionary<string, string> translations = new Dictionary<string, string>()
    {
        {"улмо", "яблоко"},
        {"писпу", "дерево"},
        {"тусьты-пуньы", "посуда"},
        {"гондыр", "медведь"},
        {"пельнянь", "пельмень"},
        {"шундыкар", "солнечный город"},
        {"кубиста","капуста"},
        {"палэсмурт", "половинчатый человек"},
        {"позыръяськон", "дискотека"},
        {"нумыр", "червь"},

    };

    private List<string> words = new List<string>() {
        "улмо",
        "нумыр",
        "писпу",
        "гондыр",
        "кубиста",
        "пельнянь",
        "шундыкар",
        "палэсмурт",
        "позыръяськон",
        "тусьты-пуньы"
    };
    private int currentWordIndex = 0;
    private int currentLetterIndex = 0;

    private List<Vector2> busyCoordianates = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnWord();
    }

    public void Spawn(char letter)
    {
        Vector2 newCoordinates;
        int x;
        int y;
        do {
            x = Random.Range((int)field.bounds.min.x, (int)field.bounds.max.x);
            y = Random.Range((int)field.bounds.min.y, (int)field.bounds.max.y);
            newCoordinates = new Vector2(x, y);
        } while (busyCoordianates.Contains(newCoordinates) || (x==0 && y==0));

        GameObject gameObject = Instantiate(applePrefab, newCoordinates, Quaternion.identity);
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = letter.ToString();
        busyCoordianates.Add(newCoordinates);
    }

    public void SpawnNextWord()
    {
        currentWordIndex++;
        currentLetterIndex = 0;
        SpawnWord();
    }

    private void SpawnWord()
    {
        busyCoordianates.Clear();
        string word = words[currentWordIndex];
        foreach (var letter in word)
        {
            Spawn(letter);
        }
        FindObjectOfType<UIManager>().ShowTranslation(translations[word]);

    }

    public void CheckLetter(char letter)
    {
        string currentWord = words[currentWordIndex];
        if (currentWord[currentLetterIndex] == letter)
        {
            currentLetterIndex++;
        }
        else
        {
            FindObjectOfType<Snake>().Reset();
            ResetFood();
        }

    }

    public void ResetFood()
    {
        currentWordIndex = 0;
        currentLetterIndex = 0;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Food"))
        {
            Destroy(gameObject);
        }
        SpawnWord();
    }

}
