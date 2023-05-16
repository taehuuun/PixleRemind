using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Firestore;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Handlers;

namespace LTH.ColorMatch.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager ins;
        public FirestoreHandler Firestore;
        public FirebaseAuthHandler FireAuth;

        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            Firestore = new FirestoreHandler();
            FireAuth = new FirebaseAuthHandler();
        }
    }
}