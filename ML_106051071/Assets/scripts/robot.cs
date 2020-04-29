using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class robot : Agent
{   
    [Header("速度") , Range(1, 50)]
    public float speed = 10;
    
    private Rigidbody rigRobot;
    private Rigidbody rigBall;

    private void Start()
    {
        rigRobot = GetComponent<Rigidbody>();
        rigBall = GameObject.Find("Soccer Ball").GetComponent<Rigidbody>();
    }


    public override void OnEpisodeBegin()
    {
        rigRobot.velocity = Vector3.zero;
        rigRobot.angularVelocity = Vector3.zero;
        rigBall.velocity = Vector3.zero;
        rigBall.angularVelocity = Vector3.zero;

        Vector3 posRobot = new Vector3(Random.Range(-1f, 1f), 0.1f , Random.Range(-2f, 0));
        transform.position = posRobot;

        Vector3 posBall = new Vector3(Random.Range(0.5f, 0.5f), 0.1f, Random.Range( 2f, -2f));
        rigBall.position = posBall;

        ball.complete = false;
        

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigBall.position);
        sensor.AddObservation(rigRobot.velocity.x);
        sensor.AddObservation(rigRobot.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigRobot.AddForce(control * speed);


        if(ball.complete)
        {
            SetReward(1);
            EndEpisode();

        }
        if(transform.position.y < 0 || rigBall.position.y<0)

        {
            SetReward(-1);
            EndEpisode();
        }

    }
   

}
