using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour ,IHit
{
    public bool raceStarted = false; // ���ְ� ���ۉ���� Ȯ���ϴ� ����
    protected float aiSpeed = 10.0f; // �ӵ�
    float aiTurnSpeed = 2.0f; // ������ �ٲܶ��� �ӵ� �󸶳� ������ �ڳʸ� �� �� �ִ��� ǥ��
    float resetAISpeed = 60.0f; 
    float resetAITurnSpeed = 1000.0f;
    public GameObject waypointController; // ��������Ʈ �׷��� �д� ���ӿ�����Ʈ waypoints�� ���������
    List<Transform> waypoints; //waypointController���� waypoint������ �޴´�
    int currentWaypoint = 0; //���� ���� ����
    float currentSpeed; // ���� �ӵ�
    Vector3 currentWaypointPosition; // ���󰡾��� Vector3�� ������

    protected Rigidbody rigid;

    
    float wayDistanse = 250.0f; // �������� ���� ��ġ��ŭ ��������� �������� ����



    float sensorLength = 8.0f; //transform.forward ���� ���� ��� ������ ray�� ����
    float frontSensorSideDist = 3.0f; // ������ ���� transform.forward ���� ���� ������ ray�� ����� ��������ų� �־���
    float frontSensorAngle = 50.0f; // ������ ���� ����̵� ���� ������ ����
    float sidewaySensorLength = 9.0f; // ���̵�ray ����


    private int flag = 0; //ȸ������ ������ �����ϴ� ����
    float avoidSpeed = 200000.0f; // ���ع� ���ϴ� ���ǵ�

    float maxSpeed = 70.7f; // �ְ� ���ǵ�

    private Transform frontTarget = null; // �տ� ������ �ִ� �� Ȯ���� ����

    float outSightAngle = 100.0f; // �տ� �ִ� ������ ������� ������ �������� Ÿ���� �������� Ȯ���ϴ� ����
    float outFrontDistance = 50.0f; // Ÿ���ϰ� ������ŭ �������� Ÿ���� �����Ѵ�.

    public ParticleSystem footParticle; // �߹ٴ� ��ƼŬ
    float delta = 1; // �߹ٴ� ��ƼŬ ��������
    float gap = 0.5f; // �߹ٴ� ��ƼŬ���� ����
    int dir = 1; // �߹ٴ� ��ƼŬ ����
    Vector3 lastEmit; // ������ �߹ٴ� ��ƼŬ�� ���� ��ġ


    protected Animator animator;
    protected GameObject dustTail;

    WaitForSeconds stateSpinSecond = new WaitForSeconds(2.0f);

    bool stateAttack = false;

    protected virtual bool StateAttack
    {
        get { return stateAttack; }
        set { stateAttack = value; }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        dustTail = transform.Find("DustTail").gameObject;

        lastEmit = transform.position; // �߹ٴ� ��ƼŬ�� ù��ġ
        GetWayPoints(); // ��������Ʈ ����
        aiSpeed = resetAISpeed; // ���ǵ� ����
        aiTurnSpeed = resetAITurnSpeed; // ���ǵ� ����
        
    }




    protected virtual void FixedUpdate()
    {
        //aiSpeed = Random.Range(resetAISpeed, maxSpeed); // ���ǵ�� ��� �ּ�~�ִ���̷� ���ŵ�
        if (raceStarted)
        {
            //���̽� ���۽� ���Ǵ� �Լ���
            MoveTowardWayPoints(); // ������ �Լ�
            Sensor(); // �ֺ� ��ֹ�Ȯ�� �Լ�
        }

        FootPrint(); //���ڱ� �Լ�
    }

    /// <summary>
    /// ���ڱ��� ����ִ� �Լ�
    /// </summary>
    protected virtual void FootPrint()
    {
        //delta�Ÿ� ��ŭ �̵������� ���ڱ��� ��� ��ġ�� �������ش�.
        if (Vector3.Distance(lastEmit, transform.position) > delta)
        {
            Vector3 pos = transform.position + (transform.right * gap * dir);
            pos.y += 0.5f;
            dir *= -1;
            ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
            ep.position = pos;
            ep.rotation = transform.rotation.eulerAngles.y;
            footParticle.Emit(ep, 1);
            lastEmit = transform.position;


        }
    }

    
    /// <summary>
    /// waypointController �׷�ȿ� �ִ� �� ������ ��ġ ������ �ҷ��´�
    /// </summary>
    private void GetWayPoints()
    {
        Transform[] potentialWaypoints = waypointController.GetComponentsInChildren<Transform>();
        waypoints = new List<Transform>();
        for (int i = 0; i < potentialWaypoints.Length; i++)
        {
            if (potentialWaypoints[i] != waypointController.transform)
            {
                waypoints.Add(potentialWaypoints[i]);
            }
        }

    }

    /// <summary>
    /// ���� �������� �̵��ϴ� ����
    /// </summary>
    private void MoveTowardWayPoints()
    {
        //���� ��������Ʈ�� Vector3Ÿ�� ��ġ ������ �д´�.
        float currentWayPointX = waypoints[currentWaypoint].position.x;
        float currentWayPointY = transform.position.y;
        float currentWayPointZ = waypoints[currentWaypoint].position.z;

        //InverseTransformPoint
        //���� position(�������)�� ȣ������ ������ǥ�� �������� �ٲ��� z��(�÷��̾��� �� ����)�� �̷�� ������ ��´�
        //Ŭ���� �̵��������� �̵��ϴ� ���ÿ� �ٶ󺸴� ���� ���� �̵� ���������� �ٲٴµ� ó��
        //��������Ʈ�� ���� ��ġ ������ �Ÿ��� ��� + ���� ��ǥ���� ��� ��ǥ�� ��ȯ
        Vector3 relativeWaypointPosition = transform.InverseTransformPoint(new Vector3(currentWayPointX, currentWayPointY, currentWayPointZ));

        currentWaypointPosition = new Vector3(currentWayPointX, currentWayPointY, currentWayPointZ);

        //�ϸ��� ��� �׸���(������ ������ �̵���)�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
        Quaternion toRotation = Quaternion.LookRotation(currentWaypointPosition - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, aiTurnSpeed);
        //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
        //���� ��ǥ �������� ���� ���Ѵ�.
        rigid.AddRelativeForce(0, 0, aiSpeed);

        //��������� ��������Ʈ ���� �� ��� ��������Ʈ�� ������ ó������Ʈ�� ���� 
        if (relativeWaypointPosition.sqrMagnitude < wayDistanse)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }

        //���� �ڵ��� �������� ������ �ڵ�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
        currentSpeed = Mathf.Abs(transform.InverseTransformDirection(rigid.velocity).z);

        float maxAgularDrag = 20.5f;
        float currentAngularDrag = 1.0f;
        float aDragLerpTime = currentSpeed * 0.1f;
        float maxDrag = 2.0f;
        float currentDrag = 3.5f;
        float dragLerpTime = currentSpeed * 0.1f;
        float myAngularDrag = Mathf.Lerp(currentAngularDrag, maxAgularDrag, aDragLerpTime);

        float myDrag = Mathf.Lerp(currentDrag, maxDrag, dragLerpTime);
        rigid.angularDrag = myAngularDrag;
        rigid.drag = myDrag;
        //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    }

    /// <summary>
    /// ���浹 ���� �� ���ΰ��� �浹����
    /// </summary>
    protected virtual void Sensor()
    {
        flag = 0; // 0�̸� ȸ���� �ʿ����
        float avoidSenstivity = 0; // �� ������ ���� ���� ū�� ȸ������ �۰� ȸ������ ����


        Vector3 pos; // �ڽ���ġ �޾ƿ� ����
        RaycastHit hit; // ��Ʈ�� ���̹޾ƿ� ����
        Quaternion rightAngle = Quaternion.Euler(0.5f * frontSensorAngle * transform.up); // ������ ���� ����
        Quaternion leftAngle = Quaternion.Euler(0.5f * -frontSensorAngle * transform.up); // ���� ���� ����


        pos = transform.position;

        // ��� ray

        pos += transform.forward + transform.up;
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.CompareTag("Animal"))
            {

                frontTarget = hit.transform; //�տ� Ÿ���� ������ �����Ѵ�.

            }
            Debug.DrawLine(pos, hit.point, Color.red);
        }

        // ��� ������ ���� ray
        pos = transform.position;
        pos += transform.forward + transform.right * frontSensorSideDist + transform.up;

        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                flag++;
                avoidSenstivity -= 600.5f; //���� �����Ǹ� ȸ�ǻ��·� �����.

            }

            Debug.DrawLine(pos, hit.point, Color.red);

        }

        //��� ���� ���� ray
        pos = transform.position;
        pos += transform.forward + -transform.right * frontSensorSideDist + transform.up;

        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                flag++;
                avoidSenstivity += 600.5f; //���� �����Ǹ� ȸ�ǻ��·� �����.

            }


            Debug.DrawLine(pos, hit.point, Color.red);
        }



        // �翷
        pos = transform.position + transform.up;
        if (Physics.Raycast(pos, transform.right, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                if (hit.distance < 2.5f)
                {
                    flag++;
                    avoidSenstivity -= 600.5f; //���� ���� �Ÿ��� ���� ȸ�ǻ��·� �����.
                }
            }

        }

        if (Physics.Raycast(pos, -transform.right, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                if (hit.distance < 2.5f)
                {
                    flag++;
                    avoidSenstivity += 600.5f; //���� ���� �Ÿ��� ���� ȸ�ǻ��·� �����.
                }
            }
        }

        //�� ���̵�
        if (Physics.Raycast(pos, rightAngle * transform.forward, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Animal"))
            {

                frontTarget = hit.transform; //Ÿ���� ������ �����Ѵ�.

            }
            Debug.DrawLine(pos, hit.point, Color.red);
        }

        if (Physics.Raycast(pos, leftAngle * transform.forward, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Animal"))
            {

                frontTarget = hit.transform; //Ÿ���� ������ �����Ѵ�.

            }
            Debug.DrawLine(pos, hit.point, Color.red);
        }


        //�տ� Ÿ���� ������ �ؾ��� �ൿ
        if (frontTarget != null)
        {
            //Ÿ�ٰ� ���� �������
            float angle = Vector3.Angle(transform.forward, frontTarget.position - transform.position);
            //Ÿ�ٰ� ���� �Ÿ����
            float distance = Vector3.SqrMagnitude(transform.position - frontTarget.position);
            float leftDistance = 999.0f;
            float rightDistance = 999.0f;
            float leftSideDistance = 999.0f;
            float rightSideDistance = 999.0f;

            //Ư�������� �������� Ÿ��null�� ����
            if (angle > outSightAngle || distance > outFrontDistance)
            {
                frontTarget = null;
                //Debug.Log("Ÿ���� ���ƽ��ϴ�.");
            }
            else // �ƴҰ��
            {
                //������ray
                if (Physics.Raycast(pos, transform.right, out hit, sidewaySensorLength))
                {
                    rightDistance = hit.distance; //�Ÿ� ����
                    Debug.DrawLine(pos, hit.point, Color.red);
                }

                //����ray
                if (Physics.Raycast(pos, -transform.right, out hit, sidewaySensorLength))
                {
                    leftDistance = hit.distance; //�Ÿ� ����
                    Debug.DrawLine(pos, hit.point, Color.red);
                }

                //�� ������ ���̵� ray
                if (Physics.Raycast(pos, rightAngle * transform.forward, out hit, sidewaySensorLength))
                {
                    rightSideDistance = hit.distance; //�Ÿ� ����
                    Debug.DrawLine(pos, hit.point, Color.red);
                }
                //�� ���� ���̵� ray
                if (Physics.Raycast(pos, leftAngle * transform.forward, out hit, sidewaySensorLength))
                {
                    leftSideDistance = hit.distance; //�Ÿ� ����
                    Debug.DrawLine(pos, hit.point, Color.red);
                }

                //���ʻ��̵�+���� ray�� �����ʻ��̵�+������ ray�� ���Ͽ� �̵��Ұ��� ���Ѵ�.
                if (leftDistance + leftSideDistance >= rightDistance + rightSideDistance)
                {
                    flag++;
                    avoidSenstivity -= 2.5f;
                    //Debug.Log("�����ʺ��� �����");
                }
                else if (leftDistance + leftSideDistance < rightDistance + rightSideDistance)
                {
                    flag++;
                    avoidSenstivity += 2.5f;
                    //Debug.Log("���ʺ��� �����");
                }


            }


        }

        //flog�� 0�� �ƴϸ� AvoidSteer�Լ��� �����ؼ� ��ֹ��� ȸ���Ѵ�.
        if (flag != 0)
        {

            AvoidSteer(avoidSenstivity);
           
        }




    }

    /// <summary>
    /// ��ֹ��� ȸ���ϴ� �Լ�
    /// </summary>
    /// <param name="senstivity">���� ���� �������� ���������� ��������.</param>
    private void AvoidSteer(float senstivity)
    {
        //wheelFL.steerAngle = avoidSpeed* senstivity; 
        rigid.AddTorque(transform.up * senstivity * avoidSpeed);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            //Debug.Log("�ε���");
            //�������� �ε������� ���� �з�������
            Vector3 dir = transform.position - collision.transform.position;
            rigid.AddForce(dir.normalized * 150.0f);

        }


    }

    public void TakeHit(float stateDamage,HitType hitType = HitType.None)
    {
        if(hitType==HitType.Spin && !StateAttack)
        {
            StateAttack = true;
            aiSpeed -= stateDamage;
            animator.SetBool("stateSpin", StateAttack);
            StartCoroutine(stateSpin(stateDamage));
        }
    }

    IEnumerator stateSpin(float stateDamage)
    {
        yield return stateSpinSecond;
        StateAttack = false;
        aiSpeed += stateDamage;
        animator.SetBool("stateSpin", StateAttack);
    }
}
