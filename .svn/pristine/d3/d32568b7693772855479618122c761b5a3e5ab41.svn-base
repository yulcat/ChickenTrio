using UnityEngine;
using System.Collections;

partial class Monster : MonoBehaviour
{
    private GameObject _monsterSprite;
    private int _monsterType; //아직은 기능x
    private int _spawnOrder;
}

partial class Monster : MonoBehaviour
{
    public GameObject getMonsterSprite()
    {
        return _monsterSprite;
    }
    public void setMonsterSprite(GameObject new_monster_sprite)
    {
        _monsterSprite = new_monster_sprite;
    }
    public int getMonsterType()
    {
        return _monsterType;
    }
    public void setMonsterType(int monster_type)
    {
        _monsterType = monster_type;
    }
    public int getSpawnOrder()
    {
        return _spawnOrder;
    }
    public void setSpawnOrder(int spawn_order)
    {
        _spawnOrder = spawn_order;
    }
    public void delete()
    {
        Destroy(_monsterSprite); //????
        Destroy(this); //???
    }
}

partial class Monster : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}