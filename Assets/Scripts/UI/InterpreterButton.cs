using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Resources;
using TMPro;

public class InterpreterButton : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public VerticalLayoutGroup vertical;
   public CardFactory cardFactory;
    public void InterpretButton()
    {
        for (int i = 0; i < vertical.transform.childCount; i++)
        {
            Destroy(vertical.transform.GetChild(0).gameObject);
        }
        Debug.Log("InterpreterButton");


        if (cardFactory != null)
        {
            Debug.Log("Compilar");

            string filePath = @"C:\Users\Barbaro\Documents\Personal\Estudio\Programación\Proyectos Pro\Proyectos de la escuela\Proyecto Gwent Pro\Gwwn-Pro (Segundo Proyecto)\Assets\Scripts\Interpreter\Input\input.txt";
            SaveInputToFile(filePath, inputText.text);

            cardFactory.ProcessInput(filePath);
        }
    }

    private void SaveInputToFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content.Substring(0,content.Length-1));
            Debug.Log("Contenido guardado en el archivo: " + filePath);
        }
     
        catch (Exception ex)
        {
            Debug.LogError("Error al guardar el archivo: " + ex.Message);
        }
    }
    public void BackButton()
    {
        Debug.Log("BackButton");
        SceneManager.LoadScene("Menu");
    }
}
