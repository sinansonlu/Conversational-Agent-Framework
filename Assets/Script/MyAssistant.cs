using FullSerializer;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.Assistant.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAssistant : MonoBehaviour {

    public MainLogic mainLogic;

    private String workspaceID_passport = "fcfb2f3c-ccfe-4abe-b8e9-251b5905d700";
    private String workspaceID_fastfood = "12fa3f36-22c1-4948-86ae-51af12322b7a";

    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/assistant/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Tooltip("The workspaceId to run the example.")]
    [SerializeField]
    private string _workspaceId;
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

    private string _createdWorkspaceId;

    private Assistant _service;

    private fsSerializer _serializer = new fsSerializer();

    private Dictionary<string, object> _context = null;

    void Start()
    {
        if(mainLogic == null)
        {
            Debug.LogError("Main Logic is not set in MyAssistant.");
        }

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

        _service = new Assistant(credentials);
        _service.VersionDate = _versionDate;

        _recognizeFinished = true;
    }

    private string givenText;
    private bool _recognizeFinished = false;

    public void GiveTextToRecognize(string text)
    {
        if(_recognizeFinished && !text.Equals(""))
        {
            _recognizeFinished = false;

            givenText = text;
            Runnable.Run(RecognizeGivenText());
        }
    }

    private void CallAfterRecognition(EntityIntent e)
    {
        mainLogic.AfterIntentFound(e);
    }

    private IEnumerator RecognizeGivenText()
    {
        Dictionary<string, object> input = new Dictionary<string, object>();
        input.Add("text", givenText);
        MessageRequest messageRequest = new MessageRequest()
        {
            Input = input
        };
        _service.Message(OnMessage, OnFail, _workspaceId, messageRequest);
        
        while (!_recognizeFinished)
            yield return null;
    }

    public void SetWorkspaceToScenario(ScenarioType st)
    {
        if(st == ScenarioType.PASSPORT)
        {
            _workspaceId = workspaceID_passport;
        }
        else if (st == ScenarioType.FASTFOOD)
        {
            _workspaceId = workspaceID_fastfood;
        }
    }

    private void OnMessage(object response, Dictionary<string, object> customData)
    {
        Log.Debug("ExampleAssistant.OnMessage()", "Response: {0}", customData["json"].ToString());

        //  Convert resp to fsdata
        fsData fsdata = null;
        fsResult r = _serializer.TrySerialize(response.GetType(), response, out fsdata);
        if (!r.Succeeded)
            throw new WatsonException(r.FormattedMessages);

        //  Convert fsdata to MessageResponse
        MessageResponse messageResponse = new MessageResponse();
        object obj = messageResponse;
        r = _serializer.TryDeserialize(fsdata, obj.GetType(), ref obj);
        if (!r.Succeeded)
            throw new WatsonException(r.FormattedMessages);

        //  Set context for next round of messaging
        object _tempContext = null;
        (response as Dictionary<string, object>).TryGetValue("context", out _tempContext);

        if (_tempContext != null)
            _context = _tempContext as Dictionary<string, object>;
        else
            Log.Debug("ExampleAssistant.OnMessage()", "Failed to get context");

        //  Get entity
        object tempEntitiesObj = null;
        string entity = "";
        (response as Dictionary<string, object>).TryGetValue("entities", out tempEntitiesObj);
        if ((tempEntitiesObj as List<object>).Count > 0)
        {
            object tempIntentObj = (tempEntitiesObj as List<object>)[0];
            object tempIntent = null;
            (tempIntentObj as Dictionary<string, object>).TryGetValue("value", out tempIntent);
            entity = tempIntent.ToString();
        }

        //  Get intent
        object tempIntentsObj = null;
        string intent = "";
        (response as Dictionary<string, object>).TryGetValue("intents", out tempIntentsObj);
        if((tempIntentsObj as List<object>).Count > 0)
        {
            object tempIntentObj = (tempIntentsObj as List<object>)[0];
            object tempIntent = null;
            (tempIntentObj as Dictionary<string, object>).TryGetValue("intent", out tempIntent);
            intent = tempIntent.ToString();
        }

        EntityIntent ei = new EntityIntent();
        ei.Entity = entity;
        ei.Intent = intent;
        CallAfterRecognition(ei);
        
        // Log.Debug("ExampleAssistant.OnMessage()", "intent: {0}", intent);
        
        _recognizeFinished = true;
    }
    
    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Debug("ExampleAssistant.OnFail()", "Response: {0}", customData["json"].ToString());
        Log.Error("TestAssistant.OnFail()", "Error received: {0}", error.ToString());
    }

}
