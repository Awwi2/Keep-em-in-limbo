using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public int x = 0;
    public int y = 0;
}

public class NonVisualMazeGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] float roomWidth;
    [SerializeField] float roomHeight;
    [SerializeField] GameObject camTrigger;

    List<GameObject> D;
    List<GameObject> DL;
    List<GameObject> DLR;
    List<GameObject> DR;
    List<GameObject> L;
    List<GameObject> LR;
    List<GameObject> R;
    List<GameObject> U;
    List<GameObject> UD;
    List<GameObject> UDL;
    List<GameObject> UDLR;
    List<GameObject> UDR;
    List<GameObject> UL;
    List<GameObject> ULR;
    List<GameObject> UR;

    private void Start()
    {
        D = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/D"));
        DL = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/DL"));
        DLR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/DLR"));
        DR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/DR"));
        L = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/L"));
        LR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/LR"));
        R = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/R"));
        U = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/U"));
        UD = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/UD"));
        UDL = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/UDL"));
        UDLR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/UDLR"));
        UDR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/UDR"));
        UL = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/UL"));
        ULR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/ULR"));
        UR = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms/UR"));

        List<Node> nodes = GenerateMaze(mazeSize);
        BuildMazeFlo(nodes);
    }

    List<Node> GenerateMaze(Vector2Int size)
    {
        List<Node> nodes = new List<Node>();

        //create Nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Node newNode = new Node();
                newNode.x = x;
                newNode.y = y;
                nodes.Add(newNode);
            }
        }
        List<Node> currentPath = new List<Node>();
        List<Node> completedNodes = new List<Node>();

        currentPath.Add(nodes[Random.Range(0,nodes.Count)]);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                Node chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.right = true;
                        currentPath[currentPath.Count - 1].left = true;
                        break;
                    case 2:
                        chosenNode.left = true;
                        currentPath[currentPath.Count - 1].right = true;
                        break;
                    case 3:
                        chosenNode.down = true;
                        currentPath[currentPath.Count - 1].up = true;
                        break;
                    case 4:
                        chosenNode.up = true;
                        currentPath[currentPath.Count - 1].down = true;
                        break;
                }

                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        return nodes;
    }

    /*
    void BuildMaze(List<Node> nodes)
    {
        GameObject prefab;
        //sort nodes
        List<Node> sortedNodes = new List<Node>();
        List<Node> tempYCollection = new List<Node>();
        for (int y = 0; y < nodes.Count; y++)
        {
            foreach (Node N in nodes)
            {
                if (N.y == y)
                {
                    tempYCollection.Add(N);
                    tempYCollection.Sort((a, b) => a.x.CompareTo(b.x)); //Fancy
                    foreach (Node N2 in tempYCollection)
                    {
                        sortedNodes.Add(N2);
                    }
                }
            }
            tempYCollection.Clear();
        }
        nodes = sortedNodes;
        for (int x = 0; x < mazeSize.x; x++)
        {
            for (int y = 0; y < mazeSize.y; y++)
            {
                if(nodes[x + (y * mazeSize.x)].up)
                {
                    if (nodes[x + (y * mazeSize.x)].down)
                    {
                        if (nodes[x + (y * mazeSize.x)].left)
                        {
                            if (nodes[x + (y * mazeSize.x)].right)
                            {
                                prefab = UDLR[Random.Range(0, UDLR.Count - 1)];
                            }
                            else
                            {
                                prefab = UDL[Random.Range(0, UDL.Count - 1)];
                            }
                        }
                        else if (nodes[x + (y * mazeSize.x)].right)
                        {
                            prefab = UDR[Random.Range(0, UDR.Count - 1)];
                        }
                        else
                        {
                            prefab = UD[Random.Range(0, UD.Count - 1)];
                        }
                    }
                    else if (nodes[x + (y * mazeSize.x)].left)
                    {
                        if (nodes[x + (y * mazeSize.x)].right)
                        {
                            prefab = ULR[Random.Range(0, ULR.Count - 1)];
                        }
                        else
                        {
                            prefab = UL[Random.Range(0, UL.Count - 1)];
                        }
                    }
                    else if (nodes[x + (y * mazeSize.x)].right)
                    {
                        prefab = UR[Random.Range(0, UR.Count - 1)];
                    }
                    else
                    {
                        prefab = U[Random.Range(0,U.Count - 1)];
                    }
                } 
                else if (nodes[x + (y * mazeSize.x)].down)
                {
                    if (nodes[x + (y * mazeSize.x)].left)
                    {
                        if (nodes[x + (y * mazeSize.x)].right)
                        {
                            prefab = DLR[Random.Range(0, DLR.Count - 1)];
                        }
                        else
                        {
                            prefab = DL[Random.Range(0, DL.Count - 1)];
                        }
                    }
                    else if (nodes[x + (y * mazeSize.x)].right)
                    {
                        prefab = DR[Random.Range(0, DR.Count - 1)];
                    }
                    else
                    {
                        prefab = D[Random.Range(0, D.Count - 1)];
                    }
                }
                else if (nodes[x + (y * mazeSize.x)].left)
                {
                    if (nodes[x + (y * mazeSize.x)].right)
                    {
                        prefab = LR[Random.Range(0, LR.Count - 1)];
                    }
                    else
                    {
                        prefab = L[Random.Range(0, L.Count - 1)];
                    }
                }
                else if (nodes[x + (y * mazeSize.x)].right)
                {
                    prefab = R[Random.Range(0, R.Count - 1)];
                } 
                else
                {
                    Debug.LogError("Say What?");
                    prefab = UDLR[Random.Range(0, UDLR.Count - 1)];
                }

                Vector3 nodePos = new Vector3(x * roomWidth, y * -roomHeight, 0);
                Instantiate(prefab, nodePos, Quaternion.identity, transform);
            }
        }
    }
    */

    void BuildMazeFlo(List<Node> n)
    {

        GameObject prefab = UDLR[0];
        string udlr = "";
        foreach (Node N in n)
        {
            if (N.up) {udlr = "U"; }
            if (N.down) {udlr += "D"; }
            if (N.left) {udlr += "L"; }
            if (N.right) {udlr += "R"; }
            
            if (udlr == "U") { prefab = U[Random.Range(0, U.Count - 1)]; }
            else if (udlr == "D") { prefab = D[Random.Range(0, D.Count - 1)]; }
            else if (udlr == "L") { prefab = L[Random.Range(0, L.Count - 1)]; }
            else if (udlr == "R") { prefab = R[Random.Range(0, R.Count - 1)]; }
            else if (udlr == "UD") { prefab = UD[Random.Range(0, UD.Count - 1)]; }
            else if (udlr == "UL") { prefab = UL[Random.Range(0, UL.Count - 1)]; }
            else if (udlr == "UR") { prefab = UR[Random.Range(0, UR.Count - 1)]; }
            else if (udlr == "DL") { prefab = DL[Random.Range(0, DL.Count - 1)]; }
            else if (udlr == "DR") { prefab = DR[Random.Range(0, DR.Count - 1)]; }
            else if (udlr == "LR") { prefab = LR[Random.Range(0, LR.Count - 1)]; }
            else if (udlr == "UDL") { prefab = UDL[Random.Range(0, UDL.Count - 1)]; }
            else if (udlr == "UDR") { prefab = UDR[Random.Range(0, UDR.Count - 1)]; }
            else if (udlr == "ULR") { prefab = ULR[Random.Range(0, ULR.Count - 1)]; }
            else if (udlr == "DLR") { prefab = DLR[Random.Range(0, DLR.Count - 1)]; }
            else if (udlr == "UDLR") { prefab = UDLR[Random.Range(0, UDLR.Count - 1)]; }
            udlr = "";

            Vector3 nodePos = new Vector3(N.x * (-roomWidth + 0.2f), N.y * (roomHeight - 0.2f), 0);
            Instantiate(prefab, nodePos, Quaternion.identity, transform);
            Instantiate(camTrigger, nodePos, Quaternion.identity, transform);
        }
    }
}
