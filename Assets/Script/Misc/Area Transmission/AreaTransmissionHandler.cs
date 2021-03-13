using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransmissionHandler
{
    private static AreaTransmissionHandler instance = new AreaTransmissionHandler();

    public static  AreaTransmissionHandler Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new AreaTransmissionHandler();
            }
            return instance;
        }
    }

    public delegate void PerformAreaTransmissionAction(SubAreaHandler subAreaHandler);

    public event PerformAreaTransmissionAction performAreaTransmissionHandler;

    public void OnPerformAreaTransmission(SubAreaHandler subAreaHandler)
    {
        performAreaTransmissionHandler?.Invoke(subAreaHandler);
    }

}
