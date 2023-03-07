using System.Collections;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class MoveUI : MonoBehaviour
    {
        [SerializeField] private RectTransform moveUI;
        [SerializeField] private Vector2 targetPos;
        [SerializeField] private float moveSpeed;

        private bool isMoving;
        private Vector2 currentPos;
        private Vector2 originPos;

        public bool isComplete;
        
        private void Start()
        {
            var anchoredPosition = moveUI.anchoredPosition;
            currentPos = anchoredPosition; // 시작 위치 초기화
            originPos = anchoredPosition;
        }

        public void StartMove()
        {
            if (!isMoving) // 이동 중이 아닐 때만 실행
            {
                StopAllCoroutines();
                StartCoroutine(MoveCoroutine(currentPos, targetPos)); // 코루틴 시작
            }
        }

        public void Return()
        {
            if (!isMoving) // 이동 중이 아닐 때만 실행
            {
                StopAllCoroutines();
                StartCoroutine(MoveCoroutine(currentPos,originPos)); // 코루틴 시작
            }
        }

        private IEnumerator MoveCoroutine(Vector2 start,Vector2 target)
        {
            isMoving = true; // 이동 중으로 변경
            isComplete = false;
            float elapsedTime = 0f; // 경과 시간 초기화

            while (elapsedTime < moveSpeed) // 이동 속도에 따라 이동 시간 계산
            {
                elapsedTime += Time.deltaTime; // 경과 시간 누적
                moveUI.anchoredPosition = Vector2.Lerp(start, target, elapsedTime / moveSpeed); // 선형 보간으로 위치 조정
                yield return null; // 한 프레임 대기
            }
            
            moveUI.anchoredPosition = target; // 목표 위치에 정확히 도달하도록 위치 조정
            currentPos = target;
            isMoving = false; // 이동 중 해제
            isComplete = true;
        }
    }
}
