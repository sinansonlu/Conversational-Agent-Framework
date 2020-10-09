using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion { happiness, surprise, fear, anger, sadness, disgust };
public enum TextOCEAN { O_pos, O_neg, C_pos, C_neg, E_pos, E_neg, A_pos, A_neg, N_pos, N_neg };
public enum SpeechAnimation { Speak_Normal };

public enum DetermineType { OneAnswer, DetermineBurger, Random };

public class AgentResponse {

    public string responseText;
    public string[] responseTextAlt;

    public TextOCEAN responseTextOCEAN;
    public SpeechAnimation speechAnimation;

    public AudioClip clip;

    public float happiness;
    public float surprise;
    public float fear;
    public float anger;
    public float sadness;
    public float disgust;

    public List<AgentResponse> answers;
    
    private DetermineType determineType;

    public AgentResponse(string text)
    {
        responseTextAlt = new string[10];
        determineType = DetermineType.OneAnswer;
        SetResponseText(text);
    }

    // Set the response text as well as all the alternatives to given text
    public void SetResponseText(string text)
    {
        responseText = text;
        for(int i = 0; i < 10; i++)
        {
            responseTextAlt[i] = text;
        }
    }

    public void SetDetermineType(DetermineType dt)
    {
        determineType = dt;
    }

    public void AddOCEANalt(TextOCEAN tOCEAN, string altText)
    {
        responseTextAlt[TextOCEANtoInt(tOCEAN)] = altText;
    }

    public void AddAnswer(AgentResponse aResponse)
    {
        if(answers == null)
        {
            answers = new List<AgentResponse>();
        }

        answers.Add(aResponse);
    }

    public AgentResponse GetAnswer()
    {
        if (answers == null)
        {
            return null;
        }

        switch (determineType)
        {
            case DetermineType.OneAnswer:
                return answers[0];
            case DetermineType.DetermineBurger:
                return answers[0];
            case DetermineType.Random:
                return answers[Random.Range(0,answers.Count)];
            default:
                return null;
        }
    }

    private static int TextOCEANtoInt(TextOCEAN t)
    {
        switch (t)
        {
            case TextOCEAN.A_neg: return 0;
            case TextOCEAN.A_pos: return 1;
            case TextOCEAN.C_neg: return 2;
            case TextOCEAN.C_pos: return 3;
            case TextOCEAN.E_neg: return 4;
            case TextOCEAN.E_pos: return 5;
            case TextOCEAN.N_neg: return 6;
            case TextOCEAN.N_pos: return 7;
            case TextOCEAN.O_neg: return 8;
            case TextOCEAN.O_pos: return 9;
            default: return -1;
        }
    }
}
