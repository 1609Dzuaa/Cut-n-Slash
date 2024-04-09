using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

[System.Serializable]
public struct PoolableObject
{
    public EPoolable _ePoolable;
    public GameObject _GObjPoolable;
    public int _ammount;
}

public class PoolManager : BaseSingleton<PoolManager>
{
    [SerializeField] List<PoolableObject> _listPoolableObj = new();

    Dictionary<EPoolable, List<GameObject>> _dictPool = new();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        FillDictionary();
        InstantiateGameObjects();
    }

    private void FillDictionary()
    {
        for (int i = 0; i < _listPoolableObj.Count; i++)
            if (!_dictPool.ContainsKey(_listPoolableObj[i]._ePoolable))
                _dictPool.Add(_listPoolableObj[i]._ePoolable, new());
    }

    private void InstantiateGameObjects()
    {
        for (int i = 0; i < _listPoolableObj.Count; i++)
        {
            for (int j = 0; j < _listPoolableObj[i]._ammount; j++)
            {
                GameObject gObj = Instantiate(_listPoolableObj[i]._GObjPoolable);
                gObj.SetActive(false);
                _dictPool[_listPoolableObj[i]._ePoolable].Add(gObj);
            }
        }
    }

    public GameObject GetObjectInPool(EPoolable objType)
    {
        for (int i = 0; i < _dictPool[objType].Count; i++)
        {
            //Tìm xem trong cái pool có thằng nào 0 kích hoạt kh thì lôi nó ra
            if (!_dictPool[objType][i].activeInHierarchy)
            {
                //Debug.Log("Bullet: " + _dictPool[bulletType][i].name + " " + i);
                return _dictPool[objType][i];
            }
        }

        Debug.Log("out of " + objType);
        return null;
    }
}
