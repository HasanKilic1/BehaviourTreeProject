using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    public BehaviourTree()
    {
        name = "Tree";
    }

    public BehaviourTree(string n)
    {
        name = n;
    }

    public override Status Process()
    {
        return children[currentChild].Process();
    }



    struct NodeLevel
    {
        public int level;
        public Node node;

        public NodeLevel(int level, Node node)
        {
            this.level = level;
            this.node = node;
        }
    }

    public void PrintTree()
    {
        string treePrintOut = "";
        Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();
        Node currentNode = this;
        nodeStack.Push(new NodeLevel(0 , currentNode));

        while(nodeStack.Count != 0)
        {
            NodeLevel nextNode = nodeStack.Pop();
            treePrintOut += new string('-' , nextNode.level) + nextNode.node.name + "\n";
            for(int i = nextNode.node.children.Count - 1; i >= 0; i--)
            {
                nodeStack.Push(new NodeLevel(nextNode.level + 1 , nextNode.node.children[i]));
            }
        }

        Debug.Log(treePrintOut);
    }
}
