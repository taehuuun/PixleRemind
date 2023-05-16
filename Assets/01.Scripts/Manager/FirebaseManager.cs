using UnityEngine;
using LTH.ColorMatch.Handlers;

namespace LTH.ColorMatch.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager ins;
        public FirestoreHandler Firestore;
        public FirebaseAuthHandler FirebaseAuth;

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
            FirebaseAuth = new FirebaseAuthHandler();
        }
    }
}