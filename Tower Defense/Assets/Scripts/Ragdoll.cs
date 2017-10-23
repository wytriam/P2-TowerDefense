using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : Enemy_Manager {

    public float walkCycleSpeed;
    public Rigidbody root;
    public CharacterJoint leftHip, rightHip, leftKnee, rightKnee, leftShoulder, rightShoulder, leftElbow, rightElbow, midSpine, neck;
    private Vector3 rootNeutralPos, leftHipNeutralPos, rightHipNeutralPos, leftKneeNeutralPos, rightKneeNeutralPos, leftShoulderNeutralPos, rightShoulderNeutralPos, leftElbowNeutralPos, rightElbowNeutralPos, midSpineNeutralPos, neckNeutralPos;
    private Quaternion uprightRotation, rootNeutralRot, leftHipNeutralRot, rightHipNeutralRot, leftKneeNeutralRot, rightKneeNeutralRot, leftShoulderNeutralRot, rightShoulderNeutralRot, leftElbowNeutralRot, rightElbowNeutralRot, midSpineNeutralRot, neckNeutralRot;
    private float counter;
    private int foot = 0;

    void Awake(){
        uprightRotation = transform.rotation;
        rootNeutralPos = root.transform.localPosition;
        rootNeutralRot = root.transform.localRotation;
        leftHipNeutralPos = leftHip.transform.localPosition;
        leftHipNeutralRot = leftHip.transform.localRotation;
        rightHipNeutralPos = rightHip.transform.localPosition;
        rightHipNeutralRot = rightHip.transform.localRotation;
        leftKneeNeutralPos = leftKnee.transform.localPosition;
        leftKneeNeutralRot = leftKnee.transform.localRotation;
        leftKneeNeutralPos = leftKnee.transform.localPosition;
        leftKneeNeutralRot = leftKnee.transform.localRotation;
        rightKneeNeutralPos = rightKnee.transform.localPosition;
        rightKneeNeutralRot = rightKnee.transform.localRotation;
        leftShoulderNeutralPos = leftShoulder.transform.localPosition;
        leftShoulderNeutralRot = leftShoulder.transform.localRotation;
        rightShoulderNeutralPos = rightShoulder.transform.localPosition;
        rightShoulderNeutralRot = rightShoulder.transform.localRotation;
        leftElbowNeutralPos = leftElbow.transform.localPosition;
        leftElbowNeutralRot = leftElbow.transform.localRotation;
        rightElbowNeutralPos = rightElbow.transform.localPosition;
        rightElbowNeutralRot = rightElbow.transform.localRotation;
        midSpineNeutralPos = midSpine.transform.localPosition;
        midSpineNeutralRot = midSpine.transform.localRotation;
        neckNeutralPos = neck.transform.localPosition;
        neckNeutralRot = neck.transform.localRotation;
    }
    
    void Start () {
		
	}
	
	void Update () {
        counter += Time.deltaTime;
        if(counter >= walkCycleSpeed){
            if (foot == 0) foot++;
            else if (foot == 1) foot -= 2;
            else if (foot == -1) foot++;
            Walk(foot);
            counter = 0.0F;
        }
	}

    void Walk(int foot){
        if (foot == 1) StepRight();
        if (foot == -1) StepLeft();
        if (foot == 0) Neutral();
    }

    void Neutral(){
        root.transform.localPosition = rootNeutralPos;
        root.transform.localRotation = rootNeutralRot;
        transform.rotation = uprightRotation;
        leftHip.transform.localPosition = leftHipNeutralPos;
        leftHip.transform.localRotation = leftHipNeutralRot;
        rightHip.transform.localPosition = rightHipNeutralPos;
        rightHip.transform.localRotation = rightHipNeutralRot;
        leftKnee.transform.localPosition = leftKneeNeutralPos;
        leftKnee.transform.localRotation = leftKneeNeutralRot;
        rightKnee.transform.localPosition = rightKneeNeutralPos;
        rightKnee.transform.localRotation = rightKneeNeutralRot;
        leftShoulder.transform.localPosition = leftShoulderNeutralPos;
        leftShoulder.transform.localRotation = leftShoulderNeutralRot;
        rightShoulder.transform.localPosition = rightShoulderNeutralPos;
        rightShoulder.transform.localRotation = rightShoulderNeutralRot;
        leftElbow.transform.localPosition = leftElbowNeutralPos;
        leftElbow.transform.localRotation = leftElbowNeutralRot;
        rightElbow.transform.localPosition = rightElbowNeutralPos;
        rightElbow.transform.localRotation = rightElbowNeutralRot;
        midSpine.transform.localPosition = midSpineNeutralPos;
        midSpine.transform.localRotation = midSpineNeutralRot;
        neck.transform.localPosition = neckNeutralPos;
        neck.transform.localRotation = neckNeutralRot;
    }

    void StepRight(){
        rightHip.transform.localRotation *= Quaternion.Euler(0, 0, -1 * Time.deltaTime);
    }

    void StepLeft(){

    }
}
