using UnityEngine;
using UnityEngine.Events;

public class GroundBlock : MonoBehaviour
{
    private GroundGenerator _generator;
    public UnityEvent OnEndOfBlockReached = new UnityEvent();
    [SerializeField] private Transform[] _spawns;
    [SerializeField] private GameObject[] _obstacles;

    private void Awake()
    {
        _generator = GameObject.Find("PlaceForScripts").GetComponent<GroundGenerator>();
        _generator._blocks.Add(this);
        OnEndOfBlockReached.AddListener(_generator.UpdateBlocks);
        GameObject obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Length - 1)], _spawns[Random.Range(0, _spawns.Length - 1)]);
        obstacle.transform.Rotate(new Vector3(0,Random.Range(0,180),0),Space.World);
        //Debug.Log(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered " + other.tag);
        if (other.GetComponent<Player>())
            OnEndOfBlockReached.Invoke();
    }

    private void OnDestroy()
    {
        _generator._blocks.Remove(this);
    }
}
