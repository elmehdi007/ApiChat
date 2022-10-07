using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace chat.Models
{
    //add-Migration createdatabase -o data/migration
    //update-database
    [Index(nameof(User.UserEmail), IsUnique = true)]
    [Index(nameof(User.UserPhone), IsUnique = true)]
    [Table("users")]
    public class User
    {
        [Column("id"), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column("user_last_name")]
        public string UserLastName { get; set; }
        [Column("user_first_name")]
        public string UserFirstName { get; set; }
        [Column("user_cnx_status")]
        public bool UserCnxStatus { get; set; }
        [Column("user_mail"), Required]
        public string UserEmail { get; set; }
        [Column("user_adress")]
        public string UserAdress { get; set; }
        [Column("user_phone")]
        public string UserPhone { get; set; }
        [Column("user_status")]
        public int UserStatus { get; set; }
        [Column("password_user"), Required]
        [JsonIgnore]
        public string UserPassword { get; set; }
        [Column("image_user")]
        public string UserImage { get; set; }
        [Column("is_connected"),DefaultValue(false)]
        public bool isConnected { get; set; }
        [Column("user_birth_date")]
        public DateTime UserBirthDate { get; set; }
        public string? token { get; set; }
    }
}
