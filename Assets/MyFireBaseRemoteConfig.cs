using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Firebase.Extensions;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class MyFireBaseRemoteConfig : MonoBehaviour
{
    //#region Singelton

    //public static MyFireBaseRemoteConfig Instance;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }

    //    DontDestroyOnLoad(gameObject);
    //}

    //#endregion

    [XmlAttribute("remote_config_defaults")]
    public int deckId;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    protected bool isFirebaseInitialized = false;

    void Update()
    {

    }
    protected virtual void Start()
    {
        FetchDataAsync();





        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        }
        );
    }

    public void FetchFireBase()
    {
        FetchDataAsync();
    }

    public void ShowData()
    {
        //Debug.Log("config_test_string: " +
        //         Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
        //             .GetValue("config_test_string").StringValue);

        Debug.Log("Currency: " +
                 Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
                     .GetValue("Currency").LongValue);

        //Debug.Log("config_test_float: " +
        //         Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
        //             .GetValue("config_test_float").DoubleValue);

        //Debug.Log("config_test_bool: " +
        //         Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
        //             .GetValue("config_test_bool").BooleanValue);
    }
    void InitializeFirebase()
    {
        // [START set_defaults]
        System.Collections.Generic.Dictionary<string, object> defaults =
            new System.Collections.Generic.Dictionary<string, object>();

        // These are the values that are used if we haven't fetched data from the
        // server
        // yet, or if we ask for values that the server doesn't have:
        defaults.Add("config_test_string", "default local string");
        defaults.Add("config_test_int", 1);
        defaults.Add("config_test_float", 1.0);
        defaults.Add("config_test_bool", false);

        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
            .ContinueWithOnMainThread(task =>
            {
                // [END set_defaults]
                Debug.Log("RemoteConfig configured and ready!");
                isFirebaseInitialized = true;
                FetchFireBase();
            });

    }
    // Start a fetch request.
    // FetchAsync only fetches new data if the current data is older than the provided
    // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
    // By default the timespan is 12 hours, and for production apps, this is a good
    // number. For this example though, it's set to a timespan of zero, so that
    // changes in the console will always show up immediately.




[XmlRoot(ElementName = "entry")]
public class Entry
{

    [XmlElement(ElementName = "key")]
    public string Key { get; set; }

    [XmlElement(ElementName = "value")]
    public int Value { get; set; }
}

[XmlRoot(ElementName = "defaults")]
public class Defaults
{

    [XmlElement(ElementName = "entry")]
    public List<Entry> Entry { get; set; }
}


private void DefultConfig()
{
    Defaults defaults;

    XmlSerializer serializer = new XmlSerializer(typeof(Defaults));
    using (StringReader reader = new StringReader(Resources.Load<TextAsset>("remote_config_defaults").text))
    {
        defaults = (Defaults)serializer.Deserialize(reader);
    }

    var dict = new Dictionary<string, object>();

    foreach (var entry in defaults.Entry)
    {
        dict.Add(entry.Key, entry.Value);
    }

    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(dict);
    }
public Task FetchDataAsync()
{

        //If internet not found
        DefultConfig();

        Debug.Log("Fetching data...");
    System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
    return fetchTask.ContinueWithOnMainThread(FetchComplete);
}
//[END fetch_async]
void FetchComplete(Task fetchTask)
{
    if (fetchTask.IsCanceled)
    {
        Debug.Log("Fetch canceled.");
    }
    else if (fetchTask.IsFaulted)
    {
        Debug.Log("Fetch encountered an error.");

    }
    else if (fetchTask.IsCompleted)
    {
        Debug.Log("Fetch completed successfully!");
    }

    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
    switch (info.LastFetchStatus)
    {
        case Firebase.RemoteConfig.LastFetchStatus.Success:
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
            .ContinueWithOnMainThread(task =>
            {
                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                               info.FetchTime));
            });

            break;
        case Firebase.RemoteConfig.LastFetchStatus.Failure:
            switch (info.LastFetchFailureReason)
            {
                case Firebase.RemoteConfig.FetchFailureReason.Error:
                    Debug.Log("Fetch failed for unknown reason");

                    break;
                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                    Debug.Log("Fetch throttled until " + info.ThrottledEndTime);

                    break;
            }
            break;
        case Firebase.RemoteConfig.LastFetchStatus.Pending:
            Debug.Log("Latest Fetch call still pending.");
            break;
    }
}

}