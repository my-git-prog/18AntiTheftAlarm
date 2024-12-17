using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTheftAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 0.1f;
    [SerializeField] private float _volumeChangeTime = 1f;

    private bool isAlarm = false;
    private float _currentVolume = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TheftMover>() == null)
            return;

        StartCoroutine(GrowAlarmVolume());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TheftMover>() == null)
            return;

        StartCoroutine(LessAlarmVolume());
    }

    private IEnumerator GrowAlarmVolume()
    {
        isAlarm = true;

        if (_audioSource.isPlaying == false)
            _audioSource.volume = _minVolume;
            _audioSource.Play();

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

    private IEnumerator LessAlarmVolume()
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
