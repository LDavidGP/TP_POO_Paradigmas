using TP_POO_Paradigmas.Console.Engine.Interfaces;

namespace TP_POO_Paradigmas.Console.Engine.Components;

public class NumericInputFormatter : IInputFormatter
{
    //To avoid the user inserts something nothing to do 
    public string Format(string current, string next)
    {
        string cleanNext = next.Replace(",", "").Replace(".", ""); //To allow format with "," and "." and do the validation
        if (string.IsNullOrEmpty(cleanNext) || next == "-") //To allow it if it's empty or is negative
            return cleanNext;
        //If it valid, continue
        if (int.TryParse(cleanNext, out int value))
            return value.ToString("N0");    

        //Else, nothing
        return current;
    }
}