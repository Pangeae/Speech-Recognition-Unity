﻿using UnityEngine;
using UnityEngine.Windows.Speech;

/*
 * This class is designed to be called by other classes
 * Attach this component to a game object
 * Then use onSpeechRecognized += to bind your method
 * Then call Listen() to start the engine.
 * Once a phrase is recognized, your method will be invoked and the engine will stop.
 * If you want a new phrase, you will need to call Listen() again.
 */
public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] keywords = {"up", "down", "left", "right"};
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public PhraseRecognizer.PhraseRecognizedDelegate onPhraseRecognized;
    private PhraseRecognizer recognizer;

    /*
     * Listen once for a phrase and then stop listening
     */
    public void Listen()
    {
        if (!recognizer.IsRunning)
            recognizer.Start();
    }

    private void Start()
    {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += OnPhraseRecognized;
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        onPhraseRecognized.Invoke(args);
        recognizer.Stop();
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null)
        {
            recognizer.OnPhraseRecognized -= OnPhraseRecognized;
            recognizer.Stop();
            recognizer.Dispose();
        }
    }
}