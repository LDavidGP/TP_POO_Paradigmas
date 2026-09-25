namespace JuegoElementos.ConsoleApp.Models;

public class CombatLog
{
    private Queue<string> _logQueue = new();
    private const int MaxLines = 3;

    public IReadOnlyList<string> GetVisibleLines()
    {
        var lines = new List<string>(_logQueue);
        while (lines.Count < MaxLines)
        {
            lines.Add(string.Empty);
        }
        return lines;   
    }

    public void AddLogMessage(string msg)
    {
        if(_logQueue.Count >= MaxLines)
            _logQueue.Dequeue();
        _logQueue.Enqueue(msg);
    }
    
    public void Clear() => _logQueue.Clear();
}