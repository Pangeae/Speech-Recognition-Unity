﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PlayerMenuVoiceSystem : MonoBehaviour
{
    public Text results;
    public Text status;
    private SpeechRecognitionEngine speechEngine;

    // Use this for initialization
    private void Start()
    {
        speechEngine = GetComponent<SpeechRecognitionEngine>();
        speechEngine.onPhraseRecognized += OnCommandReceived;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Submit"))
        {
            speechEngine.Listen();
            status.text = "Listening...";
        }
    }

    private void OnCommandReceived(PhraseRecognizedEventArgs args)
    {
        string word = args.text;
        results.text = string.Format("You said: <b>{0}</b>\nConfidence: {1}\nPhrase Duration: {2} ms",
            word,
            args.confidence,
            args.phraseDuration.TotalMilliseconds);
        print(results.text);
        if (args.semanticMeanings != null)
        {
            foreach (SemanticMeaning meaning in args.semanticMeanings)
            {
                print(meaning);
            }
        }

        status.text = "Press \"Submit\" button for voice command...";
    }
}