using Acme.BookStore.Common.Address;
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

        public CreateAddressDto HomeAddress { get; set; }
        public CreateAddressDto BusinessAddress { get; set; }


    }
}
