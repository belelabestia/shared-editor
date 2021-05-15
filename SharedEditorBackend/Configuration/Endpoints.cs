namespace SharedEditorBackend
{
    public class Endpoints
    {
        public Frontend Frontend { get; set; }
        public Backend Backend { get; set; }
    }

    public class Frontend
    {
        public string UserAction { get; set; }
        public string Editor { get; set; }
    }

    public class Backend
    {
        public string SignalRPrefix { get; set; }
        public string UserAction { get; set; }
        public string Editor { get; set; }
    }
}