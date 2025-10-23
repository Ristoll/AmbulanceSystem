using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceSystem.Core.Entities;

public partial class CallStatus
{
    [Key]
    [Column("CallStatusID")]
    public int CallStatusId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("CallStatus")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
    
    /*Waiting =1,
     * InProgress,
     * AtLocation,
     * Transporting,
     * Completed,
     * Cancelled
     */
}
