using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_View : MonoBehaviour {

    public GameObject manager;
    public Image i;
    public Sprite def;
    public Sprite Chicken;
    public Sprite Beef;
    public Sprite Egg;
    public Sprite Pepper;
    public Sprite Wine;
    public Sprite Rice;
    public Sprite Lemon;
    public Sprite Licorice;
    public int cmbnum;
    public string n;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
        i=GetComponent<Image>();
        def = i.sprite;
        n = "";
	}
	
	//Sets image based on the selected contents and nullifies it if an ingredient is removed
	void Update () {
        if (manager.GetComponent<Ingredient_Selection>().selected.Count >= cmbnum)
        {
           n =manager.GetComponent<Ingredient_Selection>().selected[cmbnum-1].name;
            if (n == "Chicken")
            {     
                i.sprite = Chicken;
            }
            else if (n == "Wine")
            {
                i.sprite = Wine;
            }
            else if (n == "Beef")
            {
                i.sprite = Beef;
            }
            else if (n == "Rice")
            {
                i.sprite = Rice;
            }
            else if (n == "Egg")
            {
                i.sprite = Egg;
            }
            else if (n == "Pepper")
            {
                i.sprite = Pepper;
            }
            else if (n=="Lemon") {
                i.sprite = Lemon;
            }
            else if (n == "Licorice")
            {
                i.sprite = Licorice;
            }
           

        }
        else
        {
            i.sprite = def;
        }

    }
}
