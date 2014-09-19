using System.Collections;
using UnityEngine;

public enum eEnum
{
    One,
    Two,
}

partial class TestClass : MonoBehaviour
{
    private static TestClass instance = null;

    public static TestClass Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(TestClass)) as TestClass;
            return instance;
        }
    }
}

partial class TestClass : MonoBehaviour
{
    public int myPublicValue;
    private int _myPrivateValue;

    public int myPrivateValue
    {
        get
        {
            return this._myPrivateValue;
        }
        set
        {
            _myPrivateValue = value;
        }
    }
}

partial class TestClass : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        int my_local_value;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}

partial class TestClass : MonoBehaviour
{
    public void MyCustomFunction()
    {
        for (int i = 0; i < 5; i++)
        {
        }
    }

    public static void MyStaticFunction()
    {
        //이렇게 쓰지 말것!
        //_myPrivateValue ++;
    }
}