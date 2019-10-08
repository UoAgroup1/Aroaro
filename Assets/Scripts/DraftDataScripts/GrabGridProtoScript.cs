using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGridProtoScript : MonoBehaviour
{
    public int gridCubeSize;

    public int gridWidth;
    public int gridHeight;
    public int gridDepth;

    public int gridDensityMultiplier;
    int totalGridPoints;

    public GameObject gridPointPrefab;
    public GameObject gridPointCornerPrefab;
    public GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        int x, y, z;
        for (int i = 0; i < gridCubeSize; i++){
            x = i * 3;
            for (int j = 0; j < gridCubeSize; j++)
            {
                y = j * 3;
                for (int k = 0; k <gridCubeSize; k++)
                {
                    GameObject holdNewObj;
                    z = k * 3;
                    if ((i == 0 || i == (gridCubeSize - 1)) && (j == 0 || j == (gridCubeSize - 1)) && (k == 0 || k == (gridCubeSize - 1)))
                    {
                        holdNewObj = (GameObject)Instantiate(gridPointCornerPrefab);
                    }
                    else
                    {
                        holdNewObj = (GameObject)Instantiate(gridPointPrefab);
                    }
                    
                    holdNewObj.transform.parent = parentObject.transform;
                    holdNewObj.transform.localPosition = new Vector3(x,y,z);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
