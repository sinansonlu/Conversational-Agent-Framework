using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;
using System.IO;

public class MyTextToSpeech : MonoBehaviour {
    public MainLogic mainLogic;
    public AudioSource lipAudioSource;
    public MyVRLipSyncPasser myVRLipSyncPasser;

    public bool OnlySaveToWav;

    public Text onScreenText;

    private string currentPlainTalkText;
    private TextOCEAN currentSpeechOCEAN;

    // save parameters
    private string[] saveNames;
    private string[] saveTexts;
    private OneOCEAN[] saveOceans;
    private AgentGender[] saveGenders;
    private int saveIndex;
    private int saveCount;

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

    private bool _synthesizePlayed = false;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    public void SpeakFromClip(AudioClip ac, string text, AgentController agent)
    {
        onScreenText.text = text;
        // onScreenText.text += " [" + currentSpeechOCEAN + "]";
        onScreenText.color = Color.magenta;

        agentToTalkNewIK = agent;

        agentToTalkNewIK.StartTalking();
        myVRLipSyncPasser.currentFace = agentToTalkNewIK.faceController;

        lipAudioSource.clip = ac;
        lipAudioSource.Play();

        Invoke("ClipStopped", ac.length + 0.1f);
    }

    public void SpeakFromClip(AgentResponse ar, AgentController agent)
    {
        onScreenText.text = ar.responseText;
        // onScreenText.text += " [" + currentSpeechOCEAN + "]";
        onScreenText.color = Color.magenta;

        agentToTalkNewIK = agent;

        agentToTalkNewIK.StartTalking();
        myVRLipSyncPasser.currentFace = agentToTalkNewIK.faceController;

        lipAudioSource.clip = ar.clip;
        lipAudioSource.Play();

        Invoke("ClipStopped", ar.clip.length + 0.1f);
    }

    public void GiveForSaving(string[] saveNames, string[] saveTexts)
    {
        this.saveNames = saveNames;
        this.saveTexts = saveTexts;
        saveIndex = 0;
        saveCount = saveNames.Length;

        OnlySaveToWav = true;

        SaveNextOne();
    }

    public void GiveForSavingManual(string[] saveNames, string[] saveTexts, OneOCEAN[] saveOceans, AgentGender[] saveGenders, int number)
    {
        this.saveNames = saveNames;
        this.saveTexts = saveTexts;
        this.saveOceans = saveOceans;
        this.saveGenders = saveGenders;

        saveIndex = 0;
        saveCount = saveNames.Length;

        StreamWriter writer = new StreamWriter("Assets\\0_SOUNDDB\\LinesNew" + number + ".txt", false);
        
        for (int i = 0; i < saveCount; i++)
        {
            SetSaveToneForCase(saveOceans[i], saveTexts[i], saveNames[i], writer);
        }

        writer.Close();

        Debug.Log("Assets\\0_SOUNDDB\\LinesNew" + number + ".txt");
    }

    public void GiveForSaving(string[] saveNames, string[] saveTexts, OneOCEAN[] saveOceans, AgentGender[] saveGenders)
    {
        this.saveNames = saveNames;
        this.saveTexts = saveTexts;
        this.saveOceans = saveOceans;
        this.saveGenders = saveGenders;
        
        saveIndex = 0;
        saveCount = saveNames.Length;

        OnlySaveToWav = true;

        SaveNextOneWithOCEAN();
    }

    private void SaveNextOne()
    {
        AgentResponse agentResponse = new AgentResponse("Empty!");
        agentResponse.responseText = saveTexts[saveIndex];

        SetTone(mainLogic.agentsController.GetJoe());
        SetTextOCEANForCurrentText(agentResponse.responseTextOCEAN);
        SpeakText(mainLogic.agentsController.GetJoe(), agentResponse.responseText);
    }

    private void SaveNextOneWithOCEAN()
    {
        AgentResponse agentResponse = new AgentResponse("Empty!");
        agentResponse.responseText = saveTexts[saveIndex];

        SetTone(saveOceans[saveIndex]);
        SetTextOCEANForCurrentText(agentResponse.responseTextOCEAN);
        SpeakText(mainLogic.agentsController.GetJoe(), agentResponse.responseText);
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
        _synthesizePlayed = true;
    }

    string textToSpeak;

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

        sp_pitch = (int)(((O + (C + E) / 2 - (A + N) / 2) / 3) * 80f);
        sp_pitchRange = (int)((O + E) / 2 * 100f);
        sp_rate = (int)((-O - C + E - A) / 4 * 70f);
        sp_breathiness = (int)((A + O) / 2 * 50f);
        sp_glottalTension = (int)((C - O - A) / 3 * 100f);
    }

    public void SetSaveToneForCase(OneOCEAN oo, string txt, string fname, StreamWriter writer)
    {
        int i = -1;
        if(oo.openness == 1)
        {
            i = 0;
        }
        else if (oo.openness == -1)
        {
            i = 1;
        }
        else if (oo.conscientiousness == 1)
        {
            i = 2;
        }
        else if (oo.conscientiousness == -1)
        {
            i = 3;
        }
        else if (oo.extraversion == 1)
        {
            i = 4;
        }
        else if (oo.extraversion == -1)
        {
            i = 5;
        }
        else if (oo.agreeableness == 1)
        {
            i = 6;
        }
        else if (oo.agreeableness == -1)
        {
            i = 7;
        }
        else if (oo.neuroticism == 1)
        {
            i = 8;
        }
        else if (oo.neuroticism == -1)
        {
            i = 9;
        }
        switch (i)
        {
            case 0: // O+
                sp_pitch = 60;
                sp_pitchRange = 100;
                sp_rate = 0;
                sp_breathiness = 20;
                sp_glottalTension = -40;
                break;
            case 1: // O-
                sp_pitch = -50;
                sp_pitchRange = -100;
                sp_rate = 30;
                sp_breathiness = -40;
                sp_glottalTension = 70;
                break;
            case 2: // C+
                sp_pitch = -30;
                sp_pitchRange = -20;
                sp_rate = -60;
                sp_breathiness = 20;
                sp_glottalTension = -20;
                break;
            case 3: // C-
                sp_pitch = -80;
                sp_pitchRange = -80;
                sp_rate = 30;
                sp_breathiness = 0;
                sp_glottalTension = -80;
                break;
            case 4: // E+
                sp_pitch = 80;
                sp_pitchRange = 100;
                sp_rate = 40;
                sp_breathiness = -20;
                sp_glottalTension = 50;
                break;
            case 5: // E-
                sp_pitch = -10;
                sp_pitchRange = -20;
                sp_rate = -30;
                sp_breathiness = 20;
                sp_glottalTension = -10;
                break;
            case 6: // A+
                sp_pitch = -90;
                sp_pitchRange = 10;
                sp_rate = -20;
                sp_breathiness = 50;
                sp_glottalTension = -40;
                break;
            case 7: // A-
                sp_pitch = 0;
                sp_pitchRange = -20;
                sp_rate = 30;
                sp_breathiness = -60;
                sp_glottalTension = 60;
                break;
            case 8: // N+
                sp_pitch = 10;
                sp_pitchRange = -70;
                sp_rate = 10;
                sp_breathiness = -40;
                sp_glottalTension = 80;
                break;
            case 9: // N-
                sp_pitch = 0;
                sp_pitchRange = 10;
                sp_rate = -10;
                sp_breathiness = 20;
                sp_glottalTension = -20;
                break;
        }

        /*
        sp_pitch = 0;
        sp_pitchRange = 0;
        sp_rate = 0;
        sp_breathiness = 0;
        sp_glottalTension = 0;
        */

        string pri = "<speak version=\"1.0\">";
        pri += "<voice-transformation type=\"Custom\" pitch=\" " + sp_pitch
            + " % \" pitch_range =\"" + sp_pitchRange
            + " % \" rate =\"" + sp_rate
            + " % \" breathiness =\"" + sp_breathiness
            + " % \" glottal_tension =\" " + sp_glottalTension
            + " % \" >";
        pri += txt;
        pri += "</voice-transformation>" + "</speak>";

        writer.WriteLine(pri);
        writer.WriteLine(fname);
        writer.WriteLine();


    }

    public void PrintAllTones()
    {
        CalculateTonePrint(1, 0, 0, 0, 0);
        CalculateTonePrint(-1, 0, 0, 0, 0);
        CalculateTonePrint(0, 1, 0, 0, 0);
        CalculateTonePrint(0, -1, 0, 0, 0);
        CalculateTonePrint(0, 0, 1, 0, 0);
        CalculateTonePrint(0, 0, -1, 0, 0);
        CalculateTonePrint(0, 0, 0, 1, 0);
        CalculateTonePrint(0, 0, 0, -1, 0);
        CalculateTonePrint(0, 0, 0, 0, 1);
        CalculateTonePrint(0, 0, 0, 0, -1);
    }

    private void CalculateTonePrint(float O, float C, float E, float A, float N)
    {
        sp_pitch = (int)(((O + (C + E) / 2 - (A + N) / 2) / 3) * 80f);
        sp_pitchRange = (int)((O + E) / 2 * 100f);
        sp_rate = (int)((-O - C + E - A) / 4 * 70f);
        sp_breathiness = (int)((A + O) / 2 * 50f);
        sp_glottalTension = (int)((C - O - A) / 3 * 100f);

        string pri = "<speak version=\"1.0\">";
        pri += "<voice-transformation type=\"Custom\" pitch=\" " + sp_pitch
            + " % \" pitch_range =\"" + sp_pitchRange
            + " % \" rate =\"" + sp_rate
            + " % \" breathiness =\"" + sp_breathiness
            + " % \" glottal_tension =\" " + sp_glottalTension
            + " % \" >";
        pri += "I think this is a test.";
        pri += "</voice-transformation>" + "</speak>";

        Debug.Log(pri);
    }

    public void SetTone(OneOCEAN ocean)
    {
        float O = ocean.openness;
        float C = ocean.conscientiousness;
        float E = ocean.extraversion;
        float A = ocean.agreeableness;
        float N = ocean.neuroticism;

        sp_pitch = (int)(((O + (C + E) / 2 - (A + N) / 2) / 3) * 80f);
        sp_pitchRange = (int)((O + E) / 2 * 100f);
        sp_rate = (int)((-O - C + E - A) / 4 * 70f);
        sp_breathiness = (int)((A + O) / 2 * 50f);
        sp_glottalTension = (int)((C - O - A) / 3 * 100f);

        Debug.Log("Setting tone: " + O + " " + C + " " + E + " " + A + " " + N + " ");
    }

    public void SetTextOCEANForCurrentText(TextOCEAN o)
    {
        currentSpeechOCEAN = o;
    }

    private AgentController agentToTalkNewIK;

    public void SpeakText(AgentController currentAgent, string text)
    {
        textToSpeak = "<speak version=\"1.0\">";
        textToSpeak += "<voice-transformation type=\"Custom\" pitch=\" " + sp_pitch
            + " % \" pitch_range =\"" + sp_pitchRange
            + " % \" rate =\"" + sp_rate
            + " % \" breathiness =\"" + sp_breathiness
            + " % \" glottal_tension =\" " + sp_glottalTension
            + " % \" >";
        currentPlainTalkText = "";

        textToSpeak += text;
        currentPlainTalkText += text;

        AgentGender gender = currentAgent.agentGender;

        if (OnlySaveToWav) gender = saveGenders[saveIndex];

        if (_synthesizePlayed)
        {
            _synthesizePlayed = false;

            textToSpeak += "</voice-transformation>" + "</speak>"; // finalize
            if(OnlySaveToWav)
            {
                Debug.Log("Saving to: " + saveNames[saveIndex] + ", " + textToSpeak);
            }
            else
            {
                Debug.Log("Saying:" + textToSpeak);
            }

            agentToTalkNewIK = currentAgent;

            Runnable.Run(CallSpeakService(gender));
        }
    }

    private IEnumerator CallSpeakService(AgentGender gender)
    {
        if (gender == AgentGender.Male)
        {
            _service.Voice = VoiceType.en_US_Michael;
        }
        else
        {
            _service.Voice = VoiceType.en_US_Allison;
        }
        _service.ToSpeech(HandleToSpeechCallback, OnFail, textToSpeak, true);

        while (!_synthesizePlayed)
            yield return null;
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
        if (agentToTalkNewIK != null)
        {
            agentToTalkNewIK.StopTalking();
        }
        mainLogic.AfterSpeakEnd();
    }

    public void TalkFromClip(AudioClip clip, AgentController agent)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;

            lipAudioSource.clip = clip;
            lipAudioSource.Play();
            
            // source.Play();

            agentToTalkNewIK = agent;
            if (agentToTalkNewIK != null)
            {
                agentToTalkNewIK.StartTalking();
                myVRLipSyncPasser.currentFace = agentToTalkNewIK.faceController;
            }

            Destroy(audioObject, clip.length);

            _synthesizePlayed = true;

        }

        Invoke("ClipStopped", clip.length + 0.1f);
    }

    private void PlayClip(AudioClip clip)
    {
        if (OnlySaveToWav)
        {
            SavWav.Save("0_SOUNDDB\\E0\\" + saveNames[saveIndex], clip);
            Debug.Log("Saving " + "0_SOUNDDB\\E0\\" + saveNames[saveIndex]);
            _synthesizePlayed = true;
            saveIndex++;
            if(saveIndex < saveCount)
            {
                SaveNextOneWithOCEAN();
            }
        }
        else if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            lipAudioSource.clip = clip;
            lipAudioSource.Play();

            // source.Play();

            if (agentToTalkNewIK != null)
            {
                agentToTalkNewIK.StartTalking();
                myVRLipSyncPasser.currentFace = agentToTalkNewIK.faceController;
            }

            Destroy(audioObject, clip.length);

            _synthesizePlayed = true;
        }
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleTextToSpeech.OnFail()", "Error received: {0}", error.ToString());
        ClipStopped();
    }
}
