using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroduceScenario : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Dictionary<string, string> introduceStateDictionary;
    string agentName;

    public void InitIntroduceDictionary()
    {
        introduceStateDictionary = new Dictionary<string, string>
        {
            // state 1 - greet
            { GetDictionaryString(TextOCEAN.O_pos, 1), "Greetings."},
            { GetDictionaryString(TextOCEAN.O_neg, 1), "Hey."},
            { GetDictionaryString(TextOCEAN.C_pos, 1), "Hello."},
            { GetDictionaryString(TextOCEAN.C_neg, 1), "Oh, hi."},
            { GetDictionaryString(TextOCEAN.E_pos, 1), "Hi."},
            { GetDictionaryString(TextOCEAN.E_neg, 1), "Hello."},
            { GetDictionaryString(TextOCEAN.A_pos, 1), "Hi my friend."},
            { GetDictionaryString(TextOCEAN.A_neg, 1), "Well..."},
            { GetDictionaryString(TextOCEAN.N_pos, 1), "Um... Hey..."},
            { GetDictionaryString(TextOCEAN.N_neg, 1), "Hello."},

            // state 2 - say name
            { GetDictionaryString(TextOCEAN.O_pos, 2), "I am known as " + agentName + "."},
            { GetDictionaryString(TextOCEAN.O_neg, 2), "I'm " + agentName + "."},
            { GetDictionaryString(TextOCEAN.C_pos, 2), "My name is " + agentName + "."},
            { GetDictionaryString(TextOCEAN.C_neg, 2), "Umm, I'm " + agentName + "."},
            { GetDictionaryString(TextOCEAN.E_pos, 2), "I'm " + agentName + "."},
            { GetDictionaryString(TextOCEAN.E_neg, 2), "My name is " + agentName + "."},
            { GetDictionaryString(TextOCEAN.A_pos, 2), "I am " + agentName + "."},
            { GetDictionaryString(TextOCEAN.A_neg, 2), "Name is " + agentName + "."},
            { GetDictionaryString(TextOCEAN.N_pos, 2), "Ah, my name is... " + agentName + "."},
            { GetDictionaryString(TextOCEAN.N_neg, 2), "My name is " + agentName + "."},

            // state 1
            { GetDictionaryString(TextOCEAN.O_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.O_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.C_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.C_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.E_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.E_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.A_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.A_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.N_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.N_neg, 1), ""},
        };
    }

    private static string GetDictionaryString(TextOCEAN ocean, int state)
    {
        return ocean.ToString() + state;
    }
}
