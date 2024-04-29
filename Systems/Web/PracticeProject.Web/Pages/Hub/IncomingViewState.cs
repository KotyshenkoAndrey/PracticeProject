namespace PracticeProject.Web.Pages.Hub
{
    public class IncomingViewState
    {
        public event Action OnChange;

        private List<string> messages = new List<string>();

        public List<string> Messages => messages;

        public void AddMessage(string message)
        {
            messages.Add(message);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
