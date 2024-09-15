using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public VisualTreeAsset startScreenAsset;
    public VisualTreeAsset endScreenAsset;
    private VisualElement startScreen;
    private VisualElement header;
    Label ScoreText;
    Label RecordText;
    Label TranslationText;
    Button pauseButton;
    private int score = 0;
    private int maxScore = 0;


    void OnEnable()
    {
        VisualElement root = FindObjectOfType<UIDocument>().rootVisualElement;
        header = root.Q("header");
        header.style.display = DisplayStyle.None;
        ScoreText = root.Q<Label>("ScoreText");
        RecordText = root.Q<Label>("RecordText");
        TranslationText = root.Q<Label>("TranslationText");

        pauseButton = root.Q<Button>("pauseButton");
        pauseButton.style.display = DisplayStyle.None;
        pauseButton.clicked += () => {
            Pause();
            FindObjectOfType<SoundManager>().PlaySound(SoundManager.Sound.ButtonClick);
        };

        startScreen = startScreenAsset.CloneTree();
        startScreen.style.flexGrow = 1;
        root.Add(startScreen);

        startScreen.Q<Button>("StartButton").clicked += () => {
            StartGame();
            FindObjectOfType<SoundManager>().PlaySound(SoundManager.Sound.ButtonClick);
        };

        Time.timeScale = 0;
    }

    private void StartGame() {
        header.style.display = DisplayStyle.Flex;
        startScreen.style.display = DisplayStyle.None;
        pauseButton.style.display = DisplayStyle.Flex;
        Time.timeScale = 1;
    }

    private void Pause() {
        header.style.display = DisplayStyle.None;
        startScreen.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        pauseButton.style.display = DisplayStyle.None;
        startScreen.Q<Button>("StartButton").text = "Азьланьтоно";
    }
    
    
    public void Increment() {
        score = score + 1;
        ScoreText.text = "Баллъёсы: " + score;
        if (score > maxScore) {
            maxScore = score;
        }
        RecordText. text = "Рекорд: " + maxScore;
    }

    public void ShowTranslation (string translation) {
         TranslationText.text = translation;
        
    }
    public void Reset() {
        score = 0;
        ScoreText.text = "Баллъёсы: " + score; 
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }
}
