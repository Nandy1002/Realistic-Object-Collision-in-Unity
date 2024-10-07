using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assignment1UI : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int number;
    [SerializeField] private Vector3 initPosition;
    private List<GameObject> items = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        SpwanItems();
    }
    private void SpwanItems(){

        if (!PlayerPrefs.HasKey("UI Items"))
        {
            // no items are saved to create and spwan items
            for (int i = 0; i < number; i++)
            {
                //spwan a cross item with prefab
                GameObject cross = Instantiate(itemPrefab, transform, true);
                cross.transform.position = new Vector3(initPosition.x, initPosition.y + i * 500f/number, initPosition.z);
                cross.SetActive(true);
                items.Add(cross);
                GameObject animate = cross.transform.Find("AnimateItem").gameObject;
                Button button = cross.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => OnButtonClick(animate));
                SaveGameObjectList(items, "UI Items");
            }
        }
        else
        {
            // items are saved so load the save object and position them.
            items = LoadGameObjectList("UI Items");
            for (int i = 0; i < items.Count; i++)
            {
                //spwan a cross item
                GameObject cross = items[i];
                cross.transform.SetParent(transform);
                cross.transform.position = new Vector3(initPosition.x, initPosition.y + i * 500f/number, initPosition.z);
                cross.SetActive(true);
                GameObject animate = cross.transform.Find("AnimateItem").gameObject;
                Button button = cross.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => OnButtonClick(animate));
                SaveGameObjectList(items, "UI Items");
            }
        }
    }
    private void OnButtonClick(GameObject animateObject){
        // animate ease in back on click
        LeanTween.moveLocalX(animateObject, 350, 1f).setEase(LeanTweenType.easeInBack);
        if (items.Contains(animateObject.transform.parent.gameObject))
        {
            GameObject parent = animateObject.transform.parent.gameObject;
            //Debug.Log(animateObject.name + " Pressed and going to be removed");
            items.Remove(parent);
            SaveGameObjectList(items, "UI Items");
            //wait sometime and destroy object
            StartCoroutine(WaitForSomeSecondsAndDelete(5f,parent));
        }
        else
        {
            Debug.Log("Object not found in the List");
        }
        
    }
    
    // Save the GameObject names to PlayerPrefs
    private void SaveGameObjectList(List<GameObject> objectList, string key)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            // Save the name of each GameObject in the list
            PlayerPrefs.SetString(key + "_" + i, objectList[i].name);
        }

        // Save the count of the list
        PlayerPrefs.SetInt(key + "_count", objectList.Count);
        PlayerPrefs.Save();
    }

    // Load the GameObject list from PlayerPrefs
    private List<GameObject> LoadGameObjectList(string key)
    {
        List<GameObject> objectList = new List<GameObject>();

        // Get the number of saved GameObjects
        int count = PlayerPrefs.GetInt(key + "_count", 0);

        // Load each GameObject by its saved name
        for (int i = 0; i < count; i++)
        {
            string objectName = PlayerPrefs.GetString(key + "_" + i, "");

            // Find the GameObject by name (make sure they exist in the scene)
            GameObject obj = GameObject.Find(objectName);
            if (obj != null)
            {
                objectList.Add(obj);
            }
            else
            {
                Debug.Log("GameObject with name " + objectName + " not found in the scene.");
            }
        }
        return objectList;
    }
    IEnumerator WaitForSomeSecondsAndDelete(float waitTime,GameObject objectToDelete)
    {
        Debug.Log("Waiting for " + waitTime + " seconds...");

        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);

        Debug.Log(waitTime + " seconds have passed!");
        Destroy(objectToDelete);
        
    }
}
