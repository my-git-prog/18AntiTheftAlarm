using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TheftsSensor _theftsSensor;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 0.3f;

    private bool _needTurnVolume = false;
    private float _targetVolume = 0f;

    private void OnEnable()
    {
        _theftsSensor.TheftsComes += StartAlarm;
        _theftsSensor.TheftsLeaves += StopAlarm;
    }

    private void OnDisable()
    {
        _theftsSensor.TheftsComes -= StartAlarm;
        _theftsSensor.TheftsLeaves -= StopAlarm;
    }

    private void StartAlarm()
    {
        _targetVolume = _maxVolume;
        TurnVolumeCoroutineStart();
    }

    private void StopAlarm()
    {
        _targetVolume = _minVolume;
        TurnVolumeCoroutineStart();
    }

    private void TurnVolumeCoroutineStart()
    {
        if(_needTurnVolume == false)
            StartCoroutine(TurnVolume());
    }

    private IEnumerator TurnVolume()
    {
        _needTurnVolume = true;

        if (_audioSource.isPlaying == false)
        {
            _audioSource.volume = _minVolume;
            _audioSource.Play();
        }

        while (_needTurnVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);

            if (_audioSource.volume == _targetVolume)
                _needTurnVolume = false;

            yield return null;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Pause();
    }
}
