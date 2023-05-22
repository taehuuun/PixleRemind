using UnityEngine;
using LTH.ColorMatch.Managers.FirebaseHandlers;

namespace LTH.ColorMatch.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        // 싱글턴 패턴이 적용되는 정적 인스턴스
        public static FirebaseManager ins;
        
        // FirestoreHandler와 FirebaseAuthHandler 객체에 대한 참조
        public FirestoreHandler Firestore { get; private set; }
        public FirebaseAuthHandler FireAuth { get; private set; }

        private void Awake()
        {
            // 싱글톤 패턴
            if (ins == null)
            {
                ins = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            // FirestoreHandler와 FirebaseAuthHandler 인스턴스를 생성
            Firestore = new FirestoreHandler();
            FireAuth = new FirebaseAuthHandler();
        }
    }
}