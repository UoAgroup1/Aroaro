using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class HardcodeCSVRead : MonoBehaviour
{
    public GameObject myPrefab;
    public string fileName;
    public GameObject currentQuestion;
    public float xPos = -19.0f;
    public float yPos = 1.0f;
    public float zPos = -4.0f;

    List<DataItemTest> TestDataItems;
    private void Awake()
    {
        TestDataItems = HardCodeCSVReader.Read(fileName);
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach (DataItemTest objectToPlot in TestDataItems)
        {
            var holdNewObj = (GameObject)Instantiate(myPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity); //Randomize the X,Y,Z position
            holdNewObj.transform.Find("New Text").GetComponent<TextMesh>().text = objectToPlot.answerText;
            zPos += 3;
            currentQuestion.GetComponent<TextMesh>().text = objectToPlot.questionText;
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public class HardCodeCSVReader
    {
        static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static char[] TRIM_CHARS = { '\"' };

        public static List<DataItemTest> Read(string file)
        {
            List<DataItemTest> list = new List<DataItemTest>(); //can just use var as type
            TextAsset data = Resources.Load(file) as TextAsset;

            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            var firstLine = Regex.Split(lines[0], SPLIT_RE);


            //for all lines in csv...
            for (int i = 0; i < lines.Length; i++)
            {
                DataItemTest dataItem = new DataItemTest();
                var rowValues = Regex.Split(lines[i], SPLIT_RE);

                //check if there is a blank row, instead of breaking continue to next iteration
                if (rowValues.Length == 0 || rowValues[0] == "") continue;

                //in the current line, look at each row
                int fQuestionID;
                if (int.TryParse(rowValues[0], out fQuestionID))
                {
                    dataItem.questionID = fQuestionID;
                }

                dataItem.questionText = rowValues[1];

                int fAnswerID;
                if (int.TryParse(rowValues[2], out fAnswerID))
                {
                    dataItem.answerID = fAnswerID;
                }

                dataItem.answerText = rowValues[3];


                //add to list of items
                list.Add(dataItem);
            }

            return list;

        }

    }
}
