using System.Collections.Generic;

public class QADataManager
{
    public static List<DataItemTest> TestDataItems;

    public static string CorrectAnswerSymbol = "CorrectAnswerObj"; // current Q's correct answer symbol, was used to name cubes
    public static bool IsHadWrongChoice = false; // This question needs to correct the answer, and the score is zero.
    public static int CurrentTotalScore = 0; // Current total score
    public static int currQNum = 1; // The default is from the first question
    public static bool IsGameOver = false; // Whether to complete all the questions

    /// <summary>
    /// Before restart the game, reset these data
    /// </summary>
    public static void ResetDataInfo()
    {
        IsHadWrongChoice = false;
        CurrentTotalScore = 0;
        currQNum = 1;
        IsGameOver = false;
    }
}