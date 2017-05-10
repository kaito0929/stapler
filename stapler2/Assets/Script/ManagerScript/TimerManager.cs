using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    public Timer timer;

	void Start ()
    {
        timer.StartTimer(30);
	}

	void Update ()
    {
	
	}

    
}
