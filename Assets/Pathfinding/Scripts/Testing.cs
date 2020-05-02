
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using System.IO;

public class Testing : MonoBehaviour {
    
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding pathfinding;

    private void Start() {
        pathfinding = new Pathfinding(13, 11);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
       setupgrid();
    }

    public void save()
    {
        string boole = "true";

        string path = "C:/Users/Mohab Badran/Desktop/4th-2nd semister/AZURE/azure-spatial-anchors-samples-master/A_star_pathfinding/grid555.txt";
        StreamWriter writer = new StreamWriter(path);

        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 11; j++)
            {

                bool x = pathfindingVisual.grid.GetGridObject(i, j).isWalkable;
                if (x)
                {
                    boole = "true";
                }
                else
                {
                    boole = "false";
                }
                writer.WriteLine(boole);
            }
        }
        writer.Close();
    }



    public void setupgrid()
    {
        string path = "C:/Users/Mohab Badran/Desktop/4th-2nd semister/AZURE/azure-spatial-anchors-samples-master/A_star_pathfinding/grid555.txt";
        bool x2 = true;
        StreamReader reader = new StreamReader(path);
        Grid<PathNode> gridx = pathfindingVisual.grid;
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                string x = reader.ReadLine();
                PathNode pt = new PathNode(gridx, i, j);
                if (x.Equals("true"))
                {
                    x2 = true;
                }
                if (x.Equals("false"))
                {
                    x2 = false;
                }
                pt.isWalkable = x2;
                pathfindingVisual.grid.SetGridObject(i, j, pt);
            }
        }
        reader.Close();
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }

}
