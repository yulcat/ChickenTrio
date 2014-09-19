using UnityEngine;
using System.Collections;

partial class Tile : MonoBehaviour
{
    private Monster _Monster;
}

partial class Tile : MonoBehaviour
{
    private bool _is_empty;
    public bool isEmpty()
    {
        return _is_empty;
    }
    public Monster getMonster()
    {
        if (_is_empty == true) return null;
        return _Monster;
    }
    public void setMonster(Monster inputMonster)
    {
        _is_empty = false;
        _Monster = inputMonster;
    }
    public void deleteMonster()
    {
        _is_empty = true;
        _Monster.delete();
    }
}

partial class Tile : MonoBehaviour
{
	// Use this for initialization
	void Start () {
        _is_empty = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
