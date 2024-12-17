using UnityEngine;
using UnityEngine.Events;

public class TheftsSensor : MonoBehaviour
{
    private int _theftsCountInHome;

    public event UnityAction TheftsComes;
    public event UnityAction TheftsLeaves;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TheftMover _theft))
            if (_theftsCountInHome++ == 0)
                TheftsComes.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TheftMover _theft))
            if(--_theftsCountInHome == 0)
                TheftsLeaves.Invoke();
    }

}
