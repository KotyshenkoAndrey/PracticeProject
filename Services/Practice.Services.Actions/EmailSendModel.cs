namespace PracticeProject.Services.Actions
{
    public class EmailSendModel
    {
        public string From { get; set; }
        public List<string> Receiver { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
