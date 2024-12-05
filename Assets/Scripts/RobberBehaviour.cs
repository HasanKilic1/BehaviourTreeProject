using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;

    public GameObject diamong;
    public GameObject van;
    public GameObject frontDoor;
    public GameObject backDoor;
    NavMeshAgent agent;

    public enum ActionState { IDLE , WORKING };
    ActionState state = ActionState.IDLE;
    Node.Status treeStatus = Node.Status.RUNNING;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van", GoToVan);
        Leaf goToFrontDoor = new Leaf("Go To Frontdoor", GoToFrontDoor);
        Leaf goToBackDoor = new Leaf("Go To Backdoor", GoToBackDoor);

        Selector openDoor = new Selector("Open Door");

        openDoor.AddChild(goToFrontDoor);
        openDoor.AddChild(goToBackDoor);
        
        steal.AddChild(openDoor);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);
    }

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = tree.Process();
        }
    }

    public Node.Status GoToDiamond()
    {
        return GoToLocation(diamong.transform.position);
    }

    public Node.Status GoToVan()
    {
        return GoToLocation(van.transform.position);
    }

    public Node.Status GoToFrontDoor()
    {
        return GoToLocation(frontDoor.transform.position);
    }
    public Node.Status GoToBackDoor()
    {
        return GoToLocation(backDoor.transform.position);
    }
    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, transform.position);
        if (state.Equals(ActionState.IDLE))
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition , destination) >= 4f && !agent.pathPending) // could not reach the destination
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarget < 2f)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        Debug.Log("Distance to target loc + " + distanceToTarget);
        return Node.Status.RUNNING;
    }
}
