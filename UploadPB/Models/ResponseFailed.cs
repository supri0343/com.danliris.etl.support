namespace UploadPB.Models
{
    public class ResponseFailed
    {
        public ResponseFailed(string message, object data = null)
        {
            this.message = message;
            this.data = data;
        }
        public string message {get; set;}
        public object? data {get; set;}
    }
}