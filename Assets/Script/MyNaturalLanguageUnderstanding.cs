using FullSerializer;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.NaturalLanguageUnderstanding.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyNaturalLanguageUnderstanding : MonoBehaviour {

    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/natural-language-understanding/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Tooltip("The version date with which you would like to use the service in the form YYYY-MM-DD.")]
    [SerializeField]
    private string _versionDate;
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

    NaturalLanguageUnderstanding _service;

    private bool _getModelsTested = false;
    private bool _analyzeTested = false;

    private bool readyToWork = false;

    [Header("Additional Parameters")]
    public string analyzeString;

    private AgentController currentAvatarToEmote;

    Parameters parameters = new Parameters()
    {
        text = "New text is here!",
        return_analyzed_text = true,
        language = "en",
        features = new Features()
        {
            emotion = new EmotionOptions()
            {
                document = true,
            }
        }
    };

    void Start()
    {
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    public void GiveTextToUnderstand(AgentController avatar, string given)
    {
        if (!readyToWork) { Debug.Log("NLU NOT READY!"); return; }

        currentAvatarToEmote = avatar;
        parameters.text = given;
        Runnable.Run(Work());
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

        _service = new NaturalLanguageUnderstanding(credentials);
        _service.VersionDate = _versionDate;

        readyToWork = true;
    }

    private IEnumerator Work()
    {
        _service.Analyze(OnAnalyze, OnFail, parameters);

        while (!readyToWork)
            yield return null;
    }

    public bool saveAnalyzeOn;

    private string[] saveNames;
    private string[] saveTexts;

    private int saveIndex;

    public void GiveForSaving(string[] saveNames, string[] saveTexts)
    {
        this.saveNames = saveNames;
        this.saveTexts = saveTexts;

        saveAnalyzeOn = true;

        saveIndex = 0;

        SaveNextOne();
    }

    public void SaveNextOne()
    {
        if (!readyToWork) { Debug.Log("NLU NOT READY!"); return; }

        parameters.text = saveTexts[saveIndex];
        Debug.Log("Save Line: " + parameters.text);
        Runnable.Run(Work());
    }

    private void OnAnalyze(AnalysisResults resp, Dictionary<string, object> customData)
    {
        Debug.Log("NLU: " + resp.emotion.document.emotion.anger + " "
                + resp.emotion.document.emotion.disgust + " "
                + resp.emotion.document.emotion.sadness + " "
                + resp.emotion.document.emotion.joy + " "
                + resp.emotion.document.emotion.fear);

        if (saveAnalyzeOn)
        {
            // save the result
            StreamWriter writer = new StreamWriter("Assets\\0_SOUNDDB\\E3\\" + saveNames[saveIndex] + ".txt",false);
            writer.WriteLine(
                resp.emotion.document.emotion.anger + " " 
                + resp.emotion.document.emotion.disgust + " " 
                + resp.emotion.document.emotion.sadness + " "
                + resp.emotion.document.emotion.joy + " " 
                + resp.emotion.document.emotion.fear + " "
                + 0f
                );
            writer.Close();

            saveIndex++;
        }
        else
        {
            currentAvatarToEmote.e_angry = resp.emotion.document.emotion.anger;
            currentAvatarToEmote.e_disgust = resp.emotion.document.emotion.disgust;
            currentAvatarToEmote.e_sad = resp.emotion.document.emotion.sadness;
            currentAvatarToEmote.e_happy = resp.emotion.document.emotion.joy;
            currentAvatarToEmote.e_fear = resp.emotion.document.emotion.fear;
            currentAvatarToEmote.e_shock = 0f;
        }
  
        _analyzeTested = true;
        readyToWork = true;

        if (saveAnalyzeOn)
        {
            if(saveIndex < saveNames.Length)
            {
                SaveNextOne();
            }
        }
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        if (saveAnalyzeOn)
        {
            // save the result
            StreamWriter writer = new StreamWriter("Assets\\0_SOUNDDB\\E0\\" + saveNames[saveIndex] + ".txt", false);
            writer.WriteLine(
                0f + " "
                + 0f + " "
                + 0f + " "
                + 0f + " "
                + 0f + " "
                + 0f
                );
            writer.Close();

            saveIndex++;
        }
        else
        {
            currentAvatarToEmote.e_angry = 0f;
            currentAvatarToEmote.e_disgust = 0f;
            currentAvatarToEmote.e_sad = 0f;
            currentAvatarToEmote.e_happy = 0f;
            currentAvatarToEmote.e_fear = 0f;
            currentAvatarToEmote.e_shock = 0f;
        }

        if (saveAnalyzeOn)
        {
            if (saveIndex < saveNames.Length)
            {
                SaveNextOne();
            }
        }
    }
}
