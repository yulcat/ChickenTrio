using System.Collections;
using UnityEngine;

public partial class TileEventDispatcher : MonoBehaviour
{
    //Tk 2d Button으로 부터 호출되는 Message는 인자를 받을 수 없기 때문에
    //함수이름을 각각 달리 하여 값을 받아온다.
    public void onClickLine1()
    {
        //Debug.Log("clicked line 1");
        _ClickLine(1);
    }

    public void onClickLine2()
    {
        //Debug.Log("clicked line 2");
        _ClickLine(2);
    }

    public void onClickLine3()
    {
        //Debug.Log("clicked line 3");
        _ClickLine(3);
    }

    public void onClickLine4()
    {
        //Debug.Log("clicked line 4");
        _ClickLine(4);
    }

    public void onClickLine5()
    {
        //Debug.Log("clicked line 5");
        _ClickLine(5);
    }

    public void onClickLine6()
    {
        //Debug.Log("clicked line 6");
        _ClickLine(6);
    }

    public void onClickLine7()
    {
        //Debug.Log("clicked line 7");
        _ClickLine(7);
    }
}

public partial class TileEventDispatcher : MonoBehaviour
{
    private void _ClickLine(int lineNumber)
    {
        audio.Play();
        if (MonsterCalculator.Instance.isFinished == true)
        {
            //Debug.Log("clicked " + lineNumber);
            TileEventListener[] listeners = (TileEventListener[])FindObjectsOfType(typeof(TileEventListener));
            foreach (TileEventListener listener in listeners)
            {
                listener.gameObject.SendMessage("ClickLine", lineNumber);
            }
        }
    }
}

public partial class TileEventBinder : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}