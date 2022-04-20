using System;

namespace Demo.Accounts
{
    public class UploadedDocumentDetails
    {
        /// <summary>
        /// Doc Url
        /// </summary> 
        public string DocUrl { get; set; }
        /// <summary>
        /// DocID
        /// </summary>
        public string DocId { get; set; }
        /// <summary>
        /// Exposed File name
        /// </summary>
        public string DocName { get; set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// File size
        /// </summary>
        public long? FileSizeBytes { get; set; }
        /// <summary>
        /// CreatedTimeUTC
        /// </summary>
        public DateTime CreatedTimeUTC { get; set; }
    }
}