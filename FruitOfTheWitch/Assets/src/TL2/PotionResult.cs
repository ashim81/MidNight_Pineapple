using System.Collections.Generic;

public abstract class PotionResult
{
    public abstract string GetMessage();
    public abstract bool CanExitLevel();
}

public class BlueMoonPotionResult : PotionResult
{
    public override string GetMessage()
    {
        return "Congratulations you have created BlueMoon potion, You pass";
    }

    public override bool CanExitLevel()
    {
        return true;
    }
}

public class WrongPotionResult : PotionResult
{
    public override string GetMessage()
    {
        return "Wrong potion, want to try again?";
    }

    public override bool CanExitLevel()
    {
        return false;
    }
}

public static class PotionEvaluator
{
    public static PotionResult Evaluate(List<string> selectedPotionNames, List<string> correctPotionNames)
    {
        if (selectedPotionNames == null || correctPotionNames == null)
        {
            return new WrongPotionResult();
        }

        if (selectedPotionNames.Count != correctPotionNames.Count)
        {
            return new WrongPotionResult();
        }

        List<string> selectedSorted = new List<string>(selectedPotionNames);
        List<string> correctSorted = new List<string>(correctPotionNames);

        selectedSorted.Sort();
        correctSorted.Sort();

        for (int i = 0; i < correctSorted.Count; i++)
        {
            if (selectedSorted[i] != correctSorted[i])
            {
                return new WrongPotionResult();
            }
        }

        return new BlueMoonPotionResult();
    }
}