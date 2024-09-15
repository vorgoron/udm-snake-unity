using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
    public GameObject segmentPrefab;
    private List<GameObject> segments;
    private KeyCode lastKeyCode;
    private float repeatRate = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<GameObject>();
        segments.Add(this.gameObject);
        InvokeRepeating(nameof(Move), repeatRate, repeatRate);   
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && lastKeyCode != KeyCode.A) {
            lastKeyCode = KeyCode.D;
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && lastKeyCode != KeyCode.S) {
            lastKeyCode = KeyCode.W;
        }
        if ((Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.LeftArrow)) && lastKeyCode != KeyCode.D){
            lastKeyCode = KeyCode.A;
        }
        if ((Input.GetKeyDown(KeyCode.S) 
            || Input.GetKeyDown(KeyCode.DownArrow)) && lastKeyCode != KeyCode.W) {
            lastKeyCode = KeyCode.S;
        }
    }

    void Move() {
        float x = segments[0].transform.position.x;
        float y = segments[0].transform.position.y;
        Vector2 newPosition = new Vector2();
        if (lastKeyCode == KeyCode.D) {
            newPosition = new Vector2 (x + 1, y);
        }
        if (lastKeyCode == KeyCode.W) {
            newPosition = new Vector2 (x, y + 1);
        }
        if (lastKeyCode == KeyCode.A) {
            newPosition = new Vector2(x - 1, y);
        }
        if (lastKeyCode == KeyCode.S) {
            newPosition = new Vector2(x, y - 1);
        }

        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].transform.position = segments[i-1].transform.position;
        }

        if (!segments[0].transform.position.Equals(newPosition)) {
            FindObjectOfType<SoundManager>().PlaySound(SoundManager.Sound.SnakeMove);
        }
        segments[0].transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Wall") {
            Reset();
        }
        if (other.gameObject.tag == "Snake" && segments.Count > 2) {
            Reset();
        }
        else if (other.gameObject.tag == "Food") {
            string letter = other.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text;
            Destroy(other.gameObject);
            FindObjectOfType<UIManager>().Increment();
            FindObjectOfType<SoundManager>().PlaySound(SoundManager.Sound.SnakeEat);
            GameObject gameObject = Instantiate(segmentPrefab);
            gameObject.transform.position = segments[segments.Count - 1].transform.position;
            segments.Add(gameObject);

            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = letter;

            FindObjectOfType<AppleSpawner>().CheckLetter(letter[0]);

            if (GameObject.FindGameObjectsWithTag("Food").Length == 1) {
                ClearSegments();
                lastKeyCode = KeyCode.None;
                FindObjectOfType<AppleSpawner>().SpawnNextWord();
            }
        }
    }

    public void Reset() {
        this.transform.position = new Vector2(0f, 0f);
        ClearSegments();
        FindObjectOfType<UIManager>().Reset();
        FindObjectOfType<AppleSpawner>().ResetFood();
        lastKeyCode = KeyCode.None;
        FindObjectOfType<SoundManager>().PlaySound(SoundManager.Sound.SnakeDie);
        
    }

    private void ClearSegments() {
        for (int i = 1 ;i<=segments.Count - 1; i++) { 
            Destroy (segments[i]);
        }
        segments.Clear();
        segments.Add(this.gameObject);
    
    }
}
