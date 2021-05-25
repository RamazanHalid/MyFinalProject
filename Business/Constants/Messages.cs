using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakimda";
        public static string ProductListed = "Urunler listelendi";
        public static string ProductCountOfCategoryError = "Urun category sayisi fazla";
        public static string ProductNameAlreadyExists = "Produt Name already exists!";
        public static string CategoryLimitedExceded = "Category limiti doldu";
        public static string AuthorizationDenied = "Yetkiniz yok";
        
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError="Şifre hatalı";
        public static string SuccessfulLogin="Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated="Access token başarıyla oluşturuldu";

       

    }
}