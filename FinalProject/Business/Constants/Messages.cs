using System;
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
    }
}

