﻿using SimpleFirebaseUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    private Firebase dbPostIt;

    public GameObject prefabPostIt;

    void Awake()
    {
        dbPostIt = Firebase.CreateNew("https://innlabvirtual-684a1.firebaseio.com", "AIzaSyDTllB8BJk_xOGI_4rLTNJozpRrKRrmOZA").Child("posts");

        dbPostIt.OnGetSuccess += GetOKHandler;
        dbPostIt.OnGetFailed += GetFailHandler;
    }

    // Start is called before the first frame update
    void Start()
    {

        dbPostIt.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void readPostIt()
    {

    }

    [Serializable]
    public class PostItDB
    {
        public string autor { get; set; }
        public string data { get; set; }

        public PostItDB(){}

        public PostItDB(string autor, string data) {
            this.data = data;
            this.autor = autor;
        }
    }

    void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        //Debug.Log("[OK] Get from key: <" + sender.FullKey + ">");
        //Debug.Log("[OK] Raw Json: " + snapshot.RawJson);

        //

        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        if (keys != null)
        {
            int z = 0;
            int y = 0;
            foreach (string key in keys)
            {
                Dictionary<string, object> newPostIt = dict[key] as Dictionary<string, object>;
                //Debug.Log(newPostIt["data"]);

                GameObject newPostItObject = Instantiate(prefabPostIt, new Vector3(-1.1f, 0.2f + (y * 0.35f), -0.2f + (z * 0.35f)), Quaternion.identity);
                newPostItObject.GetComponentInChildren<TMPro.TextMeshPro>().text = newPostIt["data"].ToString();

                z++;

                if (z >= 3)
                {
                    y++;
                    z = 0;
                }
            }
        }
    }

    void GetFailHandler(Firebase sender, FirebaseError err)
    {
        Debug.LogError("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }

}