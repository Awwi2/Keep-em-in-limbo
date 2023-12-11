using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
}

public class NonVisualMazeGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] float roomWidth;
    [SerializeField] float roomHeight;

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

        Debug.Log(D);
        List<Node> nodes = GenerateMaze(mazeSize);
        for (int x = 0; x < mazeSize.x * mazeSize.y; x++)
        {
            Debug.Log(nodes[x].up);
            Debug.Log(nodes[x].down);
            Debug.Log(nodes[x].left);
            Debug.Log(nodes[x].right);
        }
        BuildMaze(nodes);
    }

    List<Node> GenerateMaze(Vector2Int size)
    {
        List<Node> nodes = new List<Node>();

        //create Nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                nodes.Add(new Node()) ;
            }
        }
        List<Node> currentPath = new List<Node>();
        List<Node> completedNodes = new List<Node>();

        currentPath.Add(nodes[Random.Range(0,nodes.Count)]);
        Debug.Log(nodes.IndexOf(currentPath[0]));

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

                for (int i = 0; i < possibleDirections.Count; i++)
                {
                    Debug.Log(possibleDirections[i]);
                }
                Debug.Log(possibleDirections);
                Debug.Log(nodes.IndexOf(currentPath[0]));

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

    void BuildMaze(List<Node> nodes)
    {
        GameObject prefab;
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
}
