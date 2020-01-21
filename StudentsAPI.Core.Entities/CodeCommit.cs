using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace StudentsAPI.Core.Entities
{
    public class CodeCommit
    {
        [Required]
        [JsonPropertyName("userId")]
        public long? UserId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required]
        [JsonPropertyName("linesModified")]
        public long? LinesModified { get; set; }
    }
}
