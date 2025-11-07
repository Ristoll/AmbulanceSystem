using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;
public class UsedItemDTO
{
    public int UsedItemId { get; set; }
    public int ItemId { get; set; }
    public int CallId { get; set; }
    public int MedicalRecordId { get; set; }
    public int BrigadeId { get; set; }
    public int Quantity { get; set; }
}