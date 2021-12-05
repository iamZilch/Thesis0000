using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject correctAnswer;
    public GameObject wrongAnswer;

    public GameObject Quizpanel;
    //public GameObject GoPanel;
    public GameObject EndGame;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0;
    public int score;
    int totalAnswered;

    public void StartQuizMode()
    {
        totalQuestions = QnA.Count;
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        EndGame.SetActive(true);
        //GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        //when you are right
        score += 1;
        QnA.RemoveAt(currentQuestion);
        correctAnswer.SetActive(true);
        StartCoroutine(correctAnswerCountdown());
        StartCoroutine(waitForNext());
    }

    public void wrong()
    {
        //when you answer wrong
        QnA.RemoveAt(currentQuestion);
        wrongAnswer.SetActive(true);
        StartCoroutine(wrongAnswerCountdown());
        StartCoroutine(waitForNext());
    }

    IEnumerator correctAnswerCountdown()
    {
        yield return new WaitForSeconds(1f);
        correctAnswer.GetComponent<UITweener>().Disable();
    }
    IEnumerator wrongAnswerCountdown()
    {
        yield return new WaitForSeconds(1f);
        wrongAnswer.GetComponent<UITweener>().Disable();
    }


    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }



    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {

            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0) //adjust
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;

            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }

    public void backToMainMenu()
    {
        addPlayerExp();
        SceneManager.LoadScene("Main_Menu_Scene");
    }

    public void addPlayerExp()
    {
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += 3 * score;
        GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += score * 3;
        SaveData.SaveDataProgress(Database.instance);
    }
}
