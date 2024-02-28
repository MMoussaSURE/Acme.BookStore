using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Values;

namespace Acme.BookStore.ValueObjects
{
    [ComplexType]
    public class Address : ValueObject
    {
        public string? Street { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string? Country { get; private set; }
        public string? ZipCode { get; private set; }
        private Address()
        {

        }

        public Address( string? street=null, string? city = null,string? state = null,string? country = null,string? zipCode = null)
        {
           
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;

        }
    }
}
