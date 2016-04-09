using UnityEngine;
using System.Collections;

public class InflationTest : MonoBehaviour {

    public Animator animator;
    public SkinnedMeshRenderer skinnedMesh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("MorphArgument", (Time.time % 1f) * 1);
        skinnedMesh.SetBlendShapeWeight(0, (Time.time % 1f) * 100f);
	}
}
