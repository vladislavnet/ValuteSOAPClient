namespace SOAPClient
{
    public interface IView
    {
        IViewModel ViewModel { get; set; }
        void Show();
    }
}
