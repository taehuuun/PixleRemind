using System.Threading.Tasks;
using UnityEngine;
using Firebase.Firestore;

namespace LTH.ColorMatch.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        FirebaseFirestore db;

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            db = FirebaseFirestore.DefaultInstance;
        }

        public async Task<bool> CheckDocumentExists(string collection, string document)
        {
            var docRef = db.Collection(collection).Document(document);
            var docSnapShot = await docRef.GetSnapshotAsync();

            return docSnapShot.Exists;
        }
    }
}
