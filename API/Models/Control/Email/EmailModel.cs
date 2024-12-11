namespace API.Models.Control.Email
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public ICollection<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }
}
