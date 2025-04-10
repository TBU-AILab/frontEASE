namespace FrontEASE.Domain.Entities.Shared.Files
{
    public class File
    {
        public File()
        {
            this.Name = this.MimeType = string.Empty;
            this.Content = [];
        }

        #region Data

        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }

        #endregion
    }
}
