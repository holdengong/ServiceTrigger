using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServiceTrigger.Hangfire
{
    [Table("hash")]
    public class Hash : Entity<int>
    {
        [MaxLength(100)]
        public string Key { get; set; }

        [MaxLength(40)]
        public string Field { get; set; }

        public string Value { get; set; }

        [DefaultValue(null)]
        public DateTime? ExpireAt { get; set; } = null;
    }
}
