using System;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Constants
{
	public static class Messages
	{
		public static string ProductAdded = "Ürün Eklendi";
		public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "kategoride en fazla 10 ürün olabilir";

        public static string ProductNameAlreadyExists = "Eklenilen ürün isminde kayıt var";
        public static string CategoryLimitExceded = "Kategori 15'i aştığı için ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz Yoktur";
        public static string UserRegistered = "Kayıt olundu";
        public static string UserNotFound = "Kullanıcı yok";

        public static string PasswordError = "Şifre hatalı";
        public static string UserAlreadyExists = "Kullanıcı zaten var";
        public static string SuccessfulLogin = "Giriş Başarılı";

        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}

