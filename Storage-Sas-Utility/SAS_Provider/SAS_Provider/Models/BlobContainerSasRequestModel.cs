using System.ComponentModel.DataAnnotations;

namespace SAS.Provider.Web.Models
{
    public class BlobContainerSasRequestModel
    {
        [Required]
        [Display(Name = "Storage Account Name")]
        public string StorageAccountName { get; set; }

        [Required]
        [Display(Name = "Storage Account Key")]
        [DataType(DataType.Password)]
        public string StorageAccountKey { get; set; }

        [Required]
        [Display(Name = "Blob Container Name")]
        public string BlobContainerName { get; set; }

        [Required]
        [Display(Name = "Activation Period")]
        public double ActivationPeriod { get; set; }

    }
}
