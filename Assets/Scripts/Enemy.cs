using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //�ϥ�UnityEngine.AI 

public class Enemy : MonoBehaviour
{
    [Header("�l�ܥؼг]�w")]
    public string targetName = "Player";       // �]�w�ؼЪ��󪺼��ҦW��
    public float minimunTraceDistance = 5.0f;  // �]�w�̵u���l�ܶZ��

    NavMeshAgent navMeshAgent;                 // �ŧiNavMeshAgent����
    GameObject targetObject = null;            // �ؼЪ����ܼ�

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
        navMeshAgent = GetComponent<NavMeshAgent>();                   // ����NavMeshAgent
    }

    void Update()
    {
        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ���A�p�G�P�ؼЪ��Z���j��̤p�Z���A�i��l�ܡA�_�h�N����
        if (distance <= minimunTraceDistance)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;
    }

    void FixedUpdate()
    {
        navMeshAgent.SetDestination(targetObject.transform.position);    // ���ۤv���ؼЪ����y�в���   
    }
}