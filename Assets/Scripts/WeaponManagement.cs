using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // �n���o�Ӥ~�౱���r��

public class WeaponManagement : MonoBehaviour
{
    [Header("�Z��")]
    public GameObject[] weaponObjects;        // �Z���M��

    int weaponNumber = 0;                     // �ثe��ܪZ�������ǽs��
    GameObject weaponInUse;                   // �ثe��ܪZ��

    private void Start()
    {
        weaponInUse = weaponObjects[0];    // �C���@�}�l�]�w�Z������0�ӪZ��
    }

    private void Update()
    {
        MyInput();
    }

    // ��k�G�������a�ާ@���A
    private void MyInput()
    {
        // �P�_�G���S�����U����H
        if (Input.GetMouseButtonDown(0) == true)
        {
            // �p�G�٦��l�u�A�åB�S�����b���ˤl�u�A�N�i�H�g��
            weaponInUse.GetComponent<Weapon>().Attack();
        }

        // �P�_�G1.�����UR��B2.�l�u�ƶq�C��u�������u�q�B3.���O���u�������A�A�T�ӱ��󳣺����A�N�i�H���u��
        if (Input.GetKeyDown(KeyCode.R))
            weaponInUse.GetComponent<Weapon>().Reload();

        // �P�_�G���U�Ʀr��1�A�������Z��0
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0, 0);

        // �P�_�G���U�Ʀr��2�A�������Z��1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(0, 1);

        // �P�_�G�u�ʷƹ��u��
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)      // ���e�u��
            SwitchWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // ����u��
            SwitchWeapon(-1);
    }

    // ��k�G�Z�������A�Ѽ�_addNumber�B_weaponNumber
    private void SwitchWeapon(int _addNumber, int _weaponNumber = 0)
    {
        // �N�Z���M��������áA���@���������áA�A��ܻݭn���Z��
        foreach (GameObject item in weaponObjects)
        {
            item.SetActive(false);
        }

        // switch �P�_���G�H�Ѽ�_addNumber�P�_�n�������Z��
        switch (_addNumber)
        {
            case 0:                                                   // _addNumber == 0�A�N��Ϋ��䪽�����w�Z���}�C��}
                weaponNumber = _weaponNumber;
                break;
            case 1:                                                   // _addNumber == 1�A�N���W�u�ƹ��u��
                if (weaponNumber == weaponObjects.Length - 1)         // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�w�g�O�̫�@�ӪZ���A�h�N�Z���}�C��}�]�w��0
                    weaponNumber = 0;
                else
                    weaponNumber += 1;
                //weaponNumber = (weaponNumber == weaponObjects.Length - 1) ? 0 : weaponNumber += 1; // �]�i�H��H�W���P�_���g���o��
                break;
            case -1:                                                   // _addNumber == -1�A�N���U�u�ƹ��u��
                if (weaponNumber == 0)                                 // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�O�Ĥ@�ӪZ���A�h�N�Z���}�C��}���M�檺�̫�@�Ӧ�}
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;
                //weaponNumber = (weaponNumber == 0) ? weaponObjects.Length - 1 : weaponNumber -= 1; // �]�i�H��H�W���P�_���g���o��
                break;
        }
        weaponObjects[weaponNumber].SetActive(true);    // ��ܩҫ��w���Z��
        weaponInUse = weaponObjects[weaponNumber];      // �]�w�ثe�ҿ�ܪ��Z������(���ɥi�H�ΨӰ���Z���үS�w����k�A�U�@���`�|����)
    }
}
