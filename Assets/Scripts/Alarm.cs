using System.Collections;
using UnityEngine;
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TheftsSensor _theftsSensor;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 0.3f;
    [SerializeField] private bool _isActive = true;
    
    private Coroutine _coroutine;

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
        TurnVolumeCoroutineStart(_maxVolume);
    }

    private void StopAlarm()
    {
        TurnVolumeCoroutineStart(_minVolume);
    }

    private void TurnVolumeCoroutineStart(float targetVolume)
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(TurnVolume(targetVolume));
    }

    private IEnumerator TurnVolume(float targetVolume)
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.volume = _minVolume;
            _audioSource.Play();
        }

        while (_isActive)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);

            if (_audioSource.volume == targetVolume)
            {
                if (_audioSource.volume == _minVolume)
                    _audioSource.Pause();

                StopCoroutine(_coroutine);
            }

            yield return null;
        }

    }
}
