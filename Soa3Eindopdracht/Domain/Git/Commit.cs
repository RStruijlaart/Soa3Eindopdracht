using System;
using Soa3Eindopdracht.Domain.Sprints;

namespace Soa3Eindopdracht.Domain.Git
{
    public class Commit
    {
        public string Message { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Sprint Sprint { get; private set; }

        public Commit(string message, Sprint sprint)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Commit message is verplicht.");

            Message = message;
            Timestamp = DateTime.Now;
            Sprint = sprint;
        }
    }
}