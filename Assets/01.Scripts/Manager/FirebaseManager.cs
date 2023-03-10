using UnityEngine;
using Firebase.Firestore;
namespace LTH.ColorMatch.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        FirebaseFirestore db;

        private void Start()
        {
            db = FirebaseFirestore.DefaultInstance;
        }
    }
}
