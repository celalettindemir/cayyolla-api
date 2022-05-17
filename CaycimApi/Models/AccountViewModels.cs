using System;
using System.Collections.Generic;

namespace CaycimApi.Models
{
    // Models returned by AccountController actions.
    public class UserAddViewModel
    {
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string CompanyName { get; set; }
        public string GenderType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean Enable { get; set; }
        public string Rol { get; set; }
    }
    public class AidatAddUserViewModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
    }
    public class AidatViewModel
    {
        public string ID { get; set; }
        public string CayciCompanyName { get; set; }
        public float Ucret { get; set; }
        public string AidatAralik { get; set; }
    }
    public class AidatsizCayciViewModel
    {
        public string ID { get; set; }
        public string CompanyNameAndPhone { get; set; }
    }
    public class AidatAddViewModel
    {
        public string CayciId { get; set; }
        public int Ucret { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUser { get; set; }
    }
    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class CayciUrunlerViewModel
    {
        public int Id { get; set; }
        public int UrunId { get; set; }
        public string UrunIsim { get; set; }
        public bool IsPublish { get; set; }
        public float Fiyat { get; set; }
    }

    public class CayciUrunViewModel
    {
        public string CayciId { get; set; }
        public int UrunId { get; set; }
        public bool IsPublish { get; set; }
        public float Fiyat { get; set; }
    }

    public class FaturaViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public float Ucret { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        public DateTime SonOdemeTarihi { get; set; }
        public bool OdemeDurumu { get; set; }
        public string PhoneNumber { get; set; }

    }
}
