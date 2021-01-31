using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using Web.Helpers;
using Web.Twitter.API;
using Web.Twitter.DataStructures;

public class Twitter : MonoBehaviour
{
    public string TwitterApiConsumerKey;
    public string TwitterApiConsumerSecret;
    public Text tweet_text;

    public Button a1, a2, a3, a4;

    public WebAccessToken TwitterApiAccessToken;

   
    public string[] celebrities = { "kanyewest", "justinbieber", "taylorswift13", "jaden", "elonmusk", "JLo", "SnoopDogg", "Sethrogen", "icecube", "LilNasX", "KylieJenner", "chrissyteigen", "tyrabanks", "rihanna", "KimKardashian", "KrisJenner", "50cent", "MarthaStewart", "ladygaga", "iamcardib", "MoistCr1TiKaL", "ArianaGrande", "jaboukie", "souljaboy", "Drake", "tylerthecreator" };
    public string[] politicians = { "JoeBiden", "berniesanders", "tedcruz", "BarackObama", "KamalaHarris", "GOPLeader", "mattgaetz", "GOPChairwoman", "HillaryClinton", "DonaldJTrumpJr", "MittRomney", "NYGovCuomo", "GovRonDeSantis", "AOC", "BillClinton", "SpeakerPelosi", "benshapiro", "TomiLahren" };

    int answer;
    int[] answers = { -1, -1, -1, -1 };
    int tweet_num;
    string nameAnswer;

    int catigory = 1;

    System.Random random;
    // Start is called before the first frame update

    [Header("GetLatestTweetsFromUserByScreenName")]
    public Tweet[] tweets;

    void Start()
    {
        TwitterApiAccessToken = WebHelper.GetTwitterApiAccessToken(TwitterApiConsumerKey, TwitterApiConsumerSecret);
        random = new System.Random(DateTime.Now.Millisecond);
        nextQuestion(getCatarray());


    }

    string[] getCatarray()
    {
        switch (catigory)
        {
            case 0:
                return celebrities;
            case 1:
                return politicians;
            default:
                return null;
        }
    }

    Task<Tweet[]> tweetsRequest;
    public void newTweets(String name)
    {
        tweetsRequest = TwitterRestApiHelper.GetLatestTweetsFromUserByScreenName(name, this.TwitterApiAccessToken.access_token);
    }

    void nextQuestion(String[] names)
    {
        int j = random.Next(getCatarray().Length-1);
        answer = j;

        nameAnswer = getCatarray()[j];

        answers[0] = j;

        for (int i = 1; i < answers.Length; i++)
        {
            int temp = random.Next(getCatarray().Length-1);
            for (int p = 0; p < answers.Length; p++)
            {
                while(temp == answers[p])
                    temp = random.Next(getCatarray().Length - 1);
            }
            answers[i] = temp;
        }

        newTweets(getCatarray()[answer]);

        for (int w = 0; w < answers.Length - 1; w++)
        {
            int z = random.Next(w, answers.Length);
            int temp = answers[w];
            answers[w] = answers[z];
            answers[z] = temp;
        }

        tweet_num = random.Next(tweets.Length - 1);

    }
    
    public void checkButtonAnswer()
    {
        var go = EventSystem.current.currentSelectedGameObject;


        if (go.GetComponentInChildren<Text>().text == nameAnswer)
            nextQuestion(getCatarray());

    }



    // Update is called once per frame
    void Update()
    {   
        if(tweetsRequest != null && tweetsRequest.IsCompleted)
        {
            tweets = tweetsRequest.Result;

            while(tweet_num >= tweets.Length)
            {
                tweet_num = random.Next(tweets.Length - 1);
            }

            tweet_text.text = tweets[tweet_num].text;

            a1.GetComponentInChildren<Text>().text = getCatarray()[answers[0]];
            a2.GetComponentInChildren<Text>().text = getCatarray()[answers[1]];
            a3.GetComponentInChildren<Text>().text = getCatarray()[answers[2]];
            a4.GetComponentInChildren<Text>().text = getCatarray()[answers[3]];

        }
    }
}
