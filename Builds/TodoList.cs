using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodoList : MonoBehaviour
{
    // Start is called before the first frame update



    public InputField TitleField;
    public InputField DueField;
    public GameObject AddButton;

    public GameObject[] tasks= new GameObject[9];

    public MinHeap minHeap = new MinHeap();


    void Start()
    {
        foreach (var task in tasks){
            task.GetComponent<Image>().enabled = false;
            task.GetComponentInChildren<Text>().enabled = false;
            
        }


    }
    private void Clicked(GameObject butt) {
        //Debug.Log(butt.GetComponent<ButtonBrain>().val + " clicked");
        var brain = butt.GetComponent<ButtonBrain>();
        Debug.Log("working with " +butt.name);
        Debug.Log("removing" + brain.val);
        removeTask(brain.priority, brain.val);
        //minHeap.Remove(due, val);
        //Debug.Log("");
        Debug.Log(minHeap.Count);
    }

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(minHeap.Count);
        UpdateTasksDisplay();



    }

    private void UpdateTasksDisplay2() {
        int i = 0;
        MinHeap thisHeap = minHeap.Copy();
        foreach (GameObject task in tasks) {
            task.GetComponent<Image>().enabled = false;
            task.GetComponentInChildren<Text>().text = "";
            task.GetComponentInChildren<Text>().enabled = false;
        }
        while (i < thisHeap.Count) {

            (int due, string val) = thisHeap.ExtractMin();
            tasks[i].GetComponent<Image>().enabled = true;
            tasks[i].GetComponentInChildren<Text>().text = val;
            tasks[i].GetComponentInChildren<Text>().enabled = true;
            i++;
        }
    
    }



    public void addTask() {
        int due = int.Parse(DueField.text);
        string val = TitleField.text;
        minHeap.Insert(due, val);
        tasks[minHeap.Count - 1].GetComponent<Button>().onClick.AddListener(() => Clicked(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject));

        Debug.Log("added \"" + TitleField.text + "\" to minHeap");
        DueField.text = "";
        TitleField.text = "";
        Debug.Log(minHeap.Count);


        

    }

    private void UpdateTasksDisplay()
    {
        // Clear all task displays
        foreach (var task in tasks)
        {
            task.GetComponent<Image>().enabled = false;
            task.GetComponentInChildren<Text>().enabled = false;
            //task.GetComponent<Button>().interactable = false;
            task.GetComponentInChildren<Text>().text = "";
        }

        // Update task displays with minHeap values
        for (int i = 0; i < minHeap.Count; i++)
        {
            var (priority, value) = minHeap.heap[i];
            tasks[i].GetComponent<ButtonBrain>().priority = priority;
            tasks[i].GetComponent<ButtonBrain>().val = value;
            tasks[i].GetComponent<Image>().enabled = true;
            tasks[i].GetComponentInChildren<Text>().text = value;
            tasks[i].GetComponentInChildren<Text>().enabled = true;
            //tasks[i].GetComponent<Button>().interactable = true;

        }
    }

    public void removeTask(int due, string value) {
        minHeap.Remove(due, value);

    }

}


