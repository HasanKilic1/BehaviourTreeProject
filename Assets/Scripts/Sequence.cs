using UnityEngine;

public class Sequence : Node
{
    public Sequence(string name)
    {
        this.name = name;
    }

    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();

        if(childStatus == Status.RUNNING) return Status.RUNNING;
        
        if(childStatus == Status.FAILURE) return childStatus;

        currentChild++;
        if(currentChild >= children.Count)
        {
            currentChild = 0;
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }
}