using Photon.Realtime;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class HardcodeCSVRead : MonoBehaviour
{
    public GameObject myPrefab;
    public string fileName;
    public GameObject currentQuestion;
    public GameObject[] currentAnswersObj;
    public float xPos = -19.0f;
    public float yPos = 1.0f;
    public float zPos = -4.0f;
    private int answerCount = 5;
    private int[] currentAnswerList;

    private void Awake()
    {
        QADataManager.TestDataItems = HardCodeCSVReader.Read(fileName);
    }

    // Start is called before the first frame update
    private void Start()
    {
        // instantiate first 5 answer cubes
        CreateQACubes(answerCount);

        /*
        for (int i = 0; i < TestDataItems.Count; i++)
        {
            GameObject holdNewObj = (GameObject)Instantiate(myPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity); //Randomize the X,Y,Z position
            holdNewObj.name = "Ans" + i;
            holdNewObj.transform.Find("New Text").GetComponent<TextMesh>().text = TestDataItems[i].answerText;
            zPos += 3;
            currentQuestion.GetComponent<TextMesh>().text = TestDataItems[i].questionText;
            Debug.Log("question text： " + i + ", " + TestDataItems[i].questionText);
        }
        */
    }

    /// <summary>
    /// Generate questions' and answers' cubes
    /// </summary>
    /// <param name="answerCount">total annwers' number</param>
    private void CreateQACubes(int answerCount)
    {
        currentAnswersObj = new GameObject[answerCount];

        for (int i = 0; i < answerCount; i++)
        {
            GameObject holdNewObj = (GameObject)Instantiate(myPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity); //Randomize the X,Y,Z position
            currentAnswersObj[i] = holdNewObj;
            zPos += 3;
        }

        ChangeQAInfo(QADataManager.currQNum);
    }

    /// <summary>
    /// Change the content displayed by the question and answer
    /// </summary>
    /// <param name="currQuestionID">Current question's id</param>
    public void ChangeQAInfo(int currQuestionID)
    {
        currentQuestion.GetComponent<TextMesh>().text = QADataManager.TestDataItems[currQuestionID - 1].questionText;

        currentAnswerList = new int[answerCount];
        int tempAnswer;
        for (int i = 0; i < answerCount; i++)
        {
            if (i == QADataManager.TestDataItems[currQuestionID - 1].answerID - 1)
            {
                tempAnswer = int.Parse(QADataManager.TestDataItems[currQuestionID - 1].answerText);
                currentAnswersObj[i].transform.Find("Cube").Find("New Text").GetComponent<TextMesh>().text = tempAnswer + "";
                currentAnswersObj[i].name = "Ans" + i + " " + QADataManager.CorrectAnswerSymbol;
                currentAnswerList[i] = tempAnswer;
            }
            else
            {
                tempAnswer = RandomAnswer(0, 20, int.Parse(QADataManager.TestDataItems[currQuestionID - 1].answerText));
                currentAnswersObj[i].transform.Find("Cube").Find("New Text").GetComponent<TextMesh>().text = tempAnswer + "";
                currentAnswersObj[i].name = "Ans" + i;
                currentAnswerList[i] = tempAnswer;
            }
        }
    }

    /// <summary>
    /// random a answer for the answers' cubes except the correct one
    /// </summary>
    /// <param name="startNum">Start number at random range</param>
    /// <param name="endNum">end number at random range</param>
    /// <param name="correctAnswer">current correct answer</param>
    /// <returns></returns>
    private int RandomAnswer(int startNum, int endNum, int correctAnswer)
    {
        int finalAnswer = UnityEngine.Random.Range(startNum, endNum);
        while (finalAnswer == correctAnswer || currentAnswerList.Contains(finalAnswer))
        {
            finalAnswer = UnityEngine.Random.Range(startNum, endNum);
        }
        return finalAnswer;
    }

    public class HardCodeCSVReader
    {
        private static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        private static char[] TRIM_CHARS = { '\"' };

        public static List<DataItemTest> Read(string file)
        {
            List<DataItemTest> list = new List<DataItemTest>(); //can just use var as type
            TextAsset data = Resources.Load(file) as TextAsset;

            string[] lines = Regex.Split(data.text, LINE_SPLIT_RE);

            var firstLine = Regex.Split(lines[0], SPLIT_RE);

            //for all lines in csv...
            for (int i = 0; i < lines.Length; i++)
            {
                DataItemTest dataItem = new DataItemTest();
                string[] rowValues = Regex.Split(lines[i], SPLIT_RE);

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