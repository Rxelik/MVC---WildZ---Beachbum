using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inisializer : MonoBehaviour
{
	public int InsializeDeckSize = 52;

	public List<Color> Colors;
	int colorRND;
	int numRND;
	void Awake()
	{
        for (int i = 0; i < InsializeDeckSize; i++)
        {
			colorRND = Random.Range(0, 4);
			numRND = Random.Range(1, 9);
			var modelFactory = new CardModelFactory();
			var model = modelFactory.Model;

			// Set some initial state
			model.Position = new Vector3(0, 0, 0);
			model.Color = Colors[colorRND];
			model.Number = numRND;
			// Create the view
			var viewFactory = new CardViewFactory();
			var view = viewFactory.View;

			// Create the controller
			var controllerFactory = new CardControllerFactory(model, view);
			var controller = controllerFactory.Controller;
		}

	}
}
