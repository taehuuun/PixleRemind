using System.Threading.Tasks;
using UnityEngine;
using LTH.PixelRemind.Managers.FirebaseHandlers;

namespace LTH.PixelRemind.Managers.FirebaseController
{
    public class TopicDataController : MonoBehaviour
    {
        private readonly FirestoreHandler _firestore;

        public TopicDataController(FirestoreHandler firestore)
        {
            _firestore = firestore;
        }

        public async Task CheckAndUpdateTopic()
        {
            
        }
    }
}
