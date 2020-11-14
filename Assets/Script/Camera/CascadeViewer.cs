using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeViewer : MonoBehaviour
{
    public GameObject[] cascadeLayers;

    protected SortedList<int, GameObject> cascadeLayerList;

    
    void Start()
    {
        cascadeLayerList = new SortedList<int, GameObject>();
        
    }

    public class CascadeLayer
    {
        private GameObject layerGO;
        private int layerOrder;

        public CascadeLayer(GameObject layerGO, int layerOrder)
        {

        }

        
    }
}
