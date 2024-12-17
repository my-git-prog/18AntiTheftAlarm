using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TheftsSensor _theftsSensor;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 0.3f;
    [SerializeField] private bool _isAlarmWorking = true;

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

    private void Start()
    {
        StartCoroutine(TurnVolume());
    }

    private void StartAlarm()
    {
        if(_audioSource.isPlaying == false)
        {
            _audioSource.volume = _minVolume;
            _audioSource.Play();
        }

        _targetVolume = _maxVolume;
    }

    private void StopAlarm()
    {
        _targetVolume = _minVolume;
    }

    private IEnumerator TurnVolume()
    {
        while (_isAlarmWorking)
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);

                if (_audioSource.volume == _minVolume)
                    _audioSource.Pause();
            }

            yield return null;
        }
    }
}
