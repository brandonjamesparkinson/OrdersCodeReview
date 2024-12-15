namespace Domain;

public class OutboxMessage
{
    private OutboxMessage() {}

    public OutboxMessage(string payloadJson)
    {
        MessageId = Guid.NewGuid().ToString();
        Created = DateTime.UtcNow;
        // consider a method to increment retrycount rather than hard set to 0
        RetryCount = 0;
        // ensure payload is not null or empty 
        Payload = payloadJson;
    }

    public int OutboxMessageId { get; private set; }
    public string MessageId { get; private set; } // if messageID is always a guid maybe change to a Guid Type?
    public DateTime Created { get; private set; }
    public int RetryCount { get; private set; }
    public string Payload { get; private set; }
}