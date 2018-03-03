using System;

namespace C2LP.Assistant.TMSConsole.Model
{
    [Serializable]
    public class MESSAGEHEAD
    {
        public string SENDER { get; set; }
        public string FILETYPE { get; set; }
        public string CONTENTTYPE { get; set; }
        public string SENDTIME { get; set; }
        public string FILENAME { get; set; }
        public string FILEFUNCTION { get; set; }
    }
}
