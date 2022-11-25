using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour ,IHit
{
    public bool raceStarted = false; // 경주가 시작됬는지 확인하는 변수
    protected float aiSpeed = 10.0f; // 속도
    float aiTurnSpeed = 2.0f; // 방향을 바꿀때의 속도 얼마나 빠르게 코너를 돌 수 있는지 표현
    float resetAISpeed = 60.0f; 
    float resetAITurnSpeed = 1000.0f;
    public GameObject waypointController; // 웨이포인트 그룹을 읽는 게임오브젝트 waypoints에 정보줘야함
    List<Transform> waypoints; //waypointController한테 waypoint정보를 받는다
    int currentWaypoint = 0; //현재 따라갈 지점
    float currentSpeed; // 현재 속도
    Vector3 currentWaypointPosition; // 따라가야할 Vector3의 포지션

    protected Rigidbody rigid;

    
    float wayDistanse = 250.0f; // 목적지에 변수 수치만큼 가까워지면 목적지를 변경



    float sensorLength = 8.0f; //transform.forward 방향 왼쪽 가운데 오른쪽 ray의 길이
    float frontSensorSideDist = 3.0f; // 설정에 따라 transform.forward 방향 왼쪽 오른쪽 ray가 가운데에 가까워지거나 멀어짐
    float frontSensorAngle = 50.0f; // 설정에 따라 양사이드 레이 각도가 변함
    float sidewaySensorLength = 9.0f; // 사이드ray 길이


    private int flag = 0; //회피할지 안할지 결정하는 변수
    float avoidSpeed = 200000.0f; // 방해물 피하는 스피드

    float maxSpeed = 70.7f; // 최고 스피드

    private Transform frontTarget = null; // 앞에 동물이 있는 지 확인할 변수

    float outSightAngle = 100.0f; // 앞에 있던 동물이 어느정도 각도를 지나가면 타겟을 해제할지 확인하는 변수
    float outFrontDistance = 50.0f; // 타겟하고 변수만큼 떨어지면 타겟을 해제한다.

    public ParticleSystem footParticle; // 발바닥 파티클
    float delta = 1; // 발바닥 파티클 생성간격
    float gap = 0.5f; // 발바닥 파티클간의 간격
    int dir = 1; // 발바닥 파티클 방향
    Vector3 lastEmit; // 마지막 발바닥 파티클이 생긴 위치


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

        lastEmit = transform.position; // 발바닥 파티클의 첫위치
        GetWayPoints(); // 웨이포인트 갱신
        aiSpeed = resetAISpeed; // 스피드 갱신
        aiTurnSpeed = resetAITurnSpeed; // 스피드 갱신
        
    }




    protected virtual void FixedUpdate()
    {
        //aiSpeed = Random.Range(resetAISpeed, maxSpeed); // 스피드는 계속 최소~최대사이로 갱신됨
        if (raceStarted)
        {
            //레이스 시작시 사용되는 함수들
            MoveTowardWayPoints(); // 움직임 함수
            Sensor(); // 주변 장애물확인 함수
        }

        FootPrint(); //발자국 함수
    }

    /// <summary>
    /// 발자국을 찍어주는 함수
    /// </summary>
    protected virtual void FootPrint()
    {
        //delta거리 만큼 이동했을때 발자국을 찍고 위치를 갱신해준다.
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
    /// waypointController 그룹안에 있는 각 지점의 위치 정보를 불러온다
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
    /// 다음 지점으로 이동하는 로직
    /// </summary>
    private void MoveTowardWayPoints()
    {
        //현재 웨이포인트의 Vector3타입 위치 정보를 읽는다.
        float currentWayPointX = waypoints[currentWaypoint].position.x;
        float currentWayPointY = transform.position.y;
        float currentWayPointZ = waypoints[currentWaypoint].position.z;

        //InverseTransformPoint
        //인자 position(월드기준)을 호출자의 지역좌표계 기준으로 바꾼후 z축(플레이어의 앞 방향)과 이루는 각도를 얻는다
        //클릭한 이동지점으로 이동하는 동시에 바라보는 방향 또한 이동 지점쪽으로 바꾸는데 처리
        //웨이포인트와 현재 위치 사이의 거리를 계산 + 전역 좌표에서 상대 좌표로 전환
        Vector3 relativeWaypointPosition = transform.InverseTransformPoint(new Vector3(currentWayPointX, currentWayPointY, currentWayPointZ));

        currentWaypointPosition = new Vector3(currentWayPointX, currentWayPointY, currentWayPointZ);

        //완만한 곡선을 그리기(없으면 각져서 이동함)ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        Quaternion toRotation = Quaternion.LookRotation(currentWaypointPosition - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, aiTurnSpeed);
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        //로컬 좌표 기준으로 힘을 가한다.
        rigid.AddRelativeForce(0, 0, aiSpeed);

        //가까워지면 웨이포인트 변경 밑 모든 웨이포인트를 지나면 처음포인트로 변경 
        if (relativeWaypointPosition.sqrMagnitude < wayDistanse)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }

        //대충 자동차 움직임을 구현한 코드ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
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
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    }

    /// <summary>
    /// 벽충돌 방지 및 서로간에 충돌방지
    /// </summary>
    protected virtual void Sensor()
    {
        flag = 0; // 0이면 회피할 필요없음
        float avoidSenstivity = 0; // 이 변수의 값에 따라 큰게 회피할지 작게 회피할지 경정


        Vector3 pos; // 자신위치 받아올 변수
        RaycastHit hit; // 히트된 레이받아올 변수
        Quaternion rightAngle = Quaternion.Euler(0.5f * frontSensorAngle * transform.up); // 오른쪽 레이 각도
        Quaternion leftAngle = Quaternion.Euler(0.5f * -frontSensorAngle * transform.up); // 왼쪽 레이 각도


        pos = transform.position;

        // 가운데 ray

        pos += transform.forward + transform.up;
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.CompareTag("Animal"))
            {

                frontTarget = hit.transform; //앞에 타겟이 있으면 갱신한다.

            }
            Debug.DrawLine(pos, hit.point, Color.red);
        }

        // 가운데 오른쪽 직진 ray
        pos = transform.position;
        pos += transform.forward + transform.right * frontSensorSideDist + transform.up;

        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                flag++;
                avoidSenstivity -= 600.5f; //벽이 감지되면 회피상태로 만든다.

            }

            Debug.DrawLine(pos, hit.point, Color.red);

        }

        //가운데 왼쪽 직진 ray
        pos = transform.position;
        pos += transform.forward + -transform.right * frontSensorSideDist + transform.up;

        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                flag++;
                avoidSenstivity += 600.5f; //벽이 감지되면 회피상태로 만든다.

            }


            Debug.DrawLine(pos, hit.point, Color.red);
        }



        // 양옆
        pos = transform.position + transform.up;
        if (Physics.Raycast(pos, transform.right, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                if (hit.distance < 2.5f)
                {
                    flag++;
                    avoidSenstivity -= 600.5f; //벽이 일정 거리로 오면 회피상태로 만든다.
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
                    avoidSenstivity += 600.5f; //벽이 일정 거리로 오면 회피상태로 만든다.
                }
            }
        }

        //앞 사이드
        if (Physics.Raycast(pos, rightAngle * transform.forward, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Animal"))
            {

                frontTarget = hit.transform; //타겟이 있으면 갱신한다.

            }
            Debug.DrawLine(pos, hit.point, Color.red);
        }

        if (Physics.Raycast(pos, leftAngle * transform.forward, out hit, sidewaySensorLength))
        {
            if (hit.transform.CompareTag("Animal"))
            {

                frontTarget = hit.transform; //타겟이 있으면 갱신한다.

            }
            Debug.DrawLine(pos, hit.point, Color.red);
        }


        //앞에 타겟이 있을때 해야할 행동
        if (frontTarget != null)
        {
            //타겟과 나의 각도계산
            float angle = Vector3.Angle(transform.forward, frontTarget.position - transform.position);
            //타겟과 나의 거리계산
            float distance = Vector3.SqrMagnitude(transform.position - frontTarget.position);
            float leftDistance = 999.0f;
            float rightDistance = 999.0f;
            float leftSideDistance = 999.0f;
            float rightSideDistance = 999.0f;

            //특정범위를 지나가면 타겟null로 변경
            if (angle > outSightAngle || distance > outFrontDistance)
            {
                frontTarget = null;
                //Debug.Log("타겟을 놓쳤습니다.");
            }
            else // 아닐경우
            {
                //오른쪽ray
                if (Physics.Raycast(pos, transform.right, out hit, sidewaySensorLength))
                {
                    rightDistance = hit.distance; //거리 갱신
                    Debug.DrawLine(pos, hit.point, Color.red);
                }

                //왼쪽ray
                if (Physics.Raycast(pos, -transform.right, out hit, sidewaySensorLength))
                {
                    leftDistance = hit.distance; //거리 갱신
                    Debug.DrawLine(pos, hit.point, Color.red);
                }

                //앞 오른쪽 사이드 ray
                if (Physics.Raycast(pos, rightAngle * transform.forward, out hit, sidewaySensorLength))
                {
                    rightSideDistance = hit.distance; //거리 갱신
                    Debug.DrawLine(pos, hit.point, Color.red);
                }
                //앞 왼쪽 사이드 ray
                if (Physics.Raycast(pos, leftAngle * transform.forward, out hit, sidewaySensorLength))
                {
                    leftSideDistance = hit.distance; //거리 갱신
                    Debug.DrawLine(pos, hit.point, Color.red);
                }

                //왼쪽사이드+왼쪽 ray와 오른쪽사이드+오른쪽 ray를 비교하여 이동할곳을 정한다.
                if (leftDistance + leftSideDistance >= rightDistance + rightSideDistance)
                {
                    flag++;
                    avoidSenstivity -= 2.5f;
                    //Debug.Log("오른쪽벽이 가까움");
                }
                else if (leftDistance + leftSideDistance < rightDistance + rightSideDistance)
                {
                    flag++;
                    avoidSenstivity += 2.5f;
                    //Debug.Log("왼쪽벽이 가까움");
                }


            }


        }

        //flog가 0이 아니면 AvoidSteer함수를 실행해서 장애물을 회피한다.
        if (flag != 0)
        {

            AvoidSteer(avoidSenstivity);
           
        }




    }

    /// <summary>
    /// 장애물을 회피하는 함수
    /// </summary>
    /// <param name="senstivity">값에 따라 왼쪽인지 오른쪽인지 정해진다.</param>
    private void AvoidSteer(float senstivity)
    {
        //wheelFL.steerAngle = avoidSpeed* senstivity; 
        rigid.AddTorque(transform.up * senstivity * avoidSpeed);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            //Debug.Log("부딪힘");
            //동물끼리 부딪혔을때 조금 밀려나게함
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
