using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _block;
    [SerializeField] private int _blocksGeneratedAtStart;
    private GroundBlock _currentBlock;

    public List<GroundBlock> _blocks;

    private void Start()
    {
        StartCoroutine(GenetareInitialBlocks());
    }

    private void GenerateBlock()
    {
        _currentBlock = _blocks[_blocks.Count - 1];

        Vector3 newBlockPosition = _currentBlock.transform.position + new Vector3(0, 0, _currentBlock.transform.localScale.z * 10);
        Instantiate(_block, newBlockPosition, Quaternion.identity);
    }

    public void UpdateBlocks()
    {
        GenerateBlock();
        if (_blocks.Count>=3)
                Destroy(_blocks[0].gameObject);
    }

    private IEnumerator GenetareInitialBlocks()
    {
        for (int i = 0; i < _blocksGeneratedAtStart; i++)
        {
            GenerateBlock();
            yield return null;
        }
    }
}