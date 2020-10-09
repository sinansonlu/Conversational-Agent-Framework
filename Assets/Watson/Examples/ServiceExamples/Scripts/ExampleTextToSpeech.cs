/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;

public class ExampleTextToSpeech : MonoBehaviour
{
    public MainLogic mainLogic;

    public Text onScreenText;

    private string currentPlainTalkText;
    private TextOCEAN currentSpeechOCEAN;

    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://stream.watsonplatform.net/text-to-speech/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Header("CF Authentication")]
    [Tooltip("The authentication username.")]
    [SerializeField]
    private string _username;
    [Tooltip("The authentication password.")]
    [SerializeField]
    private string _password;
    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string _iamApikey;
    [Tooltip("The IAM url used to authenticate the apikey (optional). This defaults to \"https://iam.bluemix.net/identity/token\".")]
    [SerializeField]
    private string _iamUrl;
    #endregion

    TextToSpeech _service;
    string _testString = "<speak version=\"1.0\"><express-as type=\"GoodNews\">This is great news!</express-as></speak>";

    string netalk = "<speak version=\"1.0\"><express-as type=\"GoodNews\">Wow, great news!</express-as><express-as type=\"Uncertainty\">second thoughts</express-as><express-as type=\"Apology\">it is terrible</express-as></speak>"; //"<speak>this is normal.<express-as type=\"Apology\">but i'm sorry</express-as><express-as type=\"Uncertainty\">anything can happen</express-as><express-as type=\"GoodNews\">just kidding</express-as></speak>";

    string _createdCustomizationId;
    CustomVoiceUpdate _customVoiceUpdate;
    string _customizationName = "unity-example-customization";
    string _customizationLanguage = "en-US";
    string _customizationDescription = "A text to speech voice customization created within Unity.";
    string _testWord = "Watson";

    private bool _synthesizeTested = false;
    private bool _getVoicesTested = false;
    private bool _getVoiceTested = false;
    private bool _getPronuciationTested = false;
    private bool _getCustomizationsTested = false;
    private bool _createCustomizationTested = false;
    private bool _deleteCustomizationTested = false;
    private bool _getCustomizationTested = false;
    private bool _updateCustomizationTested = false;
    private bool _getCustomizationWordsTested = false;
    private bool _addCustomizationWordsTested = false;
    private bool _deleteCustomizationWordTested = false;
    private bool _getCustomizationWordTested = false;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    private IEnumerator CreateService()
    {
        //  Create credential and instantiate service
        Credentials credentials = null;
        if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
        {
            //  Authenticate using username and password
            credentials = new Credentials(_username, _password, _serviceUrl);
        }
        else if (!string.IsNullOrEmpty(_iamApikey))
        {
            //  Authenticate using iamApikey
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = _iamApikey,
                IamUrl = _iamUrl
            };

            credentials = new Credentials(tokenOptions, _serviceUrl);

            //  Wait for tokendata
            while (!credentials.HasIamTokenData())
                yield return null;
        }
        else
        {
            throw new WatsonException("Please provide either username and password or IAM apikey to authenticate the service.");
        }

        _service = new TextToSpeech(credentials);
        readyToSpeak = true;

        //Runnable.Run(MyTest());

        // SpeakText(netalk);
    }

    string textToSpeak;
    bool textNotEmpty;

    private int sp_pitch = 0;
    private int sp_pitchRange = 0;
    private int sp_rate = 0;
    private int sp_glottalTension = 0;
    private int sp_breathiness = 0;

    public void SetTone(AgentController nik)
    {
        float O = nik.openness;
        float C = nik.conscientiousness;
        float E = nik.extraversion;
        float A = nik.agreeableness;
        float N = nik.neuroticism;

        sp_pitch = (int) (((O + (C + E) / 2 - (A + N) / 2) / 3) * 100f);
        sp_pitchRange = (int) ((O + E) / 2 * 100f);
        sp_rate = (int) ((-O -C + E - A) / 4 * 75f);

    }

    public void ClearText()
    {
        textToSpeak = "<speak version=\"1.0\">";
        textToSpeak += "<voice-transformation type=\"Custom\" pitch=\" " + sp_pitch 
            + " % \" pitch_range =\"" + sp_pitchRange 
            + " % \" rate =\"" + sp_rate 
            + " % \" breathiness =\"" + sp_breathiness
            + " % \" glottal_tension =\" " + sp_glottalTension 
            + " % \" >";
        currentPlainTalkText = "";
        textNotEmpty = false;
    }

    public void AddText(string text)
    {
        textNotEmpty = true;

        currentPlainTalkText += text;
    }

    public void SetTextOCEANForCurrentText(TextOCEAN o)
    {
        currentSpeechOCEAN = o;
    }

    public void FinalizeText()
    {
        textToSpeak += "</voice-transformation>" + "</speak>";
    }
    private AgentController agentToTalkNewIK;

    public void SpeakText(AgentController currentAgent, bool isMale)
    {
        if(textNotEmpty)
        {
            FinalizeText();
            Debug.Log("Saying:" + textToSpeak);

            Runnable.Run(CallSpeakService(isMale));

            agentToTalkNewIK = currentAgent;
        }
        else
        {
            ClipStopped();
        }
    }

    private bool readyToSpeak = false;
    
    private IEnumerator CallSpeakService(bool isMale)
    {
        if (isMale)
        {
            _service.Voice = VoiceType.en_US_Michael;
        }
        else
        {
            _service.Voice = VoiceType.en_US_Allison;
        }
        _service.ToSpeech(HandleToSpeechCallback, OnFail, textToSpeak, true);

        while (!readyToSpeak)
            yield return null;
    }

    private IEnumerator MyTest()
    {
        //  Synthesize
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting synthesize.");
        _service.Voice = VoiceType.en_US_Allison;
        _service.ToSpeech(HandleToSpeechCallback, OnFail, _testString, true);
        while (!_synthesizeTested)
            yield return null;
    }

    private IEnumerator Examples()
    {
        //  Synthesize
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting synthesize.");
        _service.Voice = VoiceType.en_US_Allison;
        _service.ToSpeech(HandleToSpeechCallback, OnFail, _testString, true);
        while (!_synthesizeTested)
            yield return null;

        //	Get Voices
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get voices.");
        _service.GetVoices(OnGetVoices, OnFail);
        while (!_getVoicesTested)
            yield return null;

        //	Get Voice
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get voice {0}.", VoiceType.en_US_Allison);
        _service.GetVoice(OnGetVoice, OnFail, VoiceType.en_US_Allison);
        while (!_getVoiceTested)
            yield return null;

        //	Get Pronunciation
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get pronunciation of {0}", _testWord);
        _service.GetPronunciation(OnGetPronunciation, OnFail, _testWord, VoiceType.en_US_Allison);
        while (!_getPronuciationTested)
            yield return null;

        /*
        //  Get Customizations
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get a list of customizations");
        _service.GetCustomizations(OnGetCustomizations, OnFail);
        while (!_getCustomizationsTested)
            yield return null;

        //  Create Customization
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to create a customization");
        _service.CreateCustomization(OnCreateCustomization, OnFail, _customizationName, _customizationLanguage, _customizationDescription);
        while (!_createCustomizationTested)
            yield return null;

        //  Get Customization
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get a customization");
        if (!_service.GetCustomization(OnGetCustomization, OnFail, _createdCustomizationId))
            Log.Debug("ExampleTextToSpeech.Examples()", "Failed to get custom voice model!");
        while (!_getCustomizationTested)
            yield return null;

        //  Update Customization
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to update a customization");
        Word[] wordsToUpdateCustomization =
        {
            new Word()
            {
                word = "hello",
                translation = "hullo"
            },
            new Word()
            {
                word = "goodbye",
                translation = "gbye"
            },
            new Word()
            {
                word = "hi",
                translation = "ohioooo"
            }
        };

        _customVoiceUpdate = new CustomVoiceUpdate()
        {
            words = wordsToUpdateCustomization,
            description = "My updated description",
            name = "My updated name"
        };

        if (!_service.UpdateCustomization(OnUpdateCustomization, OnFail, _createdCustomizationId, _customVoiceUpdate))
            Log.Debug("ExampleTextToSpeech.Examples()", "Failed to update customization!");
        while (!_updateCustomizationTested)
            yield return null;

        //  Get Customization Words
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get a customization's words");
        if (!_service.GetCustomizationWords(OnGetCustomizationWords, OnFail, _createdCustomizationId))
            Log.Debug("ExampleTextToSpeech.GetCustomizationWords()", "Failed to get {0} words!", _createdCustomizationId);
        while (!_getCustomizationWordsTested)
            yield return null;

        //  Add Customization Words
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to add words to a customization");
        Word[] wordArrayToAddToCustomization =
        {
            new Word()
            {
                word = "bananna",
                translation = "arange"
            },
            new Word()
            {
                word = "orange",
                translation = "gbye"
            },
            new Word()
            {
                word = "tomato",
                translation = "tomahto"
            }
        };

        Words wordsToAddToCustomization = new Words()
        {
            words = wordArrayToAddToCustomization
        };

        if (!_service.AddCustomizationWords(OnAddCustomizationWords, OnFail, _createdCustomizationId, wordsToAddToCustomization))
            Log.Debug("ExampleTextToSpeech.AddCustomizationWords()", "Failed to add words to {0}!", _createdCustomizationId);
        while (!_addCustomizationWordsTested)
            yield return null;

        //  Get Customization Word
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to get the translation of a custom voice model's word.");
        string customIdentifierWord = wordsToUpdateCustomization[0].word;
        if (!_service.GetCustomizationWord(OnGetCustomizationWord, OnFail, _createdCustomizationId, customIdentifierWord))
            Log.Debug("ExampleTextToSpeech.GetCustomizationWord()", "Failed to get the translation of {0} from {1}!", customIdentifierWord, _createdCustomizationId);
        while (!_getCustomizationWordTested)
            yield return null;

        //  Delete Customization Word
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to delete customization word from custom voice model.");
        string wordToDelete = "goodbye";
        if (!_service.DeleteCustomizationWord(OnDeleteCustomizationWord, OnFail, _createdCustomizationId, wordToDelete))
            Log.Debug("ExampleTextToSpeech.DeleteCustomizationWord()", "Failed to delete {0} from {1}!", wordToDelete, _createdCustomizationId);
        while (!_deleteCustomizationWordTested)
            yield return null;

        //  Delete Customization
        Log.Debug("ExampleTextToSpeech.Examples()", "Attempting to delete a customization");
        if (!_service.DeleteCustomization(OnDeleteCustomization, OnFail, _createdCustomizationId))
            Log.Debug("ExampleTextToSpeech.DeleteCustomization()", "Failed to delete custom voice model!");
        while (!_deleteCustomizationTested)
            yield return null;

    */
        Log.Debug("ExampleTextToSpeech.Examples()", "Text to Speech examples complete.");
    }

    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData = null)
    {
        onScreenText.text = currentPlainTalkText;
        onScreenText.text += " [" + currentSpeechOCEAN + "]";
        onScreenText.color = Color.magenta;

        PlayClip(clip);
        Invoke("ClipStopped", clip.length + 0.1f);
    }

    void ClipStopped()
    {
        mainLogic.RefreshScreens();
        mainLogic.AfterSpeakEnd();
    }

    private void PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            if(agentToTalkNewIK != null)
            {
                agentToTalkNewIK.StartTalking();
            }

            Destroy(audioObject, clip.length);

            _synthesizeTested = true;
            readyToSpeak = true;
        }
    }

    private void OnGetVoices(Voices voices, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetVoices()", "Text to Speech - Get voices response: {0}", customData["json"].ToString());
        _getVoicesTested = true;
    }

    private void OnGetVoice(Voice voice, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetVoice()", "Text to Speech - Get voice  response: {0}", customData["json"].ToString());
        _getVoiceTested = true;
    }

    private void OnGetPronunciation(Pronunciation pronunciation, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetPronunciation()", "Text to Speech - Get pronunciation response: {0}", customData["json"].ToString());
        _getPronuciationTested = true;
    }

    private void OnGetCustomizations(Customizations customizations, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetCustomizations()", "Text to Speech - Get customizations response: {0}", customData["json"].ToString());
        _getCustomizationsTested = true;
    }

    private void OnCreateCustomization(CustomizationID customizationID, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnCreateCustomization()", "Text to Speech - Create customization response: {0}", customData["json"].ToString());
        _createdCustomizationId = customizationID.customization_id;
        _createCustomizationTested = true;
    }

    private void OnDeleteCustomization(bool success, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnDeleteCustomization()", "Text to Speech - Delete customization response: {0}", customData["json"].ToString());
        _createdCustomizationId = null;
        _deleteCustomizationTested = true;
    }

    private void OnGetCustomization(Customization customization, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetCustomization()", "Text to Speech - Get customization response: {0}", customData["json"].ToString());
        _getCustomizationTested = true;
    }

    private void OnUpdateCustomization(bool success, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnUpdateCustomization()", "Text to Speech - Update customization response: {0}", customData["json"].ToString());
        _updateCustomizationTested = true;
    }

    private void OnGetCustomizationWords(Words words, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetCustomizationWords()", "Text to Speech - Get customization words response: {0}", customData["json"].ToString());
        _getCustomizationWordsTested = true;
    }

    private void OnAddCustomizationWords(bool success, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnAddCustomizationWords()", "Text to Speech - Add customization words response: {0}", customData["json"].ToString());
        _addCustomizationWordsTested = true;
    }

    private void OnDeleteCustomizationWord(bool success, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnDeleteCustomizationWord()", "Text to Speech - Delete customization word response: {0}", customData["json"].ToString());
        _deleteCustomizationWordTested = true;
    }

    private void OnGetCustomizationWord(Translation translation, Dictionary<string, object> customData = null)
    {
        Log.Debug("ExampleTextToSpeech.OnGetCustomizationWord()", "Text to Speech - Get customization word response: {0}", customData["json"].ToString());
        _getCustomizationWordTested = true;
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleTextToSpeech.OnFail()", "Error received: {0}", error.ToString());
        ClipStopped();
    }
}
