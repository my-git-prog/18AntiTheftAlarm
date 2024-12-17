using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 0.1f;
    [SerializeField] private float _volumeChangeTime = 1f;

    private bool isAlarm = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TheftMover _theft))
            StartCoroutine(TurnVolumeUp());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TheftMover _theft))
            StartCoroutine(TurnVolumeDown());
    }

    private IEnumerator TurnVolumeUp()
    {
        isAlarm = true;

        if (_audioSource.isPlaying == false)
        {
            _audioSource.volume = _minVolume;
            _audioSource.Play();
        }

        var wait = new WaitForSeconds(_volumeChangeTime);

        while (isAlarm)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeChangeSpeed);

            if (_audioSource.volume == _maxVolume)
                yield break;

            yield return wait;
        }

        yield break;
    }

    private IEnumerator TurnVolumeDown()
    {
        isAlarm = false;

        var wait = new WaitForSeconds(_volumeChangeTime);

        while (isAlarm == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeChangeSpeed);

            if (_audioSource.volume == _minVolume)
            {
                _audioSource.Stop();
                yield break;
            }

            yield return wait;
        }

        yield break;
    }
}
