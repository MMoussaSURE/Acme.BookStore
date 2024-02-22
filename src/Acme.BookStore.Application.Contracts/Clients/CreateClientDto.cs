using System;
using System.ComponentModel.DataAnnotations;


namespace Acme.BookStore.Clients
{
    public class CreateClientDto
    {
        [Required]
        [StringLength(ClientConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(ClientType))]
        public ClientType Type { get; set; }

    }
}
