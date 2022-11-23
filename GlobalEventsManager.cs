using UnityEngine;
using UnityEngine.Events;

public class GlobalEventsManager : MonoBehaviour
{
    public static UnityEvent OnSomeGlobalStuffHappen = new UnityEvent();

}
