using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임 플레이 중인지를 체크
    // TopicData, PixelArtData를 로드 해야함
    private bool _isPlaying;
    private ColorMatchSystem _colorMatchSystem;
    private PixelArtData _selectPixelArtData;
    private TopicData _selectTopicData;
    private PlayUI _playUI;
}
