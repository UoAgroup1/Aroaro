using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CubeProcessing : MonoBehaviour
{
    private GameObject particleObj_firework;
    private GameObject explosionObj;
    private ParticleSystem particleSystem_firework;

    // private GameObject particleObj_firework; private ParticleSystem particleSystem_firework;
    private float shakeAmount = 0.05f; // amplitude

    private bool is_shake;
    private Vector3 first_pos;

    private void Start()
    {
        first_pos = this.transform.localPosition;
    }

    private GameObject LoadResources(string path)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>(path)) as GameObject;
        // if (obj) { obj.name = "LoadObj_" + path; return obj; } return null;
    }

    public void StartShake()
    {
        is_shake = true;
    }

    public void Update()
    {
        if (!is_shake) return;
        Vector3 pos = first_pos + Random.insideUnitSphere * shakeAmount;
        pos.y = transform.localPosition.y;
        transform.localPosition = pos;
    }

    public void EndShake()
    {
        is_shake = false;
        first_pos.y = transform.localPosition.y;
        transform.localPosition = first_pos;
    }

    /// <summary>
    /// when choose one of the answer cubes
    /// </summary>
    /// <param name="args"></param>
    public void OnTap(ManipulationEventData args)
    {
        if (!QADataManager.IsGameOver)
        {
            if (JudgeTheAnswer())
            {
                if (QADataManager.IsHadWrongChoice)
                {
                    Debug.Log("right answer, score + 0");
                }
                else
                {
                    Debug.Log("right answer, score + 1");
                    QADataManager.CurrentTotalScore += 1;
                }
                QADataManager.IsHadWrongChoice = false;
                AnswerEffect(true);
            }
            else
            {
                QADataManager.IsHadWrongChoice = true;
                AnswerEffect(false);
                Debug.Log("wrong answer，retry");
            }
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    private void AnswerEffect(bool isRight)
    {
        if (isRight)
        {
            // explosion + fireworks
            StartCoroutine(TimeLimittedEffect_Correct());
        }
        else
        {
            // shake + red
            StartCoroutine(TimeLimittedEffect_Wrong());
        }
    }

    private IEnumerator TimeLimittedEffect_Correct()
    {
        transform.Find("Cube").gameObject.SetActive(false);

        particleObj_firework = LoadResources("Fireworks");
        particleSystem_firework = particleObj_firework.GetComponentInChildren<ParticleSystem>();
        particleObj_firework.transform.localPosition = transform.localPosition;

        explosionObj = LoadResources("ExplorCubes");
        explosionObj.transform.localPosition = transform.localPosition;

        ChangeToNextQuestion();

        yield return new WaitForSeconds(2); // particleSystem_firework.duration

        particleSystem_firework.Stop();

        Destroy(particleObj_firework);
        Destroy(explosionObj);
        transform.Find("Cube").gameObject.SetActive(true);
    }

    private IEnumerator TimeLimittedEffect_Wrong()
    {
        transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.red;
        StartShake();
        yield return new WaitForSeconds(1);
        transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.white;
        EndShake();
    }

    /// <summary>
    /// Determine if the answer is correct
    /// </summary>
    private bool JudgeTheAnswer()
    {
        if (transform.name.Contains(QADataManager.CorrectAnswerSymbol))
            return true;
        return false;
    }

    /// <summary>
    /// switch to next question
    /// </summary>
    private void ChangeToNextQuestion()
    {
        QADataManager.currQNum += 1;
        if (QADataManager.currQNum <= QADataManager.TestDataItems.Count)
        {
            GameObject.Find("CSVReader").GetComponent<HardcodeCSVRead>().ChangeQAInfo(QADataManager.currQNum);
        }
        else
        {
            QADataManager.IsGameOver = true;
            GameObject.Find("Quiz").transform.Find("QText").GetComponent<TextMesh>().text = "Final Score: " + QADataManager.CurrentTotalScore;
        }
    }
}