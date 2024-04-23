namespace PracticeProject.Worker.Settings
{
    public class MailSettings
    {
        public string From { get; private set; }
        public string Host { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public int Port { get; private set; }
    }
}
