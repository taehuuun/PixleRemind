using System.Threading.Tasks;
using UnityEngine;

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