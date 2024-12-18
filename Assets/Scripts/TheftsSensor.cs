using System;
using UnityEngine;

public class TheftsSensor : MonoBehaviour
{
    private int _theftsCountInHome;

    public event Action TheftsComes;
    public event Action TheftsLeaves;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TheftMover theft))
            if (_theftsCountInHome++ == 0)
                TheftsComes.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TheftMover theft))
            if(--_theftsCountInHome == 0)
                TheftsLeaves.Invoke();
    }

}
