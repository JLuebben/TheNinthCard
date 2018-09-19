using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimation : MonoBehaviour {

    public Image leftPanel;

    private float leftPanelTargetX = 0;
    private float leftPanelHiddenX = 0;
    private float leftPanelRange = 0;


    // Use this for initialization
    void Start () {
        leftPanelHiddenX = leftPanel.transform.position.x;
        leftPanelTargetX = leftPanelHiddenX;
        leftPanelRange = -leftPanelHiddenX-15;

    }
	
	// Update is called once per frame
	void Update () {
        float leftPanelX = leftPanel.transform.position.x;
        float delta = leftPanelTargetX - leftPanelX;
        if (Mathf.Abs( delta) > 1)
		{
            float newX = delta * Time.deltaTime * 10;
            leftPanel.transform.position += new Vector3(newX, 0, 0);
        }
	}

    public void showLeftPanel()
    {
        leftPanelTargetX = leftPanelRange;
    }

    public void hideLeftPanel()
    {
        leftPanelTargetX = leftPanelHiddenX;
    }
}
