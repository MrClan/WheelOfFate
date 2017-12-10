using RichmondGroupTechnicalTask.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RichmondGroupTechnicalTask.Models
{
    public class Schedule : IDbEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Shift Shift { get; set; }
        public Guid EngineerId { get; set; }
        public virtual Engineer Engineer { get; set; }
    }
}