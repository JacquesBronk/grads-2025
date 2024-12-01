namespace Retro.Seeder;

public class Job
{
    public Guid JobId { get; set; }
    public required string JobName { get; set; }
    public Exception? Exception { get; set; }
    public DateTime? StartTime { get; set; }
    public bool IsStarted { get; set; }
    public bool IsCompleted { get; set; }
}