using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MCQPuzzleGame.Model
{
   [Owned]
    public class RefreshTokens
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Created { get; set; }
        public string? CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplaceByToken { get; set; }
        public bool? IsActive => Revoked == null && !IsExpired;
    }
}
