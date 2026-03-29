using BKU.ProjectManagement.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Shared.Helpers
{
    public static class Helper { 
        public static string Encrypt(string source, string key) 
            { using (TripleDES tripleDESCryptoService = TripleDES.Create()) 
                { using (MD5 hashMD5Provider = MD5.Create()) { 
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key)); 
                    tripleDESCryptoService.Key = byteHash; 
                    tripleDESCryptoService.Mode = CipherMode.ECB; 
                    byte[] data = Encoding.UTF8.GetBytes(source); 
                    return Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length)); 
                } 
            } 
        } 
        
        public static string Decrypt(string encrypt, string key) 
        { 
            using (TripleDES tripleDESCryptoService = TripleDES.Create()) 
            { 
                using (MD5 hashMD5Provider = MD5.Create()) 
                { 
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key)); 
                    tripleDESCryptoService.Key = byteHash; 
                    tripleDESCryptoService.Mode = CipherMode.ECB; 
                    byte[] data = Convert.FromBase64String(encrypt); 
                    return Encoding.UTF8.GetString(tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length)); 
                } 
            } 
        } 
        public static DataTable BulkAsDataTable<T>(this IEnumerable<T> data) 
        { 
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T)); 
            var table = new DataTable(); foreach (PropertyDescriptor prop in properties) 
            { var isVirtual = typeof(T).GetProperty(prop.Name).GetGetMethod().IsVirtual; 
                var isPrimaryKey = prop.Name == "Id" ? true : false; if (isVirtual || isPrimaryKey) continue; 
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); 
            } 
            foreach (T item in data) 
            { 
                DataRow row = table.NewRow(); 
                foreach (PropertyDescriptor prop in properties) 
                { 
                    var isVirtual = typeof(T).GetProperty(prop.Name).GetGetMethod().IsVirtual; 
                    var isPrimaryKey = prop.Name == "Id" ? true : false; if (isVirtual || isPrimaryKey) continue;
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value; } table.Rows.Add(row); } return table; } public static string GetCurrentOperateUser(this IHttpContextAccessor accessor) { if (accessor == null) return string.Empty; return accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "username")?.Value ?? ""; } public static string GetCurrentLanguage(this IHttpContextAccessor accessor) { if (accessor?.HttpContext?.Request?.Headers?.TryGetValue("Accept-Language", out var value) == true) { return value.FirstOrDefault() ?? "EN"; } return "EN"; } public static void ChildSoftDelete<T>(this ICollection<T> childs) where T : BaseEntity { foreach (var item in childs) { item.GetType().GetProperty("IsDelete").SetValue(item, true); } } }
}
