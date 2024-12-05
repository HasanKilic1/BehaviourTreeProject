using UnityEngine;

public class Selector : Node
{
    public Selector(string name)
    {
        this.name = name;
    }

    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();
        if(childStatus == Status.RUNNING) return Status.RUNNING;

        if (childStatus == Status.SUCCESS)
        {
            currentChild = 0;
            return Status.SUCCESS;
        }
        
        currentChild++;

        //all children returns failure
        if(currentChild >= children.Count) return Status.FAILURE;

        return Status.RUNNING;
    }
}

